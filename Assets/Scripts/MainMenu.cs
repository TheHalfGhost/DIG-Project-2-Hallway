﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    

    public void Playgame()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void Quitgame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
