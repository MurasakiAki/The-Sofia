using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private string type;
    public int maxHealth;
    public int currentHealth;

    // Start is called before the first frame update
    void Awake()
    {
        switch (type)
        {
            case "Player":
                maxHealth = this.gameObject.GetComponent<PlayerController>().max_health;
                break;
            case "Enemy":
                maxHealth = gameObject.GetComponent<EnemyTemplate>().maxHealth;
                break;
            case "Object":
                maxHealth = gameObject.GetComponent<ObjectTemplate>().maxHealth;
                break;
        }
        
        currentHealth = maxHealth;
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
        }        

    }
    //Heal
    public void Heal(int amount)
    {
        currentHealth += amount;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
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
