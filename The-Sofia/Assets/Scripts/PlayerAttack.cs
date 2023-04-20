using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private LayerMask hitableLayer;

    public GameObject player;
    public Transform attackPoint;

    // Update is called once per frame
    void Update()
    {
        //Attacking
        if(Input.GetKeyDown(KeyCode.E))
        {
            Attack();
        }
    }

public void Attack()
{
    //Insert Attack animation

    //Detecting enemies
    Collider2D[] hitEntities = Physics2D.OverlapCircleAll(attackPoint.position, player.GetComponent<PlayerController>().attackRange / 100, hitableLayer);

    Debug.Log(hitEntities.Length + "Enemies detected");
    foreach (Collider2D entity in hitEntities)
    {
        Health health = entity.GetComponent<Health>();
        if (health != null)
        {
            System.Random random = new System.Random();
            int damage = random.Next(player.GetComponent<PlayerController>().damage_range_min, player.GetComponent<PlayerController>().damage_range_max + 1);
            //calculating crit chance and applying multiplier
            if(random.Next(0, 101) <= player.GetComponent<PlayerController>().crit_chance)
            {   
                Debug.Log("Critical damage hit! Ddamage is x" + player.GetComponent<PlayerController>().crit_multiplier);
                damage = Convert.ToInt32(damage * player.GetComponent<PlayerController>().crit_multiplier / 100);
            }
            Debug.Log("Dealt " + damage + " damage to " + entity.name);
            health.TakeDamage(damage);
        }
    }
    
}

    private void OnDrawGizmos()
    {   
        DrawGizmos();
    }   

    private void OnDrawGizmosSelected()
    {
        DrawGizmos();
    }

    private void DrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, player.GetComponent<PlayerController>().attackRange / 100);

    }
}
