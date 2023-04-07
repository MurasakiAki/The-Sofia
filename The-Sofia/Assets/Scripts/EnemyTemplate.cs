using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Create New Enemy")]

public class EnemyTemplate : ScriptableObject
{
    //Name
    public string enemyName;

    //Movement
    public bool isStatic;
    public bool isFlying;
    public bool isRange;
    public int enemySpeed;

    //Health
    public int maxHealth;

    //Physics
    public int mass;

    //Damage
    public int damageRangeMin;
    public int damageRangeMax;
    public float hitRate;
    public float playerDetectionRange;

    //Visuals
    public Sprite sprite;
    public GameObject projectile;

}
