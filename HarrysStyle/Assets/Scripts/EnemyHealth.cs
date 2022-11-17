using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] public int health;
    [SerializeField] private bool isAlive;


    public void TakeDamage(int amount)
    {
        //damage animation
        health -= amount;
        if (health <= 0)
        {
            isAlive = false;
            //death animation, other death logic here
            Destroy(gameObject);
        }
    }
}
