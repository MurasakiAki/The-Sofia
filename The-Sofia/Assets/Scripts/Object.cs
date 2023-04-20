using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    public ObjectTemplate objectTemplate;
    private Rigidbody2D rb;
    private Collider2D coll;

    public GameObject[] weaponPrefabs;
    public Vector2Int itemChance = new Vector2Int(1, 10);
    public Vector2Int coinChance = new Vector2Int(11, 100);

    public GameObject coinPrefab;

    public string[] weaponNameList = {"Xiphos", "BrokenSpear", "RecurveBow"};

    public int maxHealth;


    void Start()
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

        if(objectTemplate.isDestroyable)
        {
            maxHealth = objectTemplate.maxHealth;
            gameObject.layer = LayerMask.NameToLayer("Hitable");
        }
    }

    private void OnDestroy()
    {
        if(objectTemplate.isLootable)
        {
            DropLoot();
        }    
    }

    private void DropLoot()
    {
        System.Random random = new System.Random();
        int pickedNumber = random.Next(1, 101);

        if(GameLogic.CheckRange(pickedNumber, itemChance.x, itemChance.y))
        {
            int itemID = random.Next(0, weaponNameList.Length - 1);
            GameLogic.SpawnObject(transform, weaponNameList[itemID] , weaponPrefabs);
        }else
        {
            GameObject coin = Instantiate(coinPrefab, this.gameObject.transform.position, this.gameObject.transform.rotation);
        }

    }

    

}
