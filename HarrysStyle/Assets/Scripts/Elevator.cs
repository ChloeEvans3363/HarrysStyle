using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public GameObject nextLevelScreen;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Time.timeScale = 0;
        nextLevelScreen.SetActive(true);
    }
}
