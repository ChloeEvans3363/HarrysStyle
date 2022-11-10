using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawn : MonoBehaviour
{
    public GameObject bullet;
    public float spawnYLimit = 6f;
    public GameManager gameManager;

    private void Start()
    {
        this.purSeek();
    }

    public void purSeek()
    {
        float random = Random.Range(-spawnYLimit, spawnYLimit);
        Vector3 spawnPos = transform.position + new Vector3(0, random, 0f);
        float theta = gameManager.calculateTheta(spawnPos);
        gameManager.AddMissileToList(Instantiate(bullet, spawnPos, Quaternion.Euler(0, 0, theta)));
        Invoke("purSeek", 0.5f);
    }
}
