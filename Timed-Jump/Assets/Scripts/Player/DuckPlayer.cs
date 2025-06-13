using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckPlayer : MonoBehaviour
{

    public List<Transform> duckTags;

    private BoxCollider2D bc;
    private Transform sprite; // Después se cambia por un SpriteRenderer

    private JumpPlayer jumpScript;

    [SerializeField] private LayerMask duckMask;

    public bool canStand;

    void Start()
    {
        bc = GetComponent<BoxCollider2D>();
        sprite = transform.Find("Sprite").transform;
        jumpScript = GetComponent<JumpPlayer>();
        duckTags.Add(transform.Find("tag_duck_left").transform);
        duckTags.Add(transform.Find("tag_duck_right").transform);
    }

    private void FixedUpdate()
    {
        canStand = Physics2D.Linecast(transform.position, duckTags[0].position, duckMask) || Physics2D.Linecast(transform.position, duckTags[1].position, duckMask);
    }

    public void Duck(float height, float position, bool tags)
    {
        bc.size = new Vector2(bc.size.x, height);
        sprite.localScale = new Vector3(sprite.localScale.x, height, sprite.localScale.z);
        sprite.localPosition = new Vector3(sprite.localPosition.x, position, sprite.localPosition.z);
        jumpScript.BoolWallJump(tags);
        BoolDuckTags(!tags);
    }

    private void BoolDuckTags(bool isEnabled)
    {
        foreach (Transform tag in duckTags)
        {
            tag.gameObject.SetActive(isEnabled);
        }
    }
    
    public bool GetCanStand() => canStand;
}
