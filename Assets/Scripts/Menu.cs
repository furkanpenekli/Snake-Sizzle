using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    public void Multiplayer()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene
        (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Singleplayer()
    {
            UnityEngine.SceneManagement.SceneManager.LoadScene
            (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 2);
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }
}
