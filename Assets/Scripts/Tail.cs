using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tail : MonoBehaviour
{
    public GameOverMenu gameOverMenu;
    public Player player;

    private void Start()
    {
        gameOverMenu.gameOver = false;
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (player.tails.Count > 2)
        {
            if (other.CompareTag("Player"))
            {
                gameOverMenu.gameOver = true;
            }
        }
    }
}
