using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampireBoss : MonoBehaviour
{
    [Header("Game")]
    private GameObject player;
    public AudioSource speaker;
    public AudioClip basicAttack;
    public AudioClip orbAttack;

    [Header("Vampire Boss")]
    [SerializeField] private float waitTime = 3f; //cooldown for actions
    [SerializeField] private Color colorOG;
    [SerializeField] private int currentAttack = 0; //change this number to whatever attack you want to test
    private SpriteRenderer sprite;
    private Vector2 startingPos;
    private int flightDir = 1; //if the boss is currently flying up or down

    [Header("Dash Attack")]
    [SerializeField] private float dashStartupOG = .5f; //how long it takes before the dash occurs (after locking onto a target position)
    [SerializeField] private float dashStartup; //the current timer for the dash
    [SerializeField] private float dashSpeed = 15f;
    [SerializeField] private Color dashColor;
    [SerializeField] private int dashProgress = 0;
    [SerializeField] private float dashStun; //how long the boss has to wait after using dash
    private Vector2 targetPos;

    [Header("Summon Bats")]
    [SerializeField] private GameObject[] batSummons;
    [SerializeField] private float batStartingY = 5.7f;
    [SerializeField] private bool batsSummoned = false;
    [SerializeField] private float batSpeed = 5f;
    [SerializeField] private Color batColor;
    [SerializeField] private float summonStun; //how long the boss has to wait after using stun

    [Header("Glare Attack")]
    [SerializeField] private LineRenderer glareLine;
    [SerializeField] private EdgeCollider2D edgeCollider;
    [SerializeField] private float glareStartupOG = .5f; //how long it takes before the dash occurs (after locking onto a target position)
    [SerializeField] private float glareStartup; //the current timer for the dash
    [SerializeField] private float glareDuration = 0.25f;
    [SerializeField] private Color glareColor;
    [SerializeField] private float glareStun; //how long the boss has to wait after using glare
    //private Vector2 targetPos used as well

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
        dashStartup = dashStartupOG;
        player = GameObject.FindWithTag("Player");
        sprite = gameObject.GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (waitTime <= 0)
        {
            if (currentAttack == 0)
            {
                currentAttack = Random.Range(1, 4);
                //currentAttack = 2;
            }
            else if(currentAttack == 1)
            {
                DashAttack();
            }
            else if(currentAttack == 2)
            {
                SummonBats();
            }
            else if(currentAttack == 3)
            {
                GlareAttack();
            }
            else if(currentAttack == 4) //glare attack followup
            {
                currentAttack = Random.Range(1, 3);
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
            speaker.PlayOneShot(basicAttack);
            targetPos = player.transform.position; //the location that the vampire boss will dash to
            if(dashProgress == 1 && Mathf.Abs(transform.position.x - targetPos.x) > 0.25f)
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
                    dashProgress = 0;

                    waitTime = dashStun;
                    currentAttack = 0;
                    sprite.color = colorOG;
                }
            }
        }
    }

    public void SummonBats()
    {
        speaker.PlayOneShot(orbAttack);
        if (!batsSummoned)
        {
            sprite.color = batColor;
            foreach (GameObject batSummon in batSummons)
            {
                batSummon.transform.position = new Vector2(player.transform.position.x + Random.Range(-2f, 2f), batStartingY);
                batSummon.SetActive(true);
            }
            batsSummoned = true;
        }
        else
        {
            foreach (GameObject batSummon in batSummons)
            {
                if(batSummon.transform.position.y > -7f)
                {
                    batSummon.transform.Translate(new Vector2(0, -batSpeed * Time.deltaTime));
                }
                else
                {
                    batsSummoned = false;
                    waitTime = summonStun;
                    currentAttack = 0;
                    sprite.color = colorOG;
                }
            }
        }
    }

    public void GlareAttack()
    {
        if (glareStartup == glareStartupOG)
        {
            targetPos = player.transform.position; //the location that the vampire boss will dash to
            if (Mathf.Abs(transform.position.x - targetPos.x) > 1.5f)
            {
                if (transform.position.x - targetPos.x > 0f)
                {
                    targetPos.x -= 5f;
                }
                else if (transform.position.x - targetPos.x < 0f)
                {
                    targetPos.x += 5f;
                }
            }

            sprite.color = glareColor;
        }

        if (glareStartup > 0)
        {
            glareStartup -= Time.deltaTime;
            if(glareStartup <= 0)
            {
                targetPos.x -= transform.position.x;
                targetPos.y -= transform.position.y;
                glareLine.GetComponent<LineRenderer>().SetPosition(1, targetPos);

                //generates collider for the line
                List<Vector2> edges = new List<Vector2>();

                for(int i = 0; i < glareLine.positionCount; i++)
                {
                    Vector2 point = glareLine.GetPosition(i);
                    edges.Add(new Vector2(point.x, point.y));
                }

                edgeCollider.SetPoints(edges);

                glareLine.gameObject.SetActive(true);
            }
        }
        else
        {
            glareStartup -= Time.deltaTime;
            if(glareStartup <= -glareDuration)
            {
                glareStartup = glareStartupOG;
                glareLine.gameObject.SetActive(false);
                waitTime = glareStun;
                currentAttack = 4;
                sprite.color = colorOG;
            }
        }
    }
}
