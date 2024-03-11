using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticles : MonoBehaviour
{
    [Header("Particles")]

    [SerializeField] private Vector3 jumpingDustOffset; 
    [SerializeField] private Vector3 landingDustOffset; 

    [SerializeField] private GameObject dustGameObject;

    [SerializeField] private ParticleSystem dashDust;

    // Update is called once per frame
    public void spawnJumpingDust()
    {
        Instantiate(dustGameObject, transform.position + jumpingDustOffset,  Quaternion.identity);
    }

    public void spawnLandingDust()
    {
        Instantiate(dustGameObject, transform.position + jumpingDustOffset,  Quaternion.identity);
    }

    public void spawnDashDust()
    {
        dashDust.time = 0;
        dashDust.Play();
    }
}
