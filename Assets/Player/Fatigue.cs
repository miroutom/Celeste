using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fatigue : MonoBehaviour
{
    [Header("Fatigue")]

    public float fatigue = 0;
    [SerializeField] public float maxFatigue = 10f;

    //
    private Color playerColor;
    public bool isFlashing = false;
    [SerializeField] private float flashingFrequency = 0.2f;

    [SerializeField] public float fatigueTick = 1f;
    [SerializeField] public float fatigueJumpTick = 2.5f;

    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();      
        playerColor = sprite.color;
    }
    
    public IEnumerator FlashPlayer()
    {
        isFlashing = true;

        while(fatigue >= maxFatigue)
        {
            if (sprite.color == playerColor)
            {
                sprite.color = Color.red;
            }
            else
            {
                sprite.color = playerColor;
            }

            yield return new WaitForSeconds(flashingFrequency);
        }

        isFlashing = false;
        sprite.color = playerColor;

        yield break;
    }

    public void Tick()
    {
        fatigue += fatigueTick * Time.deltaTime;
    }

    public void JumpTick()
    {
        fatigue += fatigueJumpTick;
    }

    public void nullifyFatigue()
    {
        fatigue = 0f;
    }
}
