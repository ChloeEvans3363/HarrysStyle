using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public GameObject nextLevelScreen;
    bool trigger = false;
    void Update()
    {
        Debug.Log(trigger);
        if ((Input.GetKeyDown(KeyCode.UpArrow) == true  || Input.GetKeyDown(KeyCode.W))
            == true && trigger)
        {
            Time.timeScale = 0;
            nextLevelScreen.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        trigger = true;

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        trigger = false;
    }
}
