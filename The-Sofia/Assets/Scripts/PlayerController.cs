using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 5.0f;
    [SerializeField] private float jumpPower = 5.0f;
    
    private Rigidbody2D _playerRigidbody;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // Get horizontal input
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        
      
        // Move the player
        Vector2 movement = new Vector2(horizontalInput * movementSpeed, rb.velocity.y);
        rb.velocity = movement;

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
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundRaycastDistance, groundLayer);
        isGrounded = hit.collider != null;

        Debug.Log(Input.GetButtonDown("Jump"));

        // Jump if the player is grounded and the jump button is pressed
        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
    private void Update()
    {
        MovePlayer();

        if (Input.GetButton("Jump") && IsGrounded())
            Jump();
    }
    private void MovePlayer()
    {
        var horizontalInput = Input.GetAxisRaw("Horizontal");
        _playerRigidbody.velocity = new Vector2(horizontalInput * playerSpeed, _playerRigidbody.velocity.y);
    }
    private void Jump() => _playerRigidbody.velocity = new Vector2( 0, jumpPower);

    private bool IsGrounded()
    {
        var groundCheck = Physics2D.Raycast(transform.position, Vector2.down, 0.7f);
        return groundCheck.collider != null && groundCheck.collider.CompareTag("Ground");
    }

   
}
