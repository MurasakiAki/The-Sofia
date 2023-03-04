using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{

    private GameObject player;
    
    void Start()
    {
        player = GameObject.Find("Player");
    }

  private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("´Touch");
        if (other.CompareTag("Player"))
        {
            player.GetComponent<PlayerController>().Die();
        }
    }

}
