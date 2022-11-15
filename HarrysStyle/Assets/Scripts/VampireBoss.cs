using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampireBoss : MonoBehaviour
{
    [SerializeField] private float waitTime = 3f; //cooldown for actions
    private Vector2 startingPos;
    private int flightDir = 1; //if the boss is currently flying up or down
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (waitTime <= 0)
        {
            DashAttack();
        }
        else
        {
            Wait();
        }
    }

    public void Wait()
    {
        if (flightDir == 1)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + (1.5f * Time.deltaTime));
            if (transform.position.y - startingPos.y > .5f)
            {
                flightDir = -1;
            }
        }
        else
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - (1.5f * Time.deltaTime));
            if (transform.position.y - startingPos.y < -.5f)
            {
                flightDir = 1;
            }
        }
        waitTime -= Time.deltaTime;
    }

    public void DashAttack()
    {
        if (Vector2.Distance(transform.position, player.transform.position) > 0.5f)
        {
            Debug.Log("Attacking");
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, 5f * Time.deltaTime);
        }
        else
        {
            waitTime = 5f;
        }
    }
}
