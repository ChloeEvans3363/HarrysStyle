using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;

    Vector3 pos, vel;
    void Start()
    {
        vel = Vector3.zero;
    }

    void Update()
    {
        pos = transform.position;
        vel = speed * transform.right;
        pos -= Time.deltaTime * vel;
        transform.position = pos;
    }
}
