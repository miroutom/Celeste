using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [Header("Jump")]
    private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;

    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;
    
    [SerializeField] private float jumpForce = 7;
    [SerializeField] private float jumpBorder = .3f;
    [SerializeField] private float fallForce = 3;

    private float basicGravityScale;

    private Rigidbody2D rb;
    private Indicators indicators;
    private PlayerInput input;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        indicators = GetComponent<Indicators>();
        input = GetComponent<PlayerInput>();

        basicGravityScale = rb.gravityScale;  
    }

    public void timeCoyotize()
    {
        if (indicators.onGround)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
    }

    public void jumpBufferize()
    {
        if (input.jumpPressed)
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }
    }

    public void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        jumpBufferCounter = 0f;
    }
}
