using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;

    [SerializeField] public static bool isPaused;

    public static bool InGame = false;

    void Start()
    {
        InGame = false;
    }

    private void Update()
    {
        if (InGame = true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isPaused = !isPaused;
            }

            if (isPaused)
            {
                ActivateMenu();
            }
            else
            {
                DeacivateMenu();
            }
        }
    }
    void ActivateMenu()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
        Cursor.lockState = CursorLockMode.None;
        pauseMenuUI.SetActive(true);
    }
    public void DeacivateMenu()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        AudioListener.pause = false;
        Cursor.lockState = CursorLockMode.Locked;
        isPaused = false;
    }
}
