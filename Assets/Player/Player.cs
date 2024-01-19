using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float horizontalInput = 0;
    public float verticalInput = 0;

    public bool isAlive = true;

    [SerializeField] private float MoveSpeed = 5;
    [SerializeField] private float jumpForce = 7;

    public Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Collider2D coll;
    [SerializeField] private string obstacleLayer;
    [SerializeField] private string killerTag = "killer";

    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();

        anim = GetComponent<Animator>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        if (isAlive == true) 
        {
            Move();
            Flip();
            
            Jump();
        }
        else 
        {
            StopMoving();
            rb.gravityScale = 0;
        }
    }

    private void Kill()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Move()
    {
        rb.velocity = new Vector2(horizontalInput * MoveSpeed, rb.velocity.y);
    }

    private void StopMoving()
    {
        rb.velocity = Vector2.zero;
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            //rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0, Vector2.down, .1f, LayerMask.GetMask(obstacleLayer));

        return hit.collider != null;
    }

    private void Flip()
    {
        if (horizontalInput < 0)
        {
            sprite.flipX = true;
        }
        else if (horizontalInput > 0)
        {
            sprite.flipX = false;
        }

    }

    void OnCollisionEnter2D(Collision2D col) 
    {
        Debug.Log("Collision");
        if (col.gameObject.tag == "Killer")
        {
            isAlive = false;
        }
    }
}
