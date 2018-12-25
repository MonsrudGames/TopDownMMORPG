using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("If Melee Weapon: ")]
    public float Damage;
    public float AttackSpeed;

    [Header("Item: ")]
    public Sprite ItemSprite;
    public int quantity;
}
