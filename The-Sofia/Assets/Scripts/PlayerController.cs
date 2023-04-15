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
    public bool can_move;

    //PlayerProperties variables
    public int max_health;
    public int current_health;
    public float speed;
    public float jump_force;
    public int max_jumps;
    public int damage_range_min;
    public int damage_range_max;
    public float attackRange;
    public int crit_chance;
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
        string maxHealthString = PropertyController.GetValueOfKey(path, "max_health");
        if (maxHealthString != null)
        {
            max_health = int.Parse(maxHealthString);
            Debug.Log("Max HP: " + max_health);
        }
        else
        {
            Debug.LogError("Failed to read 'max_health' property from file");
        }

        string currentHealthString = PropertyController.GetValueOfKey(path, "current_health");
        if (currentHealthString != null)
        {
            current_health = int.Parse(currentHealthString);
        }
        else
        {
            Debug.LogError("Failed to read 'current_health' property from file");
        }

        string speedString = PropertyController.GetValueOfKey(path, "speed");
        if (speedString != null)
        {
            speed = float.Parse(speedString);
        }
        else
        {
            Debug.LogError("Failed to read 'speed' property from file");
        }

        string jumpForceString = PropertyController.GetValueOfKey(path, "jump_force");
        if (jumpForceString != null)
        {
            jump_force = float.Parse(jumpForceString);
        }
        else
        {   
            Debug.LogError("Failed to read 'jump_force' property from file");
        }

        string maxJumpsString = PropertyController.GetValueOfKey(path, "max_jumps");
        if (maxJumpsString != null)
        {
            max_jumps = int.Parse(maxJumpsString);
        }
        else
        {
            Debug.LogError("Failed to read 'max_jumps' property from file");
        }

        string damageRangeMinString = PropertyController.GetValueOfKey(path, "damage_range_min");
        if (damageRangeMinString != null)
        {
            damage_range_min = int.Parse(damageRangeMinString);
        }
        else
        {
            Debug.LogError("Failed to read 'damage_range_min' property from file");
        }

        string damageRangeMaxString = PropertyController.GetValueOfKey(path, "damage_range_max");
        if (damageRangeMaxString != null)
        {
            damage_range_max = int.Parse(damageRangeMaxString);
        }
        else
        {
            Debug.LogError("Failed to read 'damage_range_max' property from file");
        }

        string attackRangeString = PropertyController.GetValueOfKey(path, "attack_range");
        if (attackRangeString != null)
        {
            attackRange = float.Parse(attackRangeString);
        }
        else
        {
            Debug.LogError("Failed to read 'attackRange' property from file");
        }

        string critChanceString = PropertyController.GetValueOfKey(path, "crit_chance");
        if (critChanceString != null)
        {
            crit_chance = int.Parse(critChanceString);
        }
        else
        {
            Debug.LogError("Failed to read 'crit_chance' property from file");
        }

        string critMultiplierString = PropertyController.GetValueOfKey(path, "crit_multiplier");
        if (critMultiplierString != null)
        {
            crit_multiplier = float.Parse(critMultiplierString) / 100;
        }
        else
        {
            Debug.LogError("Failed to read 'crit_multiplier' property from file");
        }

        string coinsString = PropertyController.GetValueOfKey(path, "coins");
        if (coinsString != null)
        {
            coins = int.Parse(coinsString);
        }
        else
        {
            Debug.LogError("Failed to read 'coins' property from file");
        }
        
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

            if (is_grounded)
            {
                can_jump = true;
                jumps = 0;
            }
            else
            {
                can_jump = false;
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
        if(jumps == max_jumps){can_jump = false;}
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            is_grounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
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

}
