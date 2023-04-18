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
        { "current_health", 100 },
        { "speed" , 8 },
        { "jump_force", 6 },
        { "max_jumps", 1 },
        { "weapon", 0 },
        { "armor", 0 },
        { "damage_range_min", 1 },
        { "damage_range_max", 5 },
        { "attack_range", 30 }, //dont forget to / 100
        { "crit_chance", 20 },
        { "crit_multiplier", 150 }, //dont forget to / 100
        { "coins", 0 },
        { "scene", 1 },

        { "slot0", 0 },
        { "slot1", 0 },
        { "slot2", 0 },
        { "slot3", 0 },
        { "slot4", 0 },
        { "slot5", 0 },
        { "slot6", 0 },
        { "slot7", 0 },
        { "slot8", 0 }
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
    //In saving, have to multiply attack_range by 100
    public static void SaveGame()
    {
        PlayerController player = GameObject.Find("Player").GetComponent<PlayerController>();

        PropertyController.WriteProperty("Assets/Scripts/PlayerProperties.ini","max_health",player.max_health.ToString());
        PropertyController.WriteProperty("Assets/Scripts/PlayerProperties.ini", "current_health", player.current_health.ToString());
        PropertyController.WriteProperty("Assets/Scripts/PlayerProperties.ini", "speed", player.speed.ToString());
        PropertyController.WriteProperty("Assets/Scripts/PlayerProperties.ini", "jump_force", player.jump_force.ToString());
        PropertyController.WriteProperty("Assets/Scripts/PlayerProperties.ini", "max_jumps", player.max_jumps.ToString());
        PropertyController.WriteProperty("Assets/Scripts/PlayerProperties.ini", "weapon", player.weapon.ToString());
        PropertyController.WriteProperty("Assets/Scripts/PlayerProperties.ini", "armor", player.armor.ToString());
        PropertyController.WriteProperty("Assets/Scripts/PlayerProperties.ini", "damage_range_min", player.damage_range_min.ToString());
        PropertyController.WriteProperty("Assets/Scripts/PlayerProperties.ini", "damage_range_max", player.damage_range_max.ToString());
        PropertyController.WriteProperty("Assets/Scripts/PlayerProperties.ini", "attack_range", player.attackRange.ToString());
        PropertyController.WriteProperty("Assets/Scripts/PlayerProperties.ini", "crit_chance", player.crit_chance.ToString());
        PropertyController.WriteProperty("Assets/Scripts/PlayerProperties.ini", "crit_multiplier", player.crit_multiplier.ToString());
        PropertyController.WriteProperty("Assets/Scripts/PlayerProperties.ini", "coins", player.coins.ToString());
        PropertyController.WriteProperty("Assets/Scripts/PlayerProperties.ini", "scene", GetNameNumber(SceneManager.GetActiveScene().name).ToString());//TODO
        PropertyController.WriteProperty("Assets/Scripts/PlayerProperties.ini", "slot0", player.inventory_dict["slot0"].ToString());
        PropertyController.WriteProperty("Assets/Scripts/PlayerProperties.ini", "slot1", player.inventory_dict["slot1"].ToString());
        PropertyController.WriteProperty("Assets/Scripts/PlayerProperties.ini", "slot2", player.inventory_dict["slot2"].ToString());
        PropertyController.WriteProperty("Assets/Scripts/PlayerProperties.ini", "slot3", player.inventory_dict["slot3"].ToString());
        PropertyController.WriteProperty("Assets/Scripts/PlayerProperties.ini", "slot4", player.inventory_dict["slot4"].ToString());
        PropertyController.WriteProperty("Assets/Scripts/PlayerProperties.ini", "slot5", player.inventory_dict["slot5"].ToString());
        PropertyController.WriteProperty("Assets/Scripts/PlayerProperties.ini", "slot6", player.inventory_dict["slot6"].ToString());
        PropertyController.WriteProperty("Assets/Scripts/PlayerProperties.ini", "slot7", player.inventory_dict["slot7"].ToString());
        PropertyController.WriteProperty("Assets/Scripts/PlayerProperties.ini", "slot8", player.inventory_dict["slot8"].ToString());

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

    // TODO ud�lat ukl�d�n� do souboru
    public static void NextLevel()
    {
        string levelName = SceneManager.GetActiveScene().name;
        if(levelName == "Lobby")
        {
            SceneManager.LoadScene("Level1");
        }else
        {
            int levelNumber = GetNameNumber(levelName);
            levelNumber++; // increase the number
            string newLevelName = levelName.Replace("Level" + (levelNumber - 1), "Level" + levelNumber);
            SceneManager.LoadScene(newLevelName);
        }   
    }

    public static int GetNameNumber(string levelName)
    {
        int levelNumber = int.Parse(levelName.Substring(5)); // extract the number from the string

        return levelNumber;
    } 

    public static string ItemType(GameObject item)
    {
        if(item.GetComponent<Weapon>() != null)
        {
            return "Weapon";
        }else 
        {
            return "";
        }
        //if(item.GetComponent<Armor>() != null)
        
    }
}
