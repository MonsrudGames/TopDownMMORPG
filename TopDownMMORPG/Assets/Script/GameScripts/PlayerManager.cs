using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    GameObject Player;
    public GameObject PlayerInventory;
    //public InventoryManager IM;

    [Header("UI")]
    public Slider PlayerHealthUISlider;
    public Slider PlayerManaUISlider;

    public bool CanMove, CameraCanMove, CanAttack;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        //IM = GetComponent<InventoryManager>();
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            PlayerInventory.SetActive(!PlayerInventory.activeInHierarchy);
        }

        CanMove = CanAttack  = CameraCanMove = !PlayerInventory.activeInHierarchy;

        PlayerHealthUISlider.SetValueWithoutNotify(Player.GetComponent<PlayerStats>().PlayerHealth);
        PlayerManaUISlider.SetValueWithoutNotify(Player.GetComponent<PlayerStats>().PlayerMana);
    }
}
