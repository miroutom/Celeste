
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [HideInInspector] 
    public enum State {idle, walk, jump, fall, climbDown, climbUp, death, climbStatic, climb, };

    [HideInInspector] 
    public State state;

    private float horizontalInput = 0;
    private float verticalInput = 0;

    [Header("Movement")]
    [SerializeField] private float MoveSpeed = 5;

    [Header("Climb")]
    [SerializeField] private float ClimbUpSpeed = 2;
    [SerializeField] private float ClimbDownSpeed = 5;
    [SerializeField] private float ClimbSlip = -2f;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 7;
    [SerializeField] private float jumpBorder = .3f;
    [SerializeField] private float fallForce = 3;

    private float basicGravityScale;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Collider2D coll;

    [Header("Obstacle")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private string killerTag = "killer";

    [Header("Debug")]
    [SerializeField] private Text stateText;

    private bool onWall;
    private bool onGround;

    [Header("Collision")]
    [SerializeField] private float collisionRadius;
    [SerializeField] private Vector2 bottomOffset;
    [SerializeField] private Vector2 rightOffset;
    [SerializeField] private Vector2 leftOffset;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();


        basicGravityScale = rb.gravityScale;
    }

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        onGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, groundLayer);
        onWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer) ||
            Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer);
    }

    void FixedUpdate()
    {
        if (state != State.death)
        {
            rb.gravityScale = basicGravityScale;

            if ((rb.velocity.x == 0 && rb.velocity.y == 0) && state != State.climb)
            {
                state = State.idle;
            }

            if (rb.velocity.x != 0)
            {
                state = State.walk;
            }



            if (rb.velocity.y >= jumpBorder && state != State.climb && state != State.climbUp && state != State.climbDown)
            {
                state = State.jump;
            }
            
            if (rb.velocity.y <= -jumpBorder)
            {
                state = State.fall;
            }

            if (onWall && Input.GetKey(KeyCode.LeftControl) && state != State.jump)
            {
                state = State.climb;
            }

            if ((Input.GetKeyDown(KeyCode.Space) && IsGrounded()) ||
                ((state == State.climb || state == State.climbUp || state == State.climbDown) && Input.GetKeyDown(KeyCode.Space)) )
            {
                Debug.Log("Jump!");
                state = State.jump;
                Jump();
            }

            if (!Input.GetKey(KeyCode.LeftControl) && state == State.climb)
            {
                state = State.fall;
            }
        }

        string textState = "";
        switch(state)
        {
            case State.idle:
            {
                Move();
                Flip();

                textState = "Idle";
                break;
            }
            case State.walk:
            {
                Move();
                Flip();

                textState = "Walk";
                break;
            }
            case State.jump:
            {
                Move();
                Flip();

                textState = "Jump";
                break;
            }
            case State.fall:
            {
                Move();
                Flip();
                
                textState = "Fall";
                break;
            }
            case State.climb:
            {
                bool climbUp = Input.GetKey(KeyCode.UpArrow);
                bool climbDown = Input.GetKey(KeyCode.DownArrow);

                rb.velocity = Vector2.zero;
                rb.gravityScale = 0;

                if ((climbUp && climbDown) || (!climbUp && !climbDown))
                {
                    state = State.climb;   
                    textState = "Climb";
                }
                else if (climbUp)
                {
                    state = State.climbUp;
                    rb.velocity = new Vector2(rb.velocity.x, 2 * ClimbUpSpeed);
                    //rb.velocity += new Vector2(0, ClimbSlip);
                    textState = "ClimbUp";
                }
                else if (climbDown)
                {
                    state = State.climbDown;
                    rb.velocity = new Vector2(rb.velocity.x, -ClimbDownSpeed);
                    //rb.velocity += new Vector2(0, ClimbSlip);
                    textState = "ClimbDown";
                }
                RaycastHit2D hit;
            
                Vector2 rayDirection = Vector2.right;
                if (sprite.flipX == true)
                {
                    rayDirection = Vector2.left;
                }
                hit = Physics2D.Raycast(transform.position + new Vector3(0, 0.5f, 0), rayDirection, coll.bounds.size.x / 2 + .1f, groundLayer);

                if (hit.collider == null)
                {
                    state = State.jump;
                    rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
                    rb.gravityScale = basicGravityScale;
                
                    textState = "Jump";
                }
                //Debug.DrawRay(transform.position, Vector2.right * (coll.bounds.size.x / 2 + .1f), Color.black);

                break;
            }
            case State.death:
            {
                StopMoving();

                textState = "Death";
                break;
            }
        }


        //Debug

        stateText.text = "State: " + textState;
    }

    private State getState()
    {
        if (state == State.death)
        {
            return State.death;
        }

        bool jumpPressed = Input.GetKeyDown(KeyCode.Space);
        bool grabPressed = Input.GetKey(KeyCode.LeftControl);
        bool climbUp = Input.GetKey(KeyCode.UpArrow);
        bool climbDown = Input.GetKey(KeyCode.DownArrow);

        if (onWall && jumpPressed)
        {
            Jump();
        }

        if (onWall && grabPressed)
        {
            if (climbUp)
            {

            }
            else if (climbDown)
            {

            }
            else 
            {

            }
        }

        if (onGround && jumpPressed)
        {
            
        }

        if (rb.velocity.x != 0)
        {
            return State.walk;
        }

        return State.idle;
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
        rb.gravityScale = 0;
    }

    private void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

/*
    private bool onWall()
    {
        RaycastHit2D hit;
        Vector2 boxDirection = Vector2.right;
        if (sprite.flipX == true)
        {
            boxDirection = Vector2.left;
        }

        //hit = Physics2D.BoxCast(coll.bounds.center - coll.bounds.size / 4, coll.bounds.size / 2, 0, boxDirection, .1f, LayerMask.GetMask(groundLayer));      
        hit = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0, boxDirection, .1f, LayerMask.GetMask(groundLayer));

        return hit.collider != null;
    }
*/
    void OnDrawGizmos()
    {
        //Gizmos.DrawCube(kek, lol);
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0, Vector2.down, .05f, groundLayer);

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
        if (col.gameObject.tag == "Killer")
        {
            state = State.death;
        }
    }
}
