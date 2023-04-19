using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int value;

    void Start()
    {
        System.Random random = new System.Random();
        value = random.Next(0, 26);  
    }
}
