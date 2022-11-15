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
    private bool grounded;
    private bool sliding;

    private int jumpTime;
    private int wallJumpStart;

    // Start is called before the first frame update
    void Start()
    {
        speed = 0f;
        rb = gameObject.GetComponent<Rigidbody2D>();
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
        if(xAxis > 0.1f || xAxis < -0.1f)
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
        if(Input.GetButtonDown("Jump"))
        {
            jumpQueue = 2;
        }
        //Check for if the jump should be shortened
        if (Input.GetButtonUp("Jump"))
        {
            jumpTime = 0;
            rb.velocity = new Vector2(speed * dir, rb.velocity.y * 0.7f);
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
        //I am working on it
        RaycastHit2D boxCheck1 = Physics2D.Raycast(transform.position + new Vector3(0.5f, 0f, 0f), Vector2.down, 0.7f);
        RaycastHit2D boxCheck2 = Physics2D.Raycast(transform.position - new Vector3(0.5f, 0f, 0f), Vector2.down, 0.7f);
        return boxCheck1 || boxCheck2;
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
