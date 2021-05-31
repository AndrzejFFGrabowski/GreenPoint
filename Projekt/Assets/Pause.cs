using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Pause : MonoBehaviour
{
    public bool gameIsPaused = false;
    public GameObject Menu;
    public GameObject Exit;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            gameIsPaused = !gameIsPaused;
            PauseGame();
        }
    }
    void PauseGame()
    {
        if (gameIsPaused)
        {
            Menu.SetActive(true);
            Exit.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            Menu.SetActive(false);
            Exit.SetActive(false);
            Time.timeScale = 1;
        }
    }
    public void MenuOnClick()
    {
        gameIsPaused = false;
        SceneManager.LoadScene("Menu");
    }
    public void ExitOnClick()
    {
        gameIsPaused = false;
        Application.Quit();
    }
}
