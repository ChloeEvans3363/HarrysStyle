using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    private float xVel;
    [SerializeField] private float platformSpeed;
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        xVel = platformSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if(transform.position.x > maxX)
        {
            xVel = platformSpeed * -1f;
        }
        else if (transform.position.x < minX)
        {
            xVel = platformSpeed;
        }
        rb2d.velocity = new Vector2(xVel, 0f);
    }

    public float GetPlatformXVel()
    {
        return xVel;
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(new Vector3(minX, transform.position.y, 0f), new Vector3(maxX, transform.position.y, 0f));
        Gizmos.DrawWireCube(new Vector3(maxX, transform.position.y, 0f), new Vector3(2f, 1f, 0f));
        Gizmos.DrawWireCube(new Vector3(minX, transform.position.y, 0f), new Vector3(2f, 1f, 0f));
    }
}
