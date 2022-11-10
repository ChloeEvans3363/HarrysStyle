using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    private bool movingRight = true;
    public Transform groundDetection;
    private Vector2 viewDirection;
    private GameObject player;
    public Rigidbody2D rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");

        if (!rigidbody)
        {
            rigidbody = GetComponent<Rigidbody2D>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Indicates if the enemy can view the player
        // If the enemy can view the player then it goes into attack mode
        RaycastHit2D view = Physics2D.Raycast(groundDetection.position, viewDirection, 10f);
        if(view.collider == true && view.collider.gameObject.GetComponent<CharacterController>())
        {
            Attack();
        }
        else
        {
            Patrolling();
        }
    }

    /// <summary>
    /// Patrols the enemy back and forth across the block
    /// </summary>
    void Patrolling()
    {
        // Moves the enemy to the right given a speed
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        // Raycast the view from below the enemy
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, 2f, LayerMask.GetMask("Ground"));

        // Checks if the enemy view can detect any collider below it
        // If it cannot detect anything then the enemy will turn
        if(groundInfo.collider == false)
        {
            // Turns the enemy left it was going right
            if(movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
                viewDirection = Vector2.left;
            }

            // Turns the enemy right if it was going left
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
                viewDirection = Vector2.right;
            }
        }
    }

    /// <summary>
    /// Enemy attack
    /// </summary>
    void Attack()
    {
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, 2f, LayerMask.GetMask("Ground"));

        if (Vector2.Distance(transform.position, player.transform.position) >= 2 && groundInfo.collider == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
        else
        {
            rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
            Debug.Log("Attack");
        }
    }
}
