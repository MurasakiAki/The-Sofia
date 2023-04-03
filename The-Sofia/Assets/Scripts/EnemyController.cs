using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [SerializeField] private EnemyTemplate template;
    private SpriteRenderer spriteRenderer;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        //set enemy name
        this.gameObject.name = template.enemyName;

        //set enemy sprite
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = template.sprite;

        //initialize player for sprite rotation
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Flip();
        
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
        
    }
}
