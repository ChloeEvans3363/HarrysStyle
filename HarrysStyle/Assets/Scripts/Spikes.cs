using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private int attackDamage = 1;
    public GameObject checkpoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<CharacterController>().transform.position = checkpoint.transform.position;

            if (!collision.GetComponent<CharacterController>().invincible)
            {
                collision.GetComponent<CharacterController>().Damage(attackDamage);
            }
        }
    }
}
