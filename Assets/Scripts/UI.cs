using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI : MonoBehaviour
{
    public Movement movement;
    public Snake snake;
    public GameOverMenu gameOverMenu;
    public Text speedText;
    public Text tailText;
    void Update()
    {

        speedText.text = movement.speed.ToString();
        tailText.text = snake.length.ToString();
    }
}
