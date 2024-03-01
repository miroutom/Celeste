using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private PlayerState playerState;
    private Animator anim;

    void Start()
    {
        playerState = GetComponent<PlayerState>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Debug.Log("Lol: " + playerState.state);
        anim.SetInteger("state", (int)playerState.state);
    }
}
