﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public PlayerStats _stats;

    public GameObject ActiveWeapon;

    // Start is called before the first frame update
    void Start()
    {
        _stats = GetComponent<PlayerStats>();
        _stats.PlayerHealth = 100f;
        _stats.PlayerMana = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        if (_stats.PlayerHealth <= 0f)
        {
            Die();
        }

        if(Input.GetKeyDown(KeyCode.Mouse0) && !GetComponent<PlayerAnimController>().IsAttacking)
        {
            StartCoroutine(Attack());
        }
    }

    bool DeadAnimStarted = false;

    public void ReciveDamage( GameObject DamageDeltBy, float Damage)
    {
        if (!_stats.Dead)
        {
            _stats.PlayerHealth -= Damage;
            Debug.Log("Currently have " + _stats.PlayerHealth + " Health. Lost " + Damage + "Health");
            StartCoroutine(ChangePlayerColor());
        }
        else if(!DeadAnimStarted)
        {
            StartCoroutine(PlayerKilled());
        }
    }

    void Die()
    {
        Debug.Log("Dead!");
        _stats.Dead = true;
    }

    IEnumerator Attack()
    {
        GetComponent<PlayerAnimController>().Attacking(true);
        ActiveWeapon.GetComponent<BoxCollider2D>().enabled = true;
        yield return new WaitForSeconds(GetComponent<PlayerAnimController>().Attack.length);
        ActiveWeapon.GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<PlayerAnimController>().Attacking(false);
    }

    IEnumerator ChangePlayerColor()
    {
        GetComponentInChildren<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.2f);
        GetComponentInChildren<SpriteRenderer>().color = Color.white;
    }

    IEnumerator PlayerKilled()
    {
        DeadAnimStarted = true;
        GetComponentInChildren<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(1f);
        this.gameObject.SetActive(false);
    }

}
