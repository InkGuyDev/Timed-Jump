using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlayer : MonoBehaviour
{

    public List<Transform> checkTags;
    private Rigidbody2D rb;

    [SerializeField] private LayerMask wallJumpMask; // Permite escoger qué cosa los tags van a cosinderar
    [SerializeField] private LayerMask groundMask; // 


    public bool isGrounded;
    public bool wallLeftCheck;
    public bool wallRightCheck;
    public bool canWallLeft;
    public bool canWallRight;


    private MovementPlayer movementScript;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        movementScript = GetComponent<MovementPlayer>();
        checkTags.Add(transform.Find("tag_ground").transform);
        checkTags.Add(transform.Find("tag_left_wall").transform);
        checkTags.Add(transform.Find("tag_right_wall").transform);
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.Linecast(transform.position, checkTags[0].position, groundMask);
        wallLeftCheck = Physics2D.Linecast(transform.position, checkTags[1].position, wallJumpMask);
        wallRightCheck = Physics2D.Linecast(transform.position, checkTags[2].position, wallJumpMask);

        bool wallLeft = canWallLeft && wallLeftCheck && movementScript.GetDirectionLook() == false;
        bool wallRight = canWallRight && wallRightCheck && movementScript.GetDirectionLook() == true;

        if (wallLeft || wallRight)
        {
            if (rb.velocity.y < 0)
            {
                rb.velocity = Vector2.ClampMagnitude(rb.velocity, 2);
            }
        }
    }

    void Update()
    {
        // Esto limita la velocidad de caída
        float maxFallSpeed = movementScript.GetMaxFallSpeed();
        if (rb.velocity.y > maxFallSpeed) { rb.velocity = new Vector2(rb.velocity.x, maxFallSpeed); }
        else if (rb.velocity.y < -maxFallSpeed) { rb.velocity = new Vector2(rb.velocity.x, -maxFallSpeed); }
    }


    public void Jump(float jumpStrenght)
    {
        if (isGrounded)
        {
            AerialBoost(jumpStrenght);
            canWallLeft = true;
            canWallRight = true;
        }
        else if (!isGrounded)
        {
            if (wallLeftCheck)
            {
                if (canWallLeft)
                {
                    AerialBoost(jumpStrenght * 1.2f);
                    movementScript.HorizontalBoost(25);
                    canWallLeft = false;
                    canWallRight = true;
                }
            }
            if (wallRightCheck)
            {
                if (canWallRight)
                {
                    AerialBoost(jumpStrenght * 1.2f);
                    movementScript.HorizontalBoost(-25);
                    canWallLeft = true;
                    canWallRight = false;
                }
            }
        }
    }

    public void AerialBoost(float boost) => rb.velocity = new Vector2(rb.velocity.x, boost);

    public void BoolWallJump(bool isEnabled)
    {
        foreach (Transform tag in checkTags)
        {
            tag.gameObject.SetActive(isEnabled);
        }
    }

}
