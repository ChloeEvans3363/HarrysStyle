using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    public float speed;
    private GameObject player;
    public Rigidbody2D rigid;
    private float knockbackForce = 30f;
    public float distance;
    private float heightLimit;
    private float heightMinimum;
    private bool up;

    public GameObject projectile;
    public Transform projectilePos;
    private float timer;
    private int attackDamage;

    // Start is called before the first frame update
    void Start()
    {
        heightLimit = transform.position.y + distance;
        heightMinimum = transform.position.y - distance;
        up = true;
        attackDamage = 1;
        player = GameObject.FindGameObjectWithTag("Player");
        timer = 3;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        float distance = Vector2.Distance(transform.position, player.transform.position);

        if(distance < 10)
        {

            if (timer > 3)
            {
                timer = 0;
                shoot();
            }
        }

        if (transform.position.y > heightLimit)
        {
            up = false;
        }
        else if(transform.position.y < heightMinimum)
        {
            up = true;
        }

        if (up)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
        }
    }

    void shoot()
    {
        Instantiate(projectile, projectilePos.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.GetComponent<CharacterController>().Damage(attackDamage);
            //Debug.Log("hit");
            //Vector2 direction = (collision.transform.position - transform.position).normalized;
            //Vector2 knockback = direction * knockbackForce;
            // player.GetComponent<Rigidbody2D>().AddForce(knockback, ForceMode2D.Impulse);
        }
    }
}
