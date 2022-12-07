using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public GameObject nextLevelScreen;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) == true)
        {
            Time.timeScale = 0;
            nextLevelScreen.SetActive(true);
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) == true)
            {
                Time.timeScale = 0;
                nextLevelScreen.SetActive(true);
            }
        }

    }
}
