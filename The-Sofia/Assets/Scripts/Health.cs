using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private string type;
    public int maxHealth;
    public int currentHealth;
    public HudScript HudScript; 

    // Initialize maxHealth and currentHealth, if player has a save with less current health it will be loaded
    void Start()
    {
        
        switch (type)
        {
            case "Player":
                maxHealth = this.gameObject.GetComponent<PlayerController>().max_health;
                currentHealth = this.gameObject.GetComponent<PlayerController>().current_health;
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
        }

        HudScript.SetHealth(currentHealth);
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
            this.gameObject.GetComponent<PlayerController>().current_health = currentHealth;
            Debug.Log("Player current health: " + currentHealth);
        }else 
        {
            Debug.Log(gameObject.name + "" + currentHealth);
        }

        HudScript.SetHealth(currentHealth);

    }
    
    //Heal
    public void Heal(int amount)
    {
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
                Debug.Log("Player died");
                break;
            case "Enemy":
                //Enemy death effect
                Destroy(gameObject);
                break;
            case "Object":
                //Object death effect
                Destroy(gameObject);
                break;
        }
    }
    
}
