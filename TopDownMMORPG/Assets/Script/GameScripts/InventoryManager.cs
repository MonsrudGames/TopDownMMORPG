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

    [Header("InvInteractionUI")]
    public GameObject InteractionMenu;
    public GameObject NormalMenu;
    public GameObject ActiveMenu;
    public GameObject ActiveSlot;
    public Item ActiveItem;
    
    public void AddItemToInv(GameObject ItemObj, Item ItemScript)
    {
        bool ItemAllreadyInInv = false;
        bool ItemCanStack = false;
        bool EmptySlotFound = false;

        int SlotNumber = 0;

        for (int i = 0; i < itemsInInventory.Length || ItemAllreadyInInv; i++)
        {
            if (itemsInInventory[i] == ItemScript)
            {
                ItemAllreadyInInv = true;
                SlotNumber = i;
            }
        }

        if (ItemAllreadyInInv)
        {
            ItemCanStack = ItemScript.CanStack;

            if (ItemCanStack)
            {
                itemsInInventory[SlotNumber].quantity += ItemScript.quantity;
                ItemObj.SetActive(false);
            }
            else
            {
                for (int i = 0; i < itemsInInventory.Length && EmptySlotFound == false; i++)
                {
                    if (itemsInInventory[i] == null)
                    {
                        EmptySlotFound = true;
                        SlotNumber = i;
                        itemsInInventory[i] = ItemScript;
                        ItemObj.SetActive(false);
                        ItemObj.GetComponent<Item>().IsPickupable = false;
                    }
                }
            }
        }
        else if(!ItemAllreadyInInv)
        {
            for (int i = 0; i < itemsInInventory.Length && EmptySlotFound == false; i++)
            {
                if (itemsInInventory[i] == null)
                {
                    EmptySlotFound = true;
                    SlotNumber = i;
                    itemsInInventory[i] = ItemScript;
                    ItemObj.SetActive(false);
                    ItemObj.GetComponent<Item>().IsPickupable = false;
                }
            }
        }
        /*
        if (ItemAllreadyInInv == false || ItemAllreadyInInv && ItemCanStack)
        {
            for (int i = 0; i < itemsInInventory.Length && EmptySlotFound == false; i++)
            {
                if (itemsInInventory[i] == null)
                {
                    EmptySlotFound = true;
                    SlotNumber = i;
                    itemsInInventory[i] = ItemScript;
                ItemObj.SetActive(false);
                    print("Empty Item Slot Found");
                }
                print("Empty Item Slot *Not* Found");
            }
        }*/
    }

    private void Update()
    {
        for (int i = 0; i < ISlots.Length; i++)
        {
            if (ISlots[i] != null && itemsInInventory[i] != null)
            {
                ISlots[i].GetComponent<Image>().color = Color.white;
                ISlots[i].GetComponent<Image>().sprite = itemsInInventory[i].ItemSprite;
            }
            else
            {
                ISlots[i].GetComponent<Image>().color = Color.clear;
            }
        }
        for (int i = 0; i < ArmorSlots.Length; i++)
        {
            if (ArmorSlots[i] != null && ActiveArmorInInventory[i] != null)
            {
                ArmorSlots[i].GetComponent<Image>().color = Color.white;
                ArmorSlots[i].GetComponent<Image>().sprite = ActiveArmorInInventory[i].ItemSprite;
            }
            else
            {
                ArmorSlots[i].GetComponent<Image>().color = Color.clear;
            }
        }
        for (int i = 0; i < WeaponSlots.Length; i++)
        {
            if (WeaponSlots[i] != null && ActiveWeaponsInInventory[i] != null)
            {
                WeaponSlots[i].GetComponent<Image>().color = Color.white;
                WeaponSlots[i].GetComponent<Image>().sprite = ActiveWeaponsInInventory[i].ItemSprite;
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>().Equipments[i] = ActiveWeaponsInInventory[i].OriginalItemObj;
            }
            else
            {
                WeaponSlots[i].GetComponent<Image>().color = Color.clear;
            }
        }
        for (int i = 0; i < PotionSlots.Length; i++)
        {
            if (PotionSlots[i] != null && ActivePotionsInInventory[i] != null)
            {
                PotionSlots[i].GetComponent<Image>().color = Color.white;
                PotionSlots[i].GetComponent<Image>().sprite = ActivePotionsInInventory[i].ItemSprite;
            }
            else
            {
                PotionSlots[i].GetComponent<Image>().color = Color.clear;
            }
        }
    }

    public void SlotClicked(GameObject Slot)
    {
        if(Slot.tag == "NormalInvSlot")
        {
            if (ActiveSlot != Slot)
            {
                for (int i = 0; i < ISlots.Length; i++)
                {
                    if (ISlots[i] == Slot)
                    {
                        if (itemsInInventory[i] != null)
                        {
                            print("ItemClicked On: " + itemsInInventory[i].Name);
                            ActiveSlot = Slot;
                            for (int q = 0; q < ISlots.Length; q++)
                            {
                                if (itemsInInventory[q] != null)
                                {
                                    if (ISlots[q] == ActiveSlot)
                                    {
                                        ActiveItem = itemsInInventory[q];
                                    }
                                }
                            }
                            InteractionMenu.SetActive(true);
                            ActiveMenu.SetActive(false);
                            NormalMenu.SetActive(true);
                            InteractionMenu.transform.position = ActiveSlot.transform.position;
                        }
                    }
                }
            }
            else
            {
                ActiveSlot = null;
                InteractionMenu.SetActive(false);
            }
        }
        else if(Slot.tag == "ActiveInvSlot")
        {
            if (ActiveSlot != Slot)
            {

                for (int i = 0; i < WeaponSlots.Length; i++)
                {
                    if(WeaponSlots[i] == ActiveSlot)
                    {
                        ActiveItem = ActiveWeaponsInInventory[i];
                    }
                    else
                    {
                        print("ActiveItem NOT set");
                    }
                }
                if(ActiveItem != null)
                {
                    if (ActiveItem.IsWeapon)
                    {
                        for (int i = 0; i < WeaponSlots.Length; i++)
                        {
                            if (WeaponSlots[i] == Slot)
                            {
                                if (ActiveWeaponsInInventory[i] != null)
                                {
                                    print("ItemClicked On: " + ActiveWeaponsInInventory[i].Name);
                                    ActiveSlot = Slot;
                                    InteractionMenu.SetActive(true);
                                    ActiveMenu.SetActive(true);
                                    NormalMenu.SetActive(false);
                                    InteractionMenu.transform.position = ActiveSlot.transform.position;
                                }
                                else
                                {
                                    print("ActiveWeaponsInInventory NOT empty");
                                }
                            }
                            else
                            {
                                print("WeaponSlots NOT equals to Slot");
                            }
                        }
                    }
                    else
                    {
                        print("NOT IsWeapon");
                    }
                }
            }
            else
            {
                ActiveSlot = null;
                print("Slot Was the same as ActiveSlot");
                InteractionMenu.SetActive(false);
            }
        }
    }
    public void EquipClicked()
    {
        if (ActiveItem != null)
        {
            if (ActiveItem.IsWeapon)
            {
                if (ActiveItem.Primary)
                {
                    ActiveWeaponsInInventory[0] = ActiveItem;
                    for (int q = 0; q < ISlots.Length; q++)
                    {
                        if (itemsInInventory[q] != null)
                        {
                            if (ISlots[q] == ActiveSlot)
                            {
                                itemsInInventory[q] = null;
                                ActiveItem.IsEquiped = true;
                                ActiveItem = null;
                                ActiveSlot = null;
                                InteractionMenu.SetActive(false);
                            }
                        }
                    }
                }
                else if (!ActiveItem.Primary)
                {
                    ActiveWeaponsInInventory[1] = ActiveItem;
                    for (int q = 0; q < ISlots.Length; q++)
                    {
                        if (itemsInInventory[q] != null)
                        {
                            if (ISlots[q] == ActiveSlot)
                            {
                                itemsInInventory[q] = null;
                                ActiveItem.IsEquiped = true;
                                InteractionMenu.SetActive(false);
                            }
                        }
                    }
                }
            }
        }
    }

    public void UnequipClicked()
    {
        for (int i = 0; i < ActiveWeaponsInInventory.Length; i++)
        {
            if(ActiveWeaponsInInventory[i] != null)
            {
                if(ActiveWeaponsInInventory[i] == ActiveItem)
                {
                    for (int q = 0; q < itemsInInventory.Length; q++)
                    {
                        if (itemsInInventory[q] == null)
                        {
                            if (ActiveItem != null)
                            {
                                itemsInInventory[q] = ActiveWeaponsInInventory[i];
                                ActiveWeaponsInInventory[i] = null;
                                ActiveItem.IsEquiped = false;
                                InteractionMenu.SetActive(false);
                            }
                        }
                    }
                }
            }
        }
    }

    public void DissolveClicked()
    {
    }
    public void TrowClicked()
    {
        ActiveItem.OriginalItemObj.SetActive(true);
        ActiveItem.OriginalItemObj.transform.position = GameObject.FindGameObjectWithTag("Player").transform.position + new Vector3(0, -1);

        ActiveItem.PickupTimer();
        if (ActiveItem.IsWeapon)
        {
            if (ActiveItem.IsEquiped)
            {
                for (int i = 0; i < ActiveWeaponsInInventory.Length; i++)
                {
                    if (ActiveWeaponsInInventory[i] == ActiveItem)
                    {
                        ActiveWeaponsInInventory[i] = null;
                        ActiveItem.IsEquiped = false;
                        ActiveItem.IsPickupable = false;
                        ActiveItem.transform.parent = null;
                    }
                }
            }
            else
            {
                for (int i = 0; i < itemsInInventory.Length; i++)
                {
                    if (itemsInInventory[i] == ActiveItem)
                    {
                        itemsInInventory[i] = null;
                        ActiveItem.IsEquiped = false;
                        ActiveItem.IsPickupable = false;
                        ActiveItem.transform.parent = null;
                    }
                }
            }
        }
        else if (!ActiveItem.IsWeapon)
        {
            for (int i = 0; i < itemsInInventory.Length; i++)
            {
                if (itemsInInventory[i] == ActiveItem)
                {
                    itemsInInventory[i] = null;
                    ActiveItem.transform.parent = null;
                }
            }
        }
        InteractionMenu.SetActive(false);
    }

}
