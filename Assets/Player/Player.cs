using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public enum State {idle, walk, jump, fall, climbDown, climbUp, death, climbStatic, climb};

    public State state;

    public float horizontalInput = 0;
    public float verticalInput = 0;

    [SerializeField] private float MoveSpeed = 5;
    [SerializeField] private float ClimbUpSpeed = 2;
    [SerializeField] private float ClimbDownSpeed = 5;
    [SerializeField] private float ClimbSlip = -2f;

    [SerializeField] private float jumpForce = 7;
    [SerializeField] private float jumpBorder = .3f;

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
        if (rb.velocity.x == 0 && rb.velocity.y == 0)
        {
            state = State.idle;
        }

        if (rb.velocity.x != 0)
        {
            state = State.walk;
        }

        if (state is State.idle or State.walk && Input.GetKeyDown(KeyCode.Space) && IsGrounded() ||
            (state is State.climb or State.climbUp or State.climbDown && Input.GetKeyDown(KeyCode.Space)) )
        {
            state = State.jump;
            Jump();
        }

        if (rb.velocity.y >= jumpBorder && state != State.climb && state != State.climbUp && state != State.climbDown)
        {
            state = State.jump;
        }

        if (rb.velocity.y <= -jumpBorder)
        {
            state = State.fall;
        }

        if (IsBumped() && Input.GetKey(KeyCode.Tab) && state is not State.jump)
        {
            state = State.climb;
        }

        if (!Input.GetKey(KeyCode.Tab) && state == State.climb)
        {
            state = State.fall;
        }

        switch(state)
        {
            case State.idle:
            {
                Move();
                Flip();

                break;
            }
            case State.walk:
            {
                Move();
                Flip();

                break;
            }
            case State.jump:
            {

                Move();
                Flip();

                break;
            }
            case State.fall:
            {
                Move();
                Flip();

                break;
            }
            case State.climb:
            {
                bool climbUp = Input.GetKey(KeyCode.W);
                bool climbDown = Input.GetKey(KeyCode.S);

                rb.velocity = Vector2.zero;
                if ((climbUp && climbDown) || (!climbUp && !climbDown))
                {
                    state = State.climb;
                    rb.velocity += new Vector2(0, 0.2f);      
                }
                else if (climbUp)
                {
                    state = State.climbUp;
                    rb.velocity = new Vector2(rb.velocity.x, 2 * ClimbUpSpeed);
                    rb.velocity += new Vector2(0, ClimbSlip);
                }
                else if (climbDown)
                {
                    state = State.climbDown;
                    rb.velocity = new Vector2(rb.velocity.x, -ClimbDownSpeed);
                    rb.velocity += new Vector2(0, ClimbSlip);
                }

                break;
            }
            case State.death:
            {
                StopMoving();
                rb.gravityScale = 0;

                break;
            }
        }

        Debug.Log(state);
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
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    private bool IsBumped()
    {
        RaycastHit2D hit;
        if (sprite.flipX == true)
        {
            hit = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0, Vector2.left, .1f, LayerMask.GetMask(obstacleLayer));
        }
        else 
        {
            hit = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0, Vector2.right, .1f, LayerMask.GetMask(obstacleLayer));      
        }

        return hit.collider != null;
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0, Vector2.down, .05f, LayerMask.GetMask(obstacleLayer));

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
            state = State.death;
        }
    }
}
