using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public GameObject GameCamera;
    public float GameParallax;
    float StartPosition;

    void Start()
    {
        StartPosition = transform.position.x;
    }

    void Update()
    {
        float DistanceX = (GameCamera.transform.position.x * (1 - GameParallax));
        transform.position = new Vector3(StartPosition + DistanceX, transform.position.y, transform.position.z);
    }
}
