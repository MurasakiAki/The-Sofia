using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public float playerSpeed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    
    private string path = "Assets/Scripts/PlayerProperties.ini"; // Path to player properties file
    private bool is_grounded;
    private bool can_jump;
    private int jumps = 0;
    public int max_jumps;
    public int hp;
    public int max_hp;

    public bool can_move;

    private Rigidbody2D _playerRigidbody;

    private void Start()
    {
        jumps = 0;
        _playerRigidbody = GetComponent<Rigidbody2D>();
        can_move = true;
        can_jump = false;

        //Setting speed of player
        //PropertyController.WriteProperty(path, "speed", "10");

        playerSpeed =  float.Parse(PropertyController.GetValueOfKey(path, "speed"));
        jumpPower = float.Parse(PropertyController.GetValueOfKey(path, "jump_force"));
    }

    private void Update()
    {
        // Get horizontal input
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        // Move the player
        if(can_move)
        {
            // Move the player
            Vector2 movement = new Vector2(horizontalInput * playerSpeed, _playerRigidbody.velocity.y);
            _playerRigidbody.velocity = movement;
        
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
        _playerRigidbody.velocity = new Vector2(_playerRigidbody.velocity.x, jumpPower);
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

    public void set_movability(bool set)
    {
        can_move = set;
    }

    public void Die()
    {
        set_movability(false);
        Debug.Log("Died");
    }
}
