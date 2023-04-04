using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage;
    public float projectileSpeed;

    public float distance = 10f;

    public Vector2 initialPosition;
    public float traveledDistance;

    void Awake()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
       
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
