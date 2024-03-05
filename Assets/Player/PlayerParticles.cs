using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticles : MonoBehaviour
{
    [Header("Particles")]
    [SerializeField] private GameObject dustGameObject;
    [SerializeField] private Vector3 jumpingDustOffset; 
    [SerializeField] private Vector3 landingDustOffset; 
    private ParticleSystem dust;

    [SerializeField] private ParticleSystem dustT;

    // Update is called once per frame
    public void spawnJumpingDust()
    {
        /*
        dust = Instantiate(dustGameObject, transform.position + jumpingDustOffset,  Quaternion.identity);
        dust.GetComponent<Dust>().playJumpingDustAnimation();*/

        dustT.Play();
    }

    public void spawnLandingDust()
    {
        /*
        dust = Instantiate(dustGameObject, transform.position + landingDustOffset,  Quaternion.identity);
        dust.GetComponent<Dust>().playLandingDustAnimation();*/
    }
}
