using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class RumbleManager : MonoBehaviour
{
    [Header("Gamepad properties")]
    [SerializeField] public float dashLowFrequency = 0.7f;
    [SerializeField] public float dashHighFrequency = 0.8f;
    [SerializeField] private float dashRumbleDuration = 0.4f;
    [Header("Fatigue rumble")]
    [SerializeField] private float fatigueLowFrequency = 0.2f;
    [SerializeField] private float fatigueHighFrequency = 0.35f;

    private Gamepad pad;
    public void RumblePulse()
    {
        pad = Gamepad.current;

        pad.SetMotorSpeeds(dashLowFrequency, dashHighFrequency);       
        StartCoroutine(StopRumble(pad));
    }

    private IEnumerator StopRumble(Gamepad pad) 
    {
        float elapsedTime = 0f;

        while (elapsedTime <= dashRumbleDuration)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        pad.SetMotorSpeeds(0f, 0f);
    }

    public void startFatigueRumble()
    {
        pad = Gamepad.current;
        pad.SetMotorSpeeds(fatigueLowFrequency, fatigueHighFrequency);    
    }

    public void endFatigueRumble()
    {
        pad.SetMotorSpeeds(0f, 0f);
    }

    void OnApplicationQuit()
    {
        pad.SetMotorSpeeds(0f, 0f);
    }
}
