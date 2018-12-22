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

    public void ReciveDamage()
    {

    }

    void Die()
    {

    }
}
