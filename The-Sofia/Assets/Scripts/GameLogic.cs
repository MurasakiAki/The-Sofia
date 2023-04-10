using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{

    public static Dictionary<string, int> base_data = new Dictionary<string, int>()
    {
        { "max_health", 100 },
        { "speed" , 8},
        { "jump_force", 6},
        { "max_jumps", 1},
        { "damage_range_min", 1},
        { "damage_range_max", 5},
        { "crit_chance", 20},
        { "crit_multiplier", 150},
        { "coins", 0},
        { "scene",1 },
    };

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }



    public static void StartGame()
    {
        SceneManager.LoadScene("Level" + base_data["scene"].ToString());
        foreach (KeyValuePair<string, int> entry in base_data)
        {
            string key = entry.Key;
            int value = entry.Value;
            PropertyController.WriteProperty("Assets/Scripts/PlayerProperties.ini",key,value.ToString());
        };
    }

    void ContinueGame()
    {
        
    }

    void Respawn()
    {
        foreach (KeyValuePair<string, int> entry in base_data)
        {
            string key = entry.Key;
            int value = entry.Value;
            if (!new string[] { "scene", "coins" }.Contains(key))
            {
                PropertyController.WriteProperty("Assets/Scripts/PlayerProperties.ini", key, value.ToString());
            };
        };
        SceneManager.LoadScene("Lobby");
    }

    void savePlayerPropperty()
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
