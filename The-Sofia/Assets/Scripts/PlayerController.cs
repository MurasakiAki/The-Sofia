using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    [SerializeField] private LayerMask groundLayer;
    
    private Rigidbody2D playerRigidbody;


    private string path = "Assets/Scripts/PlayerProperties.ini"; // Path to player properties file
    private bool is_grounded;
    private bool can_jump;
    private int jumps = 0;
    public int max_jumps;
    public bool can_move;

    //PlayerProperties variables
    public int max_health;
    public int current_health;
    public float speed;
    public float jump_force;
    public int damage_range_min;
    public int damage_range_max;
    public float crit_chance;
    public float crit_multiplier;
    public int coins;


    private void Awake()
    {
        jumps = 0;
        playerRigidbody = GetComponent<Rigidbody2D>();
        can_move = true;
        can_jump = false;

        //Setting speed of player
        //PropertyController.WriteProperty(path, "speed", "10");

        //initializing player properites every time the player is awaken
        max_health = int.Parse(PropertyController.GetValueOfKey(path, "max_health"));
        current_health = max_health;
        speed =  float.Parse(PropertyController.GetValueOfKey(path, "speed"));
        jump_force = float.Parse(PropertyController.GetValueOfKey(path, "jump_force"));
        damage_range_min = int.Parse(PropertyController.GetValueOfKey(path, "damage_range_min"));
        damage_range_max = int.Parse(PropertyController.GetValueOfKey(path, "damage_range_max"));
        crit_chance = float.Parse(PropertyController.GetValueOfKey(path, "crit_chance"));
        crit_multiplier = float.Parse(PropertyController.GetValueOfKey(path, "crit_multiplier"));
        coins = int.Parse(PropertyController.GetValueOfKey(path, "coins"));
        
    }

    private void Update()
    {
        // Get horizontal input
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        // Move the player
        if(can_move)
        {
            // Move the player
            Vector2 movement = new Vector2(horizontalInput * speed, playerRigidbody.velocity.y);
            playerRigidbody.velocity = movement;
        
            // Flip the player's sprite depending on the direction of movement
            if (horizontalInput > 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                
            }
            else if (horizontalInput < 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }

            // Check if the player is grounded
            if (Input.GetButtonDown("Jump"))
            {
                if (jumps < max_jumps && can_jump == true)
                {
                    jumps += 1;
                    Jump();
                }
            }
        }
    }

    private void Jump()
    {
        playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, jump_force);
        Debug.Log("jumps=" + jumps + ",max=" + max_jumps);
        if(jumps == max_jumps){can_jump = false;}
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            is_grounded = true;
            if (is_grounded)
            {
                can_jump = true;
                jumps = 0;
            }
            else
            {
                can_jump = false;
            }
            
            Debug.Log(is_grounded);
        }
        else
        {
            is_grounded = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("NextLevel"))
        {
            GameLogic.NextLevel();
        }
    }

    public void SetMovability(bool set)
    {
        can_move = set;
    }

    public static void TakeDamage(int damage)
    {
        GameObject player = GameObject.Find("Player");
        
        player.GetComponent<PlayerController>().current_health -= damage;
        Debug.Log("Current health: " + player.GetComponent<PlayerController>().current_health);
    }

    public void Die()
    {
        SetMovability(false);
        Debug.Log("Died");
    }
}
