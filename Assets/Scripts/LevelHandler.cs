using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelHandler : MonoBehaviour
{
    public GameObject settingsUI;
    public GameObject PauseMenuUI;
    public static bool GameIsPaused = false;

    private void Awake()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) | Input.GetKeyDown(KeyCode.P))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void newGame()
    {
        Debug.Log("Starting new game");
        SceneManager.LoadScene(1);
    }

    public void loadGame()
    {

    }

    public void settingsOpen()
    {
        settingsUI.SetActive(true);
        Debug.Log("Settings Open");
    }
    public void settingsClose()
    {
        settingsUI.SetActive(false);
        Debug.Log("Settings Close");
    }
    public void mainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void quitGame()
    {
        Debug.Log("quitting");
        Application.Quit();
    }
    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
