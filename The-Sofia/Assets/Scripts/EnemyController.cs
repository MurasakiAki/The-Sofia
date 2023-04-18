using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyController : MonoBehaviour
{

    public EnemyTemplate template;
    private SpriteRenderer spriteRenderer;
    public GameObject player;
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isJumping;
    private bool canAttack = false;
    private float timeSinceLastShot = 0f;
    private float timeToNextAttack;
    public HudScript HudScript;

    public int maxHealth;
    public int currentHealth;

    // Start is called before the first frame update
    void Start()
    {   
        template = Instantiate(template) as EnemyTemplate;

        //set enemy name
        this.gameObject.name = template.enemyName;        

        //set enemy sprite
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = template.sprite;

        //initialize components
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        timeToNextAttack = template.hitRate;

        //set enemy physics
        rb.mass = template.mass;
        if(template.isStatic)
        {
            rb.gravityScale = 0f;
        }

        maxHealth = template.maxHealth;
        currentHealth = maxHealth;
        

        isJumping = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Flip();
        Move();
        
        if (!isJumping)
        {
            StartCoroutine(Jump());
        }
        
        Attack();
        Shoot();
    }

    private void Flip()
    {
        if(player.transform.position.x > this.gameObject.transform.position.x)
        {
            spriteRenderer.flipX = true;
        }else
        {
            spriteRenderer.flipX = false;
        }
    }

    private void Move()
    {
        if(template.isFollowing)
        {
            if(PlayerDetected())
            {
                Vector2 direction = (player.transform.position - transform.position).normalized;
                direction.y = 0;
                transform.Translate(direction * template.enemySpeed * Time.deltaTime);
            }
        }
        
        if(template.isStatic)
        {
            rb.velocity = Vector2.zero;
        }
        
    }

    private bool PlayerDetected()
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

    private void Attack()
    {
        if(canAttack)
        {
            if(timeToNextAttack == template.hitRate || timeToNextAttack <= 0)
            {
                System.Random random = new System.Random();
                int damage = random.Next(template.damageRangeMin, template.damageRangeMax + 1);
                player.GetComponent<Health>().TakeDamage(damage);
                
                timeToNextAttack = template.hitRate;

                if(timeToNextAttack <= 0)
                {
                    timeToNextAttack = template.hitRate;
                }
            }

            timeToNextAttack -= Time.deltaTime;

        }else
        {
            timeToNextAttack = template.hitRate;
        }   
    }

    private void Shoot()
    {
        if(template.isRange)
        {
            if(PlayerDetected() && timeSinceLastShot >= template.hitRate)
            {
                System.Random random = new System.Random();
                int damage = random.Next(template.damageRangeMin, template.damageRangeMax + 1);
                float offsetX = (float)(random.NextDouble() * 0.8f - 0.4f);
                float offsetY = (float)(random.NextDouble() * 0.8f - 0.4f);

                

                Vector2 targetLocation = (player.transform.position - transform.position).normalized;
                targetLocation.x += offsetX;
                targetLocation.y += offsetY;
                GameObject newProjectile = Instantiate(template.projectile, this.gameObject.transform.position, this.gameObject.transform.rotation);

                newProjectile.GetComponent<Projectile>().damage = damage;
                newProjectile.GetComponent<Rigidbody2D>().velocity = targetLocation * newProjectile.GetComponent<Projectile>().projectileSpeed;
                newProjectile.GetComponent<Projectile>().initialPosition = transform.position;

                timeSinceLastShot = 0f;
            }
            timeSinceLastShot += Time.deltaTime;
        }
    }

    private IEnumerator Jump()
    {
        isJumping = true;

        Vector2 direction = (player.transform.position - transform.position).normalized;
        Vector2 targetPosition = (Vector2)transform.position + direction * template.jumpLength;
        targetPosition.y += template.jumpHeight;

        GetComponent<Rigidbody2D>().AddForce((targetPosition - (Vector2)transform.position).normalized * template.jumpForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(template.jumpRate);

        GetComponent<Rigidbody2D>().velocity = Vector2.zero; // set velocity to zero

        isJumping = false;
    }

  
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            canAttack = true;
        }
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            rb.velocity = Vector2.zero;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            canAttack = false;
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
