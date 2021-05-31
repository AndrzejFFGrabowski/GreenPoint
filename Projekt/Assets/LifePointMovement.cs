using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;
using System;
public class LifePointMovement : MonoBehaviour
{
    private Vector3 vector3;
    private Vector3 V;
    Boolean run;
    private static int velocity = 10;
    void Start()
    {

        if (this.transform.position.x == 13)
        {
            run = false;
        }
        else
        {
            run = true;
            setDirection();
        }

    }

    void Update()
    {
        if (run) this.transform.position += V * Time.deltaTime * velocity;
    }

    void setDirection()
    {
        Random random = new Random();
        float x = 0.5f / (float)random.Next(1, 2);
        float y = 0.5f / (float)random.Next(1, 2);
        float z = (float)Math.Sqrt((double)1.0f - x * x - y * y);
        if (random.Next(2) == 0) x = -x;
        if (random.Next(2) == 0) y = -y;
        if (random.Next(2) == 0) z = -z;
        V = new Vector3(x, y, z);
    }
    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            V = Quaternion.AngleAxis(180, contact.normal) * transform.forward * -1;
        }
    }
}
