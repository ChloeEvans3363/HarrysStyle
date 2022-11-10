using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserMover : MonoBehaviour
{
    public float speed = 1f;
    Vector3 pos;

    // Update is called once per frame
    void Update()
    {
        pos = transform.position;

        if(pos.y < 0)
        {
            pos.y -= -speed * Time.deltaTime;
        }
        else if(pos.y > 0)
        {
            pos.y += -speed * Time.deltaTime;
        }

        transform.position = pos;
    }
}
