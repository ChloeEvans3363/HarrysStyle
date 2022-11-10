using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour
{ 

    private Vector3 position;
    private Vector3 velocity;
    private Vector3 acceleration;

    Vector3 max;
    Vector3 min;

    public float MAX_SPEED = 4;

    public const float MAX_ACC_PER_TIMESTEP = 2f;

    private const float MIN_SPEED = 0.1f;


    SpriteRenderer spriteRenderer;

    public GameObject sueshe;

    private void Start()
    {
        // Initialize all the vectors
        position = transform.position;
        velocity = Vector3.zero;
        acceleration = Vector3.zero;
        spriteRenderer = GetComponent<SpriteRenderer>();

        Camera cam = Camera.main;
        max = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, cam.pixelHeight, cam.nearClipPlane));
        min = cam.ScreenToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));

    }

    private void Update()
    {

        acceleration = Seek(sueshe.transform.position);

        velocity += acceleration * Time.deltaTime;

        position += velocity * Time.deltaTime;

        transform.position = position;

        //Keep shark pointed in direction of velocity, unnecessary (and potentially numerically unstable) when speed is too close to zero
        if (velocity.magnitude >= MIN_SPEED)
            transform.right = velocity.normalized;
  
        if (velocity.x < 0)
            spriteRenderer.flipY = true;
        else
            spriteRenderer.flipY = false;

        Bounce();

    }

    /// <summary>
    /// Elastically bounces the vehicle off the screen boundaries
    /// </summary>
    private void Bounce()
    {

        if (position.x > max.x && velocity.x > 0)
        {
            velocity.x *= -1;
            position.x = max.x;
        }
        if (position.y > max.y && velocity.y > 0)
        {
            velocity.y *= -1;
            position.y = max.y;
        }
        if (position.x < min.x && velocity.x < 0)
        {
            velocity.x *= -1;
            position.x = min.x;
        }
        if (position.y < min.y && velocity.y < 0)
        {
            velocity.y *= -1;
            position.y = min.y;
        }
    }


    private Vector3 Seek(Vector3 targetPosition)
    {
        Vector3 desiredVelocity = (targetPosition - position);
        desiredVelocity.Normalize();
        desiredVelocity *= MAX_SPEED;
        Vector3 steeringAcceleration = (desiredVelocity - velocity).normalized * MAX_ACC_PER_TIMESTEP; 
        return steeringAcceleration;
    }
}
