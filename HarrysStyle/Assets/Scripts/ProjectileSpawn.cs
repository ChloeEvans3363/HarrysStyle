using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawn : MonoBehaviour
{
    public GameObject projectile;
    public float spawnYLimit = 6f;
    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        this.purSeek();
    }
    public void purSeek()
    {
        Vector3 spawnPos = transform.position;
        float theta = gameManager.calculateTheta(spawnPos);
        gameManager.AddProjectileToList(Instantiate(projectile, spawnPos, Quaternion.Euler(0, 0, theta)));
        Invoke("purSeek", 2.0f);
    }
}
