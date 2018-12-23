using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public GameObject Player;

    [Header("UI")]
    public Slider PlayerHealthUISlider;
    public Slider PlayerManaUISlider;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        PlayerHealthUISlider.SetValueWithoutNotify(Player.GetComponent<PlayerStats>().PlayerHealth);
        PlayerManaUISlider.SetValueWithoutNotify(Player.GetComponent<PlayerStats>().PlayerMana);
    }
}
