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
        if (other.CompareTag("Player"))
        {
            player.GetComponent<Health>().Die();
        }
    }

}
