using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPlyer : MonoBehaviour
{
    private Rigidbody2D PlayerRigidbody2D;
    private BoxCollider2D boxCollider2D;

    [SerializeField] int Velocity;
    [SerializeField] int JumpSpeed;
    [SerializeField] private List<GameObject> Past = new List<GameObject>();
    [SerializeField] private List<GameObject> Present = new List<GameObject>();
    [SerializeField] private List<GameObject> Future = new List<GameObject>();
    private List<GameObject> ActualTime = new List<GameObject>();

    bool pastActive;
    bool presentActive;
    bool futureActive;

    private float Horizontal;
    // Start is called before the first frame update
    void Start()
    {

        PlayerRigidbody2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        pastActive = false;
        presentActive = true;
        futureActive = false;
        ActualTime = Present;
    }

    // Update is called once per frame
    void Update()
    {
        
        GoTo();
    }
    private void FixedUpdate()
    {
        Horizontal = Input.GetAxisRaw("Horizontal"); //Movimiento del jugador
        Movement();
    }
    private void Movement() //Movimiento normal del jugador
    {
        PlayerRigidbody2D.velocity = new Vector2(Horizontal * Velocity, PlayerRigidbody2D.velocity.y);
        if ((Input.GetKeyDown("w") || Input.GetKeyDown("up")) && CheckGround.IsGrounded)
        {
            PlayerRigidbody2D.velocity = new Vector2(PlayerRigidbody2D.velocity.x, JumpSpeed);
        }
    }
    private void GoTo()
    {
        if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.K) && !presentActive)
        { //presente
            presentActive = true;
            pastActive = false;
            futureActive = false;
            Change(ActualTime, Present);
        }
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.J) && !pastActive)
        {//pasdo
            pastActive = true;
            futureActive = false;
            presentActive = false;
            Change(ActualTime, Past);
        }
        if (Input.GetKeyDown(KeyCode.C)|| Input.GetKeyDown(KeyCode.L) && !futureActive)
        {//futuro
            futureActive = true;
            pastActive = false;
            presentActive = false;
            Change(ActualTime, Future);
        }
    }
    public void Change(List<GameObject> thisTime, List<GameObject> otherTime)
    {
   
        foreach (GameObject go in otherTime)
        {
            go.SetActive(true);
        }

        foreach (GameObject go in thisTime)
        {
            go.SetActive(false);
        }
        ActualTime = otherTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CheckGround.IsGrounded = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        CheckGround.IsGrounded = false;
    }
}
