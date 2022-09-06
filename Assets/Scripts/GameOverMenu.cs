using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public PauseMenu pauseMenu;
    public GameObject gameOverMenu;

    private Snake[] _snakes;
    
    private void Start()
    {
        _snakes = FindObjectsOfType<Snake>();
    }
    private void Update()
    {
        // 1.snake: dead
        // 2.snake: alive
        
        foreach (var snake in _snakes)
        {
            if (!snake.isAlive)
            {
                gameOverMenu.SetActive(true);
                Time.timeScale = 0f;
                break;
            }
        }
    }
}
