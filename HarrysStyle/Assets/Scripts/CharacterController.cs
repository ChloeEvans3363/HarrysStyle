using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private float xAxis;
    private float yAxis;
    private Rigidbody2D rb;
    private int jumpQueue;
    [SerializeField] private float acceleration;
    [SerializeField] private float maxSpeed;
    private float speed;
    private float previousYVel;
    private bool reducedApex;
    private float dir;
    [SerializeField] private bool hasWallJump;
    [SerializeField] private bool hasClimb;
    [SerializeField] private float stun = 0; //used in the boss battle
    private bool grounded;
    private bool sliding;

    private int jumpTime;
    private int wallJumpStart;

    [SerializeField] private int maxHealth = 20;
    private int currentHealth;
    public HealthBar healthBar;
    public bool invincible;

    // Start is called before the first frame update
    void Start()
    {
        speed = 0f;
        rb = gameObject.GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        yAxis = Input.GetAxisRaw("Vertical");
        if (xAxis != 0f && wallJumpStart <= 0)
        {
            dir = Mathf.Round(xAxis);
        }

        //Changes the player's speed
        if((xAxis > 0.1f || xAxis < -0.1f) && stun <= 0)
        {
            speed += acceleration;
            if (speed > maxSpeed)
            {
                speed = maxSpeed;
            }
        }
        //Slows the player to a stop
        else
        {
            if(speed > 0f)
            {
                speed -= acceleration;
            }
            else if (speed < 0f)
            {
                speed = 0f;
            }
        }
        if(Input.GetButtonDown("Jump") && stun <= 0)
        {
            jumpQueue = 2;
        }
        //Check for if the jump should be shortened
        if (Input.GetButtonUp("Jump") && stun <= 0)
        {
            jumpTime = 0;
            rb.velocity = new Vector2(speed * dir, rb.velocity.y * 0.7f);
        }

        //removes stun over time
        stun -= Time.deltaTime;

        if (invincible)
        {
            Debug.Log("i am anime");
        }
    }

    private void FixedUpdate()
    {
        //Used for wall jumping. Not applicable in this project.
        /*if (!hasWallJump || xAxis == 0f)
        {
            grounded = CheckIfGrounded();
        }
        else
        {
            grounded = CheckIfGrounded() || CheckIfNearWall();
        }*/

        //Check if the player is on the ground
        grounded = CheckIfGrounded();
        //Jumps if that input has been recieved
        if (jumpQueue > 0)
        {
            if(sliding)
            {
                wallJumpStart = 9;
                dir *= -1f;
            }
            //Debug.Log(check.collider.gameObject.name);
            if(grounded)
            {
                jumpTime = 7;
                jumpQueue = 0;
                rb.gravityScale = 1.75f;
                reducedApex = false;
                rb.velocity = new Vector2(speed, 15f);
            }
        } else
        {
            rb.velocity = new Vector2(speed * dir, rb.velocity.y);
        }
        //
        if(jumpTime <= 0 || rb.velocity.y < 0f && !sliding)
        {
            if (rb.velocity.y > 0 && reducedApex == false)
            {
                rb.velocity = new Vector2(speed * dir, rb.velocity.y * 0.7f);
                reducedApex = true;
            }
            rb.gravityScale = 2.5f;
        } else
        {
            jumpTime--;
        }
        //Lowers the jump queue timer
        if(jumpQueue > 0)
        {
            jumpQueue--;
        }
        if(wallJumpStart > 0)
        {
            wallJumpStart--;
        }
        previousYVel = rb.velocity.y;
    }

    /// <summary>
    /// Checks if the player is on the ground
    /// </summary>
    /// <returns></returns>
    private bool CheckIfGrounded()
    {
        //The overly complicated "am I on the ground" detection
        RaycastHit2D boxCheck;
        Collider2D sideHit = Physics2D.OverlapBox(transform.position, new Vector2(0.8f, 1f), 0f, 7);
        //Decides on the scale of the boxcast depending on whether or not a larger cast would hit a nearby wall
        if (sideHit == null)
        {
            boxCheck = Physics2D.BoxCast(transform.position, new Vector2(0.8f, 1.0f), 0f, new Vector2(0f, -1f), 0.1f, 7);
        }
        else
        {
            boxCheck = Physics2D.BoxCast(transform.position, new Vector2(0.5f, 1.0f), 0f, new Vector2(0f, -1f), 0.1f, 7);
        }
        return boxCheck;
    }

    public void Damage(int damage)
    {
        if(currentHealth > 0 && !invincible)
        {
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);
            invincible = true;
        }
        else
        {
            Death();
        }
    }

    public void Stun(float stunDuration)
    {
        stun = stunDuration;
    }

    private void Death()
    {
        Debug.Log("You Died");
    }

    /// <summary>
    /// Checks if the player is next to a wall (used for wall jumping)
    /// </summary>
    /// <returns></returns>
    /*private bool CheckIfNearWall()
    {
        RaycastHit2D checkForWall = Physics2D.Raycast(transform.position, new Vector2(dir, 0f), 0.42f, 9);

        if(checkForWall)
        {
            if(hasClimb)
            {
                rb.velocity = new Vector2(0f, yAxis * 4.5f);
            }
            else if(rb.velocity.y < -4.5f)
            {
                rb.velocity = new Vector2(rb.velocity.x, -4.5f);
            }
            sliding = true;
            return true;
        }
        else
        {
            sliding = false;
            return false;
        }
    }*/
}
