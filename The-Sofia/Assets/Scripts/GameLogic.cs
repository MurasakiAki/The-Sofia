using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }



    void StartGame()
    {


    }

    public static void NextLevel()
    {
        string levelName = SceneManager.GetActiveScene().name;
        int levelNumber = int.Parse(levelName.Substring(5)); // extract the number from the string
        levelNumber++; // increase the number
        string newLevelName = levelName.Replace("Level" + (levelNumber - 1), "Level" + levelNumber);
        SceneManager.LoadScene(newLevelName);
    }
    
    public void PreviousLevel()
    {

    }
}
