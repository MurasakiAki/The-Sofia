using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Create New Enemy")]

public class EnemyTemplate : ScriptableObject
{
    //Name
    public string enemyName; //enemy name

    //Movement
    public bool isStatic; //won't move
    public bool isFollowing; //follows player
    public bool isFlying; //will be flying
    public bool isRange; //the enemy will be shooting projectiles
    public bool isJumping; //if enemy will be jumping, for instance spider
    public float enemySpeed; //speed of enemy
    public float jumpForce; //force of enemy jump
    public float jumpHeight; //height of enemy jump
    public float jumpLength; //length of enemy jump
    public float jumpRate; // rate jump rate

    //Health
    public int maxHealth; //max health of player

    //Physics
    public int mass; //mass of enemy, can influence enemy knockback

    //Damage
    public int damageRangeMin; //a minimum damage an enemy can make
    public int damageRangeMax; //a maximum damage an enemy can make
    public float hitRate; //how fast enemy can attack, for instance if hitRate = 1; enemy will attack each second
    public float playerDetectionRange; //an invisible range for detecting player

    //Visuals
    public Sprite sprite; //enemy sprite
    public GameObject projectile; //if isRange enemy will need a projectile to shoot

}
