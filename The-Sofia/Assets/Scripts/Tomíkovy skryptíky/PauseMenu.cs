using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    
    public static bool GameEsc = false;

    public GameObject pauseMenu; 
    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameEsc )
            {
                Resume(); 
            }else
            {
                Pause(); 
            }
        }

    }

    public void Resume()
    {
        pauseMenu.SetActive(false);

        Time.timeScale = 1f;
        GameEsc = false;

    }

    void Pause()
    {
        pauseMenu.SetActive(true);

        Time.timeScale = 0f;
        GameEsc = true;
    }

    public void ToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0); 
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    //uklada to hru
    public void Slave()
    {
        GameLogic.SaveGame();
    }
}
