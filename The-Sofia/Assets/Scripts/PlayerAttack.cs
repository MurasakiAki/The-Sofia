using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private LayerMask hitableLayer;

    private GameObject player;
    public Transform attackPoint;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

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
        //if has method take damage

        foreach (Collider2D enemy in hitEnemies)
        {
            Health health = enemy.GetComponent<Health>();
            if (health != null)
            {
                System.Random random = new System.Random();
                int damage = random.Next(player.GetComponent<PlayerController>().damage_range_min, player.GetComponent<PlayerController>().damage_range_max + 1);
                if(random.Next(0, 101) <= player.GetComponent<PlayerController>().crit_chance)
                {
                    damage *= player.GetComponent<PlayerController>().crit_multiplier;
                }
                health.TakeDamage(damage);
            }
        }

    }
}
