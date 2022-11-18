using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] public int health;
    [SerializeField] private bool isAlive = true;
    public HealthBar healthBar;

    private void Start()
    {
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(health);
        }
    }


    public void TakeDamage(int amount)
    {
        //damage animation
        health -= amount;
        healthBar.SetHealth(health);
        if (health <= 0)
        {
            isAlive = false;
            //death animation, other death logic here
            Destroy(gameObject);
        }
    }
}
