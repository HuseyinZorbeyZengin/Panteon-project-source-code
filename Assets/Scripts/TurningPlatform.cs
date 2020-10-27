using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurningPlatform : MonoBehaviour
{

    public float speed;
    void Start()
    {

    }


    void Update()
    {
        //this will make object spin on the y axis
        transform.Rotate(0f, speed, 0f);
    }
}
