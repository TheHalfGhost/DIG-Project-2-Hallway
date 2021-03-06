using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    

    public void Playgame()
    {
        SceneManager.LoadScene("Level 1");
        PauseMenu.InGame = true;
    }

    public void Quitgame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
        PauseMenu.InGame = false;
        PauseMenu.isPaused = false;
    }
    void Update()
    {
        Cursor.lockState = CursorLockMode.None;
    }
}

