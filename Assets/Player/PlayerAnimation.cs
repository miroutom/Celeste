using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private enum AnimationState {idle, walk, jump, fall, climbDown, climbUp, death};
    private AnimationState state = AnimationState.idle;

    private Player player;
    private Animator anim;

    void Start()
    {
        player = GetComponent<Player>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (player.horizontalInput != 0f)
        {
            state = AnimationState.walk;
        }
        else
        {
            state = AnimationState.idle;
        }

        if (player.rb.velocity.y > .3f)
        {
            state = AnimationState.jump;
        }
        
        if (player.rb.velocity.y < -.3f)
        {
            state = AnimationState.fall;
        }

        if (player.isAlive == true)
        {
            anim.SetInteger("state", (int)state);
        }
        else
        {
            anim.SetInteger("state", (int)AnimationState.death);
        }

    }
}
