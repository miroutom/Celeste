using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fatigue : MonoBehaviour
{
    [Header("Fatigue")]

    private float fatigue = 0;
    [SerializeField] private float maxFatigue = 10f;

    //
    private Color playerColor;
    bool isFlashing = false;
    [SerializeField] private float flashingFrequency = 0.2f;

    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();      
        playerColor = sprite.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    IEnumerator flashPlayer()
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
}
