using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Create New Weapon")]

public class WeaponTemplate : ScriptableObject
{
    //Name
    public string weaponName; //weapon name
    public string type; //type of a weapon
    public string rarity; //rarity of the weapon

    //Physics
    public int mass; //mass of a weapon
    //sword -> light weapon
    //spear -> heavy weapon
    //bow -> middle

    //Damage
    public int damageRangeMin; //a minimum damage a weapon can make
    public int damageRangeMax; //a maximum damage a weapon can make
    public float hitRate; //hit rate of a weapon(how fast it is) 
    
    //Bonuses
    public int critChance; //critical damage chance
    public int critMultiplier;

    //Visuals
    public Sprite icon; //weapon icon in inventory/ground
    
}

