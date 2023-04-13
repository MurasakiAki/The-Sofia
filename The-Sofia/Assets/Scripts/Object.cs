using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    public ObjectTemplate objectTemplate;
    private Rigidbody2D rb;
    private Collider2D coll;

    void Awake()
    {
        //set object name
        this.gameObject.name = objectTemplate.objectName;

        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();

        //set object physics
        rb.mass = objectTemplate.mass;
        
        coll.sharedMaterial = objectTemplate.material;

        if(objectTemplate.isStatic)
        {
            rb.gravityScale = 0f;
        }
    }
}
