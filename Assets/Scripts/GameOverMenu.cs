using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public bool gameOver;
    public PauseMenu pauseMenu;
    public GameObject gameOverMenu;

    private void Start()
    {
        gameOver = false;
    }

    void Update()
    {
        if (gameOver)
        {
            gameOverMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            gameOverMenu.SetActive(false);
            if (pauseMenu.paused == false)
            {
                Time.timeScale = 1f;   
            }
        }
    }
}
