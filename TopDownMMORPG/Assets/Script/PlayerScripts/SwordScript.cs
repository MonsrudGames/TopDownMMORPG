using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour
{

    PlayerScript Player;

    void Start()
    {
        Player = GetComponentInParent<PlayerScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy" || collision.tag == "boss")
        {
            collision.gameObject.GetComponent<EnemyScript>().GetHit(Player.gameObject);
        }
    }
}
