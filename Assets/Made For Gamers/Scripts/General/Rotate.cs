using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
public class Rotate : MonoBehaviour
{
    public float speed = 1f; //rotation speed
    public bool direction;   // reverse roation toggle
    public bool rotationX;   // toggle rotation on X axis
    public bool rotationY;   // toggle rotation on Y axis
    public bool rotationZ;   // toggle rotation on Z axis

    private void Start()
    {        
        if (direction)
        {
            speed -= speed * 2; // reverse direction
        }
    }

    void Update()
    {        
        //Rotate realtime
        if (rotationX)
        {
            transform.Rotate(speed * Time.deltaTime, 0, 0);
        }
        if (rotationY)
        {
            transform.Rotate(0, speed * Time.deltaTime, 0);
        }
        if (rotationZ)
        {
            transform.Rotate(0, 0, speed * Time.deltaTime);
        }
    }
}
