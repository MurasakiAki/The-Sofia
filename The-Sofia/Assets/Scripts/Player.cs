using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed = 5f; // The player's movement speed


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal"); // Get horizontal input from keyboard or controller
        float vertical = Input.GetAxis("Vertical"); // Get vertical input from keyboard or controller

        // Calculate the movement vector based on the input and the speed
        Vector2 movement = new Vector2(horizontal, vertical) * speed * Time.deltaTime;

        // Move the player's position by the calculated movement vector
        transform.position += new Vector3(movement.x, movement.y, 0);
    }
}
