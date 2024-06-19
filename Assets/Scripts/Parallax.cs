using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public GameObject GameCamera;
    public float GameParallax;
    float StartPositionX, StartPositionY;

    void Start()
    {
        StartPositionX = transform.position.x;
        StartPositionY = transform.position.y;
    }

    void Update()
    {
        float DistanceX = (GameCamera.transform.position.x * (1 - GameParallax));
        float DistanceY = (GameCamera.transform.position.y * (1 - GameParallax));
        transform.position = new Vector3(StartPositionX + DistanceX, StartPositionY + DistanceY, transform.position.z);
    }
}
