using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //public float speed = 10f;

    private GameObject player;
    private Rigidbody2D rb;
    public float force;
    private float speed;
    private float timer;
    private int attackDamage;
    private float knockbackForce = 10f;

    Vector3 pos, vel;
    void Start()
    {
        //vel = Vector3.zero;
        attackDamage = 1;
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
    }

    void Update()
    {
        //pos = transform.position;
        //vel = speed * transform.right;
        //pos -= Time.deltaTime * vel;
        //transform.position = pos;

        timer += Time.deltaTime;

        if(timer > 5)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            if (!player.GetComponent<CharacterController>().invincible)
            {
                Vector2 direction = (collision.transform.position - transform.position).normalized;
                Vector2 knockback = direction * knockbackForce;
                player.GetComponent<CharacterController>().Knockback(knockback.x, knockback.y);
                player.GetComponent<CharacterController>().Damage(attackDamage);
            }
        }
    }
}
