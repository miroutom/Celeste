using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool PauseGame;
    public GameObject PauseMenuObject;
    public Text ScoreText;

    public void Resume()
    {
        PauseMenuObject.SetActive(false);
        Color color = ScoreText.color;
        color.a = 0f;
        ScoreText.color = color;
        Time.timeScale = 1f;
        PauseGame = false;
    }

    public void Pause()
    {
        PauseMenuObject.SetActive(true);
        Color color = ScoreText.color;
        color.a = 1f;
        ScoreText.color = color;
        Time.timeScale = 0f;
        PauseGame = true;

    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PauseGame)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
}
