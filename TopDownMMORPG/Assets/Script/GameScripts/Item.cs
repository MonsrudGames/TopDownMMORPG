using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("If Melee Weapon: ")]
    public float Damage;
    public float AttackSpeed;
    public bool IsWeapon;
    public bool Primary;
    public bool IsEquiped;
    public bool IsPickupable;

    [Header("Item: ")]
    public string Name;
    public Sprite ItemSprite;
    public bool CanStack;
    public int quantity;
    public int MetalDissolveMats;
    public int WoodDissolveMats;
    public GameObject OriginalItemObj;

    GameObject Player;
    //InventoryManager IM;

    private void Start()
    {
        OriginalItemObj = this.gameObject;
        Player = GameObject.FindGameObjectWithTag("Player");
        //IM = GameObject.Find("GameManager").GetComponent<InventoryManager>();
    }

    float f_pickupTimer;

    public void PickupTimer()
    {
        f_pickupTimer = 3f;   
    }

    public void FixedUpdate()
    {

        if(f_pickupTimer > 0f)
        {
            f_pickupTimer -= Time.fixedDeltaTime;
        }

        if (Vector2.Distance(this.transform.position, Player.transform.position) <= 1f && f_pickupTimer <= 0f && IsPickupable)
        {
            //IM.AddItemToInv(this.gameObject, this.GetComponent<Item>());
        }
    }

}
