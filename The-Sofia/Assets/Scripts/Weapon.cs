using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponTemplate itemTemplate;
    public Sprite itemSprite;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        itemSprite = itemTemplate.icon;

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = itemSprite;

        
    }

}
