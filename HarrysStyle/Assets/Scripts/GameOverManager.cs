using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("restart");
    }

    public void MenuButton()
    {
        SceneManager.LoadScene("MainMenu");
        Debug.Log("menu");
    }
}
