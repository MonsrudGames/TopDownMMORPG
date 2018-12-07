using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{

    //                <>

    public GameObject[] Equipment;
    public GameObject ActiveEquipment;
    public bool ActiveWeapon;

    public bool IsAttacking;

    SpriteRenderer _SpriteRenderer;

    public GameObject[] EnemiesAroundPlayer;

    void Start()
    {
        _SpriteRenderer = GetComponent<SpriteRenderer>();
        ActiveEquipment = Equipment[0];
        ActiveWeapon = true;
    }

    void Update()
    {

        if (Input.GetButtonDown("Fire1") && !IsAttacking)
        {
            StartCoroutine(SwingSwordAnim());
            DamageEnemies();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            for (int i = 0; i < EnemiesAroundPlayer.Length; i++)
            {
                if (EnemiesAroundPlayer[i] == null)
                {
                    EnemiesAroundPlayer[i] = collision.gameObject;
                    return;
                }
                else if (EnemiesAroundPlayer[i] == collision.gameObject)
                {
                    i = EnemiesAroundPlayer.Length;
                    return;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        for (int i = 0; i < EnemiesAroundPlayer.Length; i++)
        {
            if (EnemiesAroundPlayer[i] == collision.gameObject)
            {
                EnemiesAroundPlayer[i] = null;
            }
        }
    }

    IEnumerator SwingSwordAnim()
    {
        IsAttacking = true;
        GetComponent<PlayerAnimController>().Attacking(true);

        yield return new WaitForSeconds(0.95f);

        GetComponent<PlayerAnimController>().Attacking(false);
        IsAttacking = false;
    }

    void DamageEnemies()
    {
        foreach (GameObject enemy in EnemiesAroundPlayer)
        {
            if(enemy != null)
            {
                float TimeUntilDamageApply = 0;
                
                Mathf.an

                Debug.Log("Angle = " + angle);

                enemy.GetComponent<EnemyScript>().GetDamaged(this.gameObject, TimeUntilDamageApply);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, 1.5f);
    }
}
