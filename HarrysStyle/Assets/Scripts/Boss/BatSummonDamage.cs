using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatSummonDamage : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private int attackDamage;
    private float knockbackForce = 20f;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
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
