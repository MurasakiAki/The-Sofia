using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [SerializeField] private EnemyTemplate template;
    private SpriteRenderer spriteRenderer;
    public GameObject player;
    private Rigidbody2D rb;

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
    }

    // Update is called once per frame
    void Update()
    {
        Flip();
        Move();
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
        if(PlayerDetected())
        {
            Vector2 targetLocation = (player.transform.position - transform.position).normalized;
            GameObject newProjectile = Instantiate(template.projectile, transform.position, transform.rotation);
            newProjectile.GetComponent<Rigidbody2D>().velocity = targetLocation * newProjectile.GetComponent<Projectile>().projectileSpeed;
        }
    }
}
