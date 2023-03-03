using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 5.0f;
    [SerializeField] private float jumpPower = 5.0f;
    [SerializeField] private LayerMask groundLayer;
    int jumps = 0;

    private Rigidbody2D _playerRigidbody;

    private void Start()
    {
        jumps = 0;
        _playerRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Get horizontal input
        float horizontalInput = Input.GetAxisRaw("Horizontal");

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
            if (IsGrounded() || jumps==1)
            {
                Jump();
                jumps += 1;
                if (jumps == 2){ jumps = 0; }
            }
        }
    }

    private void Jump()
    {
        _playerRigidbody.velocity = new Vector2(_playerRigidbody.velocity.x, jumpPower);
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.7f, groundLayer);
        return hit.collider != null && hit.collider.CompareTag("Ground");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("dotek");
        if (other.CompareTag("Trap"))
        {
            Die();
        }
    }

    private void Die()
    {
        // Kód pro smrt postavy
        // Mùžeš napøíklad ukonèit hru, zobrazit nìjakou zprávu, apod.
        Debug.Log("Zemøel jsi");
    }
}
