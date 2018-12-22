using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{

    public GameObject[] Equipments;
    public GameObject ActiveEquipment;
    public bool ActiveWeapon;

    public GameObject ArrowToSpawn;
    public GameObject[] Arrows;

    public bool IsAttacking;
    public bool CanAttack;

    SpriteRenderer _SpriteRenderer;

    public GameObject EnemyToHit;
    public GameObject[] EnemiesThatWasHit;

    void Start()
    {
        _SpriteRenderer = GetComponent<SpriteRenderer>();
        ActiveEquipment = Equipments[0];
        ActiveWeapon = true;
    }

    void Update()
    {

        if (CanAttack)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && !IsAttacking && ActiveEquipment != Equipments[0])
            {
                ActiveEquipment = Equipments[0];
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) && !IsAttacking && ActiveEquipment != Equipments[1])
            {
                ActiveEquipment = Equipments[1];
            }

            foreach (GameObject equipment in Equipments)
            {
                if (equipment != null)
                {
                    if (equipment != ActiveEquipment && equipment.activeSelf == true)
                    {
                        equipment.SetActive(false);
                    }
                    else if (equipment.activeSelf == true)
                    {
                        equipment.SetActive(true);
                    }
                }
            }

            if (Input.GetButtonDown("Fire1") && !IsAttacking)
            {
                if (ActiveEquipment == Equipments[0])
                {
                    StartCoroutine(SwingSwordAnim());
                }
                else if (ActiveEquipment == Equipments[1])
                {
                    StartCoroutine(DrawBowAnim());
                }
            }
            else if (!IsAttacking)
            {
                GetComponentInChildren<BoxCollider2D>().enabled = false;
            }
            else if (IsAttacking)
            {
                GetComponentInChildren<BoxCollider2D>().enabled = true;
                DamageEnemies();
            }
        }
    }

    IEnumerator DrawBowAnim()
    {
        IsAttacking = true;
        bool a = false;
        while (!a)
        {
            for (int i = 0; i < Arrows.Length || a == true; i++)
            {
                if(Arrows[i] == null)
                {
                    a = true;
                }
            }
        }

        Instantiate(ArrowToSpawn, this.transform.position, this.transform.rotation);

        yield return new WaitForSeconds(0.5f);
        IsAttacking = false;
    }

    IEnumerator SwingSwordAnim()
    {
        IsAttacking = true;
        GetComponent<PlayerAnimController>().Attacking(true);

        yield return new WaitForSeconds(0.95f);

        GetComponent<PlayerAnimController>().Attacking(false);
        IsAttacking = false;

        for (int i = 0; i < EnemiesThatWasHit.Length; i++)
        {
            EnemiesThatWasHit[i] = null;
        }
        EnemyToHit = null;
    }

    void DamageEnemies()
    {
        if (ActiveEquipment = Equipments[0])
        {
            bool a = false;

            for (int i = 0; i < EnemiesThatWasHit.Length; i++)
            {
                if (EnemiesThatWasHit[i] == EnemyToHit)
                {
                    a = true;
                    Debug.Log("NEIN!");
                    i = EnemiesThatWasHit.Length;
                }

                Debug.Log("ja!");
            }

            if (!a)
            {
                EnemyToHit.GetComponent<EnemyScript>().GetHit(this.gameObject);

                for (int i = 0; i < EnemiesThatWasHit.Length; i++)
                {
                    if (EnemiesThatWasHit[i] == null)
                    {
                        Debug.Log("YASS!");
                        EnemiesThatWasHit[i] = EnemyToHit;
                        i = EnemiesThatWasHit.Length;
                    }
                    Debug.Log("nei!");
                }
            }

            EnemyToHit = null;
        }

        /*
        else if (ActiveEquipment == Equipments[1])
        {
            for (int i = 0; i < Arrows.Length; i++)
            {
                if(Arrows[i].GetComponent<ArrowScript>().Hit != null)
                {
                    if (Arrows[i].GetComponent<ArrowScript>().Hit.GetComponent<EnemyScript>() != null)
                    {
                        Arrows[i].GetComponent<ArrowScript>().Hit.GetComponent<EnemyScript>().GetHit(this.gameObject);
                        GameObject.Destroy(Arrows[i]);
                    }
                }
            }
        }
            */
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, 1.5f);
    }
}