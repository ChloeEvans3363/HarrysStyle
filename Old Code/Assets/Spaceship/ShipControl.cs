using UnityEngine;

public class ShipControl : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject antiRock;
    public GameObject fireShot;
    public float speed = 10f;


    public void purSeek(float theta, bool fire, bool quick)
    {
        Vector3 spawnPos = transform.position;
        spawnPos += new Vector3(0, 0, 0);
        GameObject prefab = antiRock;
        if (fire)
        {
            prefab = fireShot;
        }

        if (quick)
        {
            AntiRock antrock = prefab.GetComponent<AntiRock>();
            if (antrock != null)
            {
                antrock.speed = 20f;
            }
        }
        else
        {
            AntiRock antrock = prefab.GetComponent<AntiRock>();
            if (antrock != null)
            {
                antrock.speed = 10f;
            }
        }

        gameManager.AddAntiRockToList(Instantiate(prefab, spawnPos, Quaternion.Euler(0, 0, theta)));
    }

    //Exercise 15 requires that you add the code to Update() to have the "vertical axis" keys (Up/Down arrow, W/S key) translate ship in +/- y direction
    //See Exercise 9 Rocket + AntiRocks - Rocsk project for code that handles the "horizontal axis" keys translation in +/- x direction

    private void Update()
    {
        float yInput = Input.GetAxis("Vertical");
        transform.Translate(-yInput * speed * Time.deltaTime, 0f, 0f);

        Vector3 position = transform.position;
        position.y = Mathf.Clamp(position.y, -5.3f, 5.3f);
        transform.position = position;

        if (gameManager.GetComponent<GameManager>().bossBattle)
        {
            float xInput = Input.GetAxis("Horizontal");
            transform.Translate(0f, xInput * speed * Time.deltaTime, 0f);

            position = transform.position;
            position.x = Mathf.Clamp(position.x, -12.5f, 12.5f);
            transform.position = position;
        }
    }
}