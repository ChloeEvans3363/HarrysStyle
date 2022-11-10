using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject circle;
    public GameObject laser;
    public GameObject movingLaser;
    public float minSpawnDelay = 1f;
    public float maxSpawnDelay = 3f;
    public float spawnYLimit = 6f;
    int circleShots = 0;

    // Start is called before the first frame update
    void Start()
    {
        randomAttack();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void randomAttack()
    {
        if (gameManager.GetComponent<GameManager>().bossBattle)
        {
            Invoke("circleAttack", 1f);

            Invoke("threeLineAttack", 25f);

            Invoke("movingLineAttackTop", 30f);

            Invoke("movingLineAttackBottom", 35f);

            Invoke("randomAttack", 40f);
        }
        else
        {
            Invoke("randomAttack", 1f);
        }
    }

    void movingLineAttackTop()
    {
        Vector3 spawnPos = new Vector3(transform.position.x - 10, transform.position.y + 4, transform.position.z);
        gameManager.AddMovingLaserFromList( Instantiate(movingLaser, spawnPos, Quaternion.identity));
    }
    
    void movingLineAttackBottom()
    {
        Vector3 spawnPos = new Vector3(transform.position.x - 10, transform.position.y - 4, transform.position.z);
        gameManager.AddMovingLaserFromList(Instantiate(movingLaser, spawnPos, Quaternion.identity));
    }

    void threeLineAttack()
    {
        if (gameManager.GetComponent<GameManager>().bossBattle)
        {
            Vector3 spawnPos = new Vector3(transform.position.x - 10, transform.position.y, transform.position.z);
            Vector3 spawnPos2 = new Vector3(transform.position.x -10, transform.position.y + 2, transform.position.z);
            Vector3 spawnPos3 = new Vector3(transform.position.x - 10, transform.position.y - 2, transform.position.z);
            gameManager.AddLaserFromList(Instantiate(laser, spawnPos, Quaternion.identity), Instantiate(laser, spawnPos2, Quaternion.Euler(0, 0, -10)), Instantiate(laser, spawnPos3, Quaternion.Euler(0, 0, 10)));
        }
    }

    void circleAttack()
    {

        if (gameManager.GetComponent<GameManager>().bossBattle)
        {
            Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
            Vector3 spawnPos2 = new Vector3(transform.position.x, transform.position.y - 2, transform.position.z);
            gameManager.AddCircleFromLIst(Instantiate(circle, spawnPos, Quaternion.identity), Instantiate(circle, spawnPos2, Quaternion.identity));
            circleShots++;
        }

        if(circleShots < 5)
        {
            Invoke("circleAttack", 3f);
        }

        else if(circleShots == 5)
        {
            circleShots = 0;
        }
    }
}
