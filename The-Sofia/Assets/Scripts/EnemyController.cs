using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyController : MonoBehaviour
{

    [SerializeField] private EnemyTemplate template;
    private SpriteRenderer spriteRenderer;
    public GameObject player;
    private Rigidbody2D rb;

    private float timeSinceLastShot = 0f;

    // Start is called before the first frame update
    void Start()
    {
        //set enemy name
        this.gameObject.name = template.enemyName;

        //set enemy sprite
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = template.sprite;

        //initialize components
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();

        //set enemy physics
        rb.mass = template.mass;
    }

    // Update is called once per frame
    void Update()
    {
        Flip();
        Move();
        Shoot();
    }

    void Flip()
    {
        if(player.transform.position.x > this.gameObject.transform.position.x)
        {
            spriteRenderer.flipX = true;
        }else
        {
            spriteRenderer.flipX = false;
        }
    }

    void Move()
    {
        if(template.isStatic == false)
        {
            if(PlayerDetected())
            {
                Vector2 direction = (player.transform.position - transform.position).normalized;
                transform.Translate(direction * template.enemySpeed * Time.deltaTime);
            }
        }
    }

    public bool PlayerDetected()
    {
        if (Vector2.Distance(transform.position, player.transform.position) <= template.playerDetectionRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void Shoot()
    {
        if(template.isRange)
        {
            if(PlayerDetected() && timeSinceLastShot >= template.hitRate)
            {
                System.Random random = new System.Random();
                int damage = random.Next(template.damageRangeMin, template.damageRangeMax + 1);

                Vector2 targetLocation = (player.transform.position - transform.position).normalized;
                GameObject newProjectile = Instantiate(template.projectile, this.gameObject.transform.position, this.gameObject.transform.rotation);

                newProjectile.GetComponent<Projectile>().damage = damage;
                newProjectile.GetComponent<Rigidbody2D>().velocity = targetLocation * newProjectile.GetComponent<Projectile>().projectileSpeed;
                newProjectile.GetComponent<Projectile>().initialPosition = transform.position;
                newProjectile.GetComponent<Projectile>().distance = 10f;


                // Destroy the bullet if it has traveled the specified distance
                StartCoroutine(DestroyAfterDistance(newProjectile));

                timeSinceLastShot = 0f;
            }
            timeSinceLastShot += Time.deltaTime;
        }
    }

    IEnumerator DestroyAfterDistance(GameObject projectile)
    {
        float traveledDistance = 0f;
        while (traveledDistance < projectile.GetComponent<Projectile>().distance)
        {
            traveledDistance = Vector2.Distance(projectile.GetComponent<Projectile>().initialPosition, projectile.transform.position);
            yield return null;
        }
        if(projectile != null)
        {
            Destroy(projectile);
        }   
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            System.Random random = new System.Random();
            int damage = random.Next(template.damageRangeMin, template.damageRangeMax + 1);
            PlayerController.TakeDamage(damage);
        }
    }
}
