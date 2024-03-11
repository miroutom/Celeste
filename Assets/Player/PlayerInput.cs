using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [HideInInspector] 
    public float horizontalInput = 0;
    [HideInInspector] 
    public float verticalInput = 0;

    [HideInInspector] 
    public bool jumpPressed;
    [HideInInspector] 
    public bool dashPressed;
    [HideInInspector] 
    public bool grabPressed;
    
    [HideInInspector] 
    public bool climbUp;
    [HideInInspector] 
    public bool climbDown;

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        
        jumpPressed = Input.GetKeyDown(KeyCode.Space);
        dashPressed = Input.GetKeyDown(KeyCode.LeftShift);
        grabPressed = Input.GetKey(KeyCode.LeftControl);

        climbUp = Input.GetKey(KeyCode.UpArrow);
        climbDown = Input.GetKey(KeyCode.DownArrow);

    }
}
