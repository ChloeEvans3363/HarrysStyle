using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampireBoss : MonoBehaviour
{
    [Header("Game")]
    private GameObject player;

    [Header("Vampire Boss")]
    [SerializeField] private float waitTime = 3f; //cooldown for actions
    [SerializeField] private Color colorOG;
    private SpriteRenderer sprite;
    private Vector2 startingPos;
    private int flightDir = 1; //if the boss is currently flying up or down
    private int currentAttack = 1;

    [Header("Dash Attack")]
    [SerializeField] private float dashStartupOG = .5f; //how long it takes before the dash occurs (after locking onto a target position)
    [SerializeField] private float dashSpeed = 15f;
    [SerializeField] private Color dashColor;
    [SerializeField] private float dashStartup; //the current timer for the dash
    [SerializeField] private int dashProgress = 0;
    private Vector2 targetPos;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
        dashStartup = dashStartupOG;
        player = GameObject.FindWithTag("Player");
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (waitTime <= 0)
        {
            if(currentAttack == 0)
            {
                //randomly choose next attack
            }
            else if(currentAttack == 1)
            {
                DashAttack();
            }
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
            transform.position = new Vector2(transform.position.x, transform.position.y + (3f * Time.deltaTime));
            if (transform.position.y - startingPos.y > .5f)
            {
                flightDir = -1;
            }
        }
        else
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - (1f * Time.deltaTime));
            if (transform.position.y - startingPos.y < -.5f)
            {
                flightDir = 1;
            }
        }
        waitTime -= Time.deltaTime;
    }

    public void DashAttack()
    {
        if(dashStartup == dashStartupOG)
        {
            targetPos = player.transform.position; //the location that the vampire boss will dash to
            if(dashProgress == 1 && Mathf.Abs(transform.position.x - targetPos.x) > 0.5f)
            {
                if(transform.position.x - targetPos.x > 0f)
                {
                    targetPos.x -= 3f;
                }
                else if (transform.position.x - targetPos.x < 0f)
                {
                    targetPos.x += 3f;
                }
            }

            sprite.color = dashColor;
        }

        if(dashStartup > 0)
        {
            dashStartup -= Time.deltaTime;
            if(dashProgress == 1) //the second dash has less delay
            {
                dashStartup -= Time.deltaTime;
            }
        }
        else
        {
            if (Vector2.Distance(transform.position, targetPos) > 0.25f)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPos, dashSpeed * Time.deltaTime);
            }
            else
            {

                //resets the dash
                dashStartup = dashStartupOG;
                //2-part dash
                if(dashProgress == 0)
                {
                    waitTime = 0f;
                    dashProgress++;
                }
                else
                {
                    waitTime = 5f;
                    dashProgress = 0;
                    sprite.color = colorOG;
                }
            }
        }
    }
}
