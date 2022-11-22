using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    [SerializeField] private int attackDamage;
    private float knockbackForce = 30f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!collision.GetComponent<CharacterController>().invincible)
            {
                Vector2 direction = (collision.transform.position - transform.position).normalized;
                Vector2 knockback = direction * knockbackForce;
                collision.GetComponent<CharacterController>().Knockback(knockback.x, knockback.y);
                collision.GetComponent<CharacterController>().Damage(attackDamage);
            }
        }
    }
}
