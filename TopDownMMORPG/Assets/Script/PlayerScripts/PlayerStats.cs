using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public float BonusArmorHealth;

    [Range(0f, 100f)]
    public float PlayerHealth;

    [Range(0f, 100f)]
    public float PlayerMana;
}
