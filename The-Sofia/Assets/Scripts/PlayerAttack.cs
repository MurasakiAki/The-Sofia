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
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, player.GetComponent<PlayerController>().attackRange, hitableLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            Health health = enemy.GetComponent<Health>();
            if (health != null)
            {
                System.Random random = new System.Random();
                int damage = random.Next(player.GetComponent<PlayerController>().damage_range_min, player.GetComponent<PlayerController>().damage_range_max + 1);
                //calculating crit chance and applying multiplier
                if(random.Next(0, 101) <= player.GetComponent<PlayerController>().crit_chance)
                {
                    damage = Convert.ToInt32(damage * player.GetComponent<PlayerController>().crit_multiplier);
                }
                health.TakeDamage(damage);
            }
        }

    }

    private void OnDrawGizmos()
    {
        // Draw a sphere around the attack point in the editor for debugging purposes
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, 0.5f); //player.GetComponent<PlayerController>().attackRange
    }
}
