using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public PlayerStats _stats;

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
