using System;
using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [HideInInspector] 
    public enum State {idle, walk, jump, fall, climbDown, climbUp, death, climbStatic, grab, wallJump, pullUp};

    [HideInInspector] 
    public State state;

    [Header("Climb")]
    [SerializeField] private float climbUpSpeed = 2;
    [SerializeField] private float climbDownSpeed = -5;
    [SerializeField] private float climbSlip = -2f;


    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Collider2D coll;

    [Header("Obstacle")]
    [SerializeField] public LayerMask groundLayer;
    [SerializeField] private string killerTag = "killer";

    [Header("Debug")]
    [SerializeField] private Text stateText;
    [SerializeField] private Text onGroundText;
    [SerializeField] private Text fatigueText;
    private string textState = "";
    private string textOnGround = "";
    private string textFatigue = "";

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        //Debug
        stateText.text = "State: " + textState;

        if (jumpPressed)
        {
            textOnGround = "True";
        }
        else
        {
            textOnGround = "False";
        }
        onGroundText.text = "OnGround: " + textOnGround;

        fatigueText.text = "Fatigue: " + Math.Round(fatigue, 1) + "/" + maxFatigue;
    }

    void FixedUpdate()
    {
        state = getState();

        if (state != State.grab && state != State.climbUp && state != State.climbDown)
        {
            Move();
            Flip();
        }


        if (landed)
        {
            spawnLandingDust();
        }

        if (onGround)
        {
            fatigue = 0f;
        }

        if (fatigue >= maxFatigue && !isFlashing)
        {  
            StartCoroutine(flashPlayer());
        }

        //PseudoParallax();
    }
    
    private State getState()
    {
        if (state == State.death)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            return State.death;
        }

        rb.gravityScale = basicGravityScale;   

        if (onPullUp && wallJump == false)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce / 1.4f);

            wallJump = true;
            return State.jump;
            //tossAsideTimer = tossAsideDelay;
        }

        if (onWall && jumpPressed && (state == State.grab || state == State.climbDown || state == State.climbUp))
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            Jump();
            fatigue += 2.5f;

            wallJump = true;

            textState = "WallJump";
            return State.wallJump;
        }

        if (onWall && grabPressed && !wallJump)
        {
            rb.gravityScale = 0;
            rb.velocity = new Vector2(rb.velocity.x, 0f);

            if (fatigue >= maxFatigue)
            {
                textState = "Slip";        

                rb.velocity = new Vector2(rb.velocity.x, climbSlip);    

                return State.climbDown;
            }
            else if (climbUp)
            {
                rb.velocity = new Vector2(rb.velocity.x, climbUpSpeed);

                textState = "ClimbUp";

                fatigue += Time.deltaTime;
                return State.climbUp;
            }
            else if (climbDown)
            {
                rb.velocity = new Vector2(rb.velocity.x, climbDownSpeed);

                textState = "ClimbDown";
        
                fatigue += Time.deltaTime;
                return State.climbDown;
            }

            textState = "Grab";

            fatigue += Time.deltaTime;
            return State.grab;   
        }

        if ((coyoteTimeCounter > 0f) && (jumpBufferCounter > 0f))
        {
            Jump();   
            spawnJumpingDust();

            textState = "Jump";

            return State.jump;
        }

        if (rb.velocity.y < -jumpBorder)
        {
            textState = "Fall";

            wallJump = false;
            return State.fall;
        }

        if (rb.velocity.y > jumpBorder)
        {
            textState = "Jump";
            return State.jump;
        }

        if (rb.velocity.x != 0)
        {
            textState = "Walk";
            return State.walk;
        }

        textState = "Idle";
        return State.idle;
    }

    private void PullUp()
    {
        if (tossAsideTimer > 0)
        {
            tossAsideTimer -= Time.deltaTime;
        }
        else
        {
            pullUp = false;

            rb.velocity = 20 * getPlayerDirection() + Vector2.up;
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

    private Vector2 getPlayerDirection()
    {
        if (sprite.flipX == true)
        {
            return Vector2.left;
        }

        return Vector2.right;
    }

    void OnCollisionEnter2D(Collision2D col) 
    {
        if (col.gameObject.tag == "Killer")
        {
            state = State.death;
        }
    }

    //Particles

}
