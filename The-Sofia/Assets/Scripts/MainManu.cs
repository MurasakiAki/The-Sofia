using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManu : MonoBehaviour
{
    public void PlayGame()
    {
        new GameLogic().StartGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
