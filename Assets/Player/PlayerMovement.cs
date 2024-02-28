using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float MoveSpeed = 5;

    private PlayerJump jump;

    void Start()
    {
        jump = GetComponent<PlayerJump>();
    }

    // Update is called once per frame
    void Update()
    {
        jump.timeCoyotize();
        jump.jumpBufferize();       
    }
}
