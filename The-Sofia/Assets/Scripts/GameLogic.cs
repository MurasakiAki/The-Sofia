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

    //Starts a new game
    public static void StartGame()
    {
        foreach (KeyValuePair<string, int> entry in base_data)
        {
            string key = entry.Key;
            int value = entry.Value;
            PropertyController.WriteProperty("Assets/Scripts/PlayerProperties.ini",key,value.ToString());
        };
        SceneManager.LoadScene("Lobby");
    }

    public static void ContinueGame()
    {
        int sceneNumber=int.Parse(PropertyController.GetValueOfKey("Assets/Scripts/PlayerProperties.ini", "scene"));
        if (sceneNumber==0)
        {
            SceneManager.LoadScene("Lobby");
        }
        else
        {
            SceneManager.LoadScene("Level"+sceneNumber);
        }
    }

    //TODO
    public static void SaveGame()
    {

    }

    //Respawns player in the lobby, sets default settings
    public static void Respawn()
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

    //Transfer player to another level

    // TODO udìlat ukládání do souboru
    public static void NextLevel()
    {
        string levelName = SceneManager.GetActiveScene().name;
        if(levelName == "Lobby")
        {
            SceneManager.LoadScene("Level1");
        }else
        {
            int levelNumber = GetLevelNumber(levelName);
            levelNumber++; // increase the number
            string newLevelName = levelName.Replace("Level" + (levelNumber - 1), "Level" + levelNumber);
            SceneManager.LoadScene(newLevelName);
        }   
    }

    public static int GetLevelNumber(string levelName)
    {
        int levelNumber = int.Parse(levelName.Substring(5)); // extract the number from the string

        return levelNumber;
    }  
    
}
