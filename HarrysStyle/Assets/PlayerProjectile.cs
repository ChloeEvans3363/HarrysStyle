using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public float speed = 20f;
    public bool bouncer = false;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
       // rb = GetComponent<Rigidbody2D>();
        if (bouncer)
        {
            GetComponent<CircleCollider2D>().isTrigger = false;
        }
       
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player")
        {
            Destroy(gameObject);
        }

        
    }

}
