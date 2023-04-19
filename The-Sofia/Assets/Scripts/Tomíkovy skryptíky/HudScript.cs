using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HudScript : MonoBehaviour
{

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI coinText;
    public void SetHealth(int health)
    {
        healthText.text = health.ToString() + "/100";
    }

    public void SetCoin(int coin)
    {
        healthText.text = coin.ToString(); 
    }

}
