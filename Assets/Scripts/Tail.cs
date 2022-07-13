using UnityEngine;

public class Tail : MonoBehaviour
{
    public GameOverMenu gameOverMenu;
    public Snake player;

    private void Start()
    {
        gameOverMenu.gameOver = false;
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (player.GetTailSize() > 2)
        {
            if (other.CompareTag("Player"))
            {
                gameOverMenu.gameOver = true;
            }
        }
    }
}
