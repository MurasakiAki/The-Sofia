using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Object", menuName = "Create New Object")]

public class ObjectTemplate : ScriptableObject
{
    //Name
    public string objectName; //object name

    //Health
    public int maxHealth; //max health of object

    //Physics
    public int mass; //mass of enemy, can influence enemy knockback
    public bool isStatic; //won't be affected by physics
    public bool isDestroyable; //can be destroyed
    public bool isLootable; //will drop loot
    public PhysicsMaterial2D material; //material of the object

    //Visuals
    public Sprite sprite; //object sprite

}
