using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    PlayerManager PM;

    [Header("Items")]
    public Item[] itemsInInventory;
    public Item[] ActiveArmorInInventory;
    public Item[] ActiveWeaponsInInventory;
    public Item[] ActivePotionsInInventory;

    [Header("UI")]
    public GameObject[] ISlots;
    public GameObject[] ArmorSlots;
    public GameObject[] WeaponSlots;
    public GameObject[] PotionSlots;

    public Sprite Test;
    
    public void AddItemToInv(GameObject ItemObj, Item ItemScript)
    {
        bool EmptySlotFound = false;
        for (int i = 0; i < itemsInInventory.Length && EmptySlotFound == false; i++)
        {
            if(itemsInInventory[i] == null)
            {
                itemsInInventory[i] = ItemScript;
                GameObject.Destroy(ItemObj);

                EmptySlotFound = true;
            }
        }
    }

    private void Update()
    {
        for (int i = 0; i < ArmorSlots.Length; i++)
        {
            if(ArmorSlots[i] != null && ActiveArmorInInventory[i] != null)
            {
                ArmorSlots[i].GetComponent<Image>().color = Color.white;
                ArmorSlots[i].GetComponent<Image>().sprite = ActiveArmorInInventory[i].ItemSprite;
            }
            else
            {
                ArmorSlots[i].GetComponent<Image>().color = Color.clear;
            }
        }
    }

}
