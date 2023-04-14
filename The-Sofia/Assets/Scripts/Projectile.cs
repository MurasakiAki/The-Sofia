using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage;
    public float projectileSpeed;

    public float distance = 15f;

    public Vector2 initialPosition;
    public float traveledDistance;

    void Awake()
    {
        initialPosition = transform.position;
    }

    void FixedUpdate()
    {   
        float distanceFromInit = this.gameObject.transform.position.x - initialPosition.x;
        if(Mathf.Abs(distanceFromInit) >= distance)
        {
            Destroy(this.gameObject);
        }    
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Ground") || other.CompareTag("Walls"))
        {
            Destroy(gameObject);
        }
        else if(other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Health>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
