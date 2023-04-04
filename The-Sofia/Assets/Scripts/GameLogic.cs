using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{

    public static Dictionary<string, double> base_data = new Dictionary<string, double>()
    {
        { "max_health", 100 },
        { "current_health", 100 },
        { "speed" , 8},
        { "jump_force", 6},
        { "damage_range_min", 1},
        { "damage_range_max", 5},
        { "crit_chance", 20},
        { "crit_multiplier", 150},
        { "coins", 0},
        {"scena",1 },
    };

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }



    void StartGame()
    {
        foreach (KeyValuePair<string, double> entry in base_data)
        {
            string key = entry.Key;
            double value = entry.Value;
            PropertyController.WriteProperty("Assets/Scripts/PlayerProperties.ini",key,value.ToString());
        };
        player.GetComponent<PlayerController>().hp=100;
        player.GetComponent<PlayerController>().max_hp = 100;
        player.GetComponent<PlayerController>().playerSpeed = 8;

    }

    void ContinueGame()
    {

    }

    void Respawn()
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
    
}
