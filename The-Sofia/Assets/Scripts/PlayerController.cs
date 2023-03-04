using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 5.0f;
    [SerializeField] private float jumpPower = 5.0f;
    [SerializeField] private LayerMask groundLayer;
    private bool can_jump;
    private int jumps = 0;
    public int max_jumps;

    public bool can_move;

    private Rigidbody2D _playerRigidbody;

    private void Start()
    {
        jumps = 0;
        _playerRigidbody = GetComponent<Rigidbody2D>();
        can_move = true;
    }

    private void Update()
    {
        if (IsGrounded())
        {
            can_jump = true;
            jumps = 0;
        }
        else{
            can_jump = false;
        }

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
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (horizontalInput < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }

            // Check if the player is grounded
            if (Input.GetButtonDown("Jump"))
            {
                jumps += 1;
                if (jumps<max_jumps)
                {
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

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.7f, groundLayer);
        return hit.collider != null && hit.collider.CompareTag("Ground");
    }

    public void set_movability(bool set)
    {
        can_move = set;
    }

    public void Die()
    {
        set_movability(false);
        Debug.Log("It seems, that you have been impaled by nasty pointy sticks.");

    }
}
