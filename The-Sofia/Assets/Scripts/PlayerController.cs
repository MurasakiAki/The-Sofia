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
    public float crit_chance;
    public int crit_multiplier;
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
        current_health = int.Parse(PropertyController.GetValueOfKey(path, "current_health"));
        speed =  float.Parse(PropertyController.GetValueOfKey(path, "speed"));
        jump_force = float.Parse(PropertyController.GetValueOfKey(path, "jump_force"));
        max_jumps = int.Parse(PropertyController.GetValueOfKey(path, "max_jumps"));
        damage_range_min = int.Parse(PropertyController.GetValueOfKey(path, "damage_range_min"));
        damage_range_max = int.Parse(PropertyController.GetValueOfKey(path, "damage_range_max"));
        attackRange = float.Parse(PropertyController.GetValueOfKey(path, "attackRange")) / 100;
        crit_chance = float.Parse(PropertyController.GetValueOfKey(path, "crit_chance"));
        crit_multiplier = int.Parse(PropertyController.GetValueOfKey(path, "crit_multiplier"));
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
