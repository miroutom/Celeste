using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [HideInInspector]
    public bool isDashing = false;
    [HideInInspector]
    public bool hasDashed = false;

    [SerializeField] private float dashPower = 10f;
    [SerializeField] private float dashTimer = 1f;

    private Rigidbody2D rb;
    private PlayerJump jump;
    private PlayerInput input;
    private Player player;

    private float dashTime = 0.2f;
    private float dashTimeCounter;

    private float inputBufferTime = 0.2f;
    private float inputBufferTimeCounter;

    private float horizontalBuffer;
    private float verticalBuffer;

    [SerializeField] private GameObject shadowGameObject;
    private GameObject shadow;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jump = GetComponent<PlayerJump>();
        input = GetComponent<PlayerInput>();
        player = GetComponent<Player>();
    }

    void Update()
    {
        dashTimeUpdate();
        inputBufferTimeUpdate();
    }

    public bool dashCanBeDone()
    {
        if (dashTimeCounter > 0f && inputBufferTimeCounter > 0f)
        {
            inputBufferTimeCounter = 0;
            return true;
        }

        return false;
    }

    private void inputBufferTimeUpdate()
    {
        if (input.horizontalInput != 0 || input.verticalInput != 0)
        {
            inputBufferTimeCounter = inputBufferTime;
            horizontalBuffer = input.horizontalInput;
            verticalBuffer = input.verticalInput;
        }
        else
        {
            inputBufferTimeCounter -= Time.deltaTime;
        }
    }

    private void dashTimeUpdate()
    {
        if (input.dashPressed)
        {
            dashTimeCounter = dashTime;
        }
        else
        {
            dashTimeCounter -= Time.deltaTime;
        }
    }

    public void DashStart()
    {
        StartCoroutine(DashManager());
    }

    IEnumerator DashManager()
    {
        isDashing = true;
        hasDashed = true;
        jump.nullifyGravity();
        rb.velocity = Vector2.zero;
        Dash();

        float timer = 0;
        while (timer < dashTimer)
        {
            timer += 0.1f;
            spawnShadow();

            yield return new WaitForSeconds(0.1f);
        }



        isDashing = false;
        jump.refreshGravity();
        
        rb.velocity = Vector2.zero;
    }

    private void spawnShadow()
    {
        shadow = Instantiate(shadowGameObject, transform.position,  Quaternion.identity);
        Shadow shadowScript = shadow.GetComponent<Shadow>();

        if (player.getPlayerDirection() == Vector2.left)
        {
            shadowScript.Flip();
        }

    }

    public void Dash() 
    {
        rb.velocity = new Vector2(horizontalBuffer, verticalBuffer).normalized * dashPower;
    }

    public void dashRefresh()
    {
        hasDashed = false;
    }
}
