using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainManu : MonoBehaviour
{

    public void PlayGame()
    {
        GameLogic.StartGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
