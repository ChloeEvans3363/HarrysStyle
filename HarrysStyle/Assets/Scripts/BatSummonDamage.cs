using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatSummonDamage : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private int attackDamage;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.GetComponent<CharacterController>().Damage(attackDamage);
        }
    }
}
