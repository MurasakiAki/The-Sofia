using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public string type;
    public int maxHealth;
    public int currentHealth;
    public HudScript HudScript;

    public AudioSource enemenySmrt;
    public AudioClip enemySmrtaa;

    public AudioSource playerSmrt;
    public AudioClip playerSmrtej;

    // Initialize maxHealth and currentHealth, if player has a save with less current health it will be loaded
    void Start()
    {
        
        switch (type)
        {
            case "Player":
                maxHealth = this.gameObject.GetComponent<PlayerController>().max_health;
                currentHealth = this.gameObject.GetComponent<PlayerController>().current_health;
                if (HudScript != null)
                {
                    HudScript.SetHealth(currentHealth);
                }else
                {
                    return;
                }

                break;
            case "Enemy":
                maxHealth = gameObject.GetComponent<EnemyController>().template.maxHealth;
                break;
            case "Object":
                maxHealth = gameObject.GetComponent<Object>().objectTemplate.maxHealth;
                break;
        }
        
        Debug.Log("Max HP in Health: " + maxHealth);
        if(type != "Player")
        {
            currentHealth = maxHealth;
            Debug.Log(currentHealth);
        }

        
    }

    //TakeDamage
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            Die();
        }

        if(type == "Player")
        {    
            //player taking dmg sound
            this.gameObject.GetComponent<PlayerController>().current_health = currentHealth;
            Debug.Log("Player current health: " + currentHealth);
            if( HudScript != null)
            {
                HudScript.SetHealth(currentHealth); 
            }else
            {
                return;
            }
            
        }
        else 
        {
            //other taking dmg sound
            Debug.Log(gameObject.name + "" + currentHealth);
        }


    }
    
    //Heal
    public void Heal(int amount)
    {
        //heal sound
        currentHealth += amount;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
            this.gameObject.GetComponent<PlayerController>().current_health = currentHealth;
        }
    }
    //Die
    public void Die()
    {
        switch (type)
        {
            case "Player":
                //Player death effect/death screan
                //die sound + anim
                playerSmrt.PlayOneShot(playerSmrtej);

                gameObject.GetComponent<PlayerController>().SetMovability(false);
                gameObject.GetComponent<PlayerAttack>().setCanAttack(false);
                Debug.Log("Player died");
                break;
            case "Enemy":
                //Enemy death effect
                enemenySmrt.PlayOneShot(enemySmrtaa);
                Destroy(gameObject);
                break;
            case "Object":
                //Object death effect
                //object breaking sound + anim/effect
                Destroy(gameObject);
                break;
        }
    }
    
}
