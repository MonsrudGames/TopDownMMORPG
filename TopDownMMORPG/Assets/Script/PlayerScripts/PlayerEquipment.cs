using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{

    public GameObject[] Equipments;
    public GameObject ActiveEquipment;
    public bool WeaponActive;
    
    public bool SwordActive;
    public bool BowActive;
    public bool StaffActive;
    public bool KnifeActive;

    public GameObject ArrowToSpawn;
    public GameObject[] Arrows;

    public bool IsAttacking;

    public GameObject EnemyToHit;
    public GameObject[] EnemiesThatWasHit;

    PlayerManager PM;

    void Start()
    {
        PM = GameObject.Find("GameManager").GetComponent<PlayerManager>();
        ActiveEquipment = Equipments[0];
        WeaponActive = false;
    }

    void Update()
    {

        for (int i = 0; i < Equipments.Length; i++)
        {
            if(Equipments[i] != null)
            {
                if (Equipments[i].transform.parent != this.transform)
                {
                    Equipments[i].transform.parent = this.transform.parent;
                }
            }
        }

        if (!WeaponActive)
        {
            foreach (GameObject Weapon in Equipments)
            {
                if(Weapon != null)
                {
                    Weapon.SetActive(false);
                }
            }
        }

        if (PM.CanAttack)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && !IsAttacking && ActiveEquipment != Equipments[0])
            {
                ActiveEquipment = Equipments[0];

                WeaponActive = true;
                if(Equipments[0] != null)
                {
                    Equipments[0].SetActive(true);
                }
                if(Equipments[1] != null)
                {
                    Equipments[1].SetActive(false);
                }
            }else if (Input.GetKeyDown(KeyCode.Alpha2) && !IsAttacking && ActiveEquipment != Equipments[1])
            {
                ActiveEquipment = Equipments[1];

                WeaponActive = true;
                if (Equipments[0] != null)
                {
                    Equipments[0].SetActive(false);
                }
                if (Equipments[1] != null)
                {
                    Equipments[1].SetActive(true);
                }
            }

            if (Input.GetButtonDown("Fire1") && !IsAttacking)
            {
                if (SwordActive)
                {
                    StartCoroutine(SwingSwordAnim());
                }
                else if (BowActive)
                {
                    StartCoroutine(DrawBowAnim());
                }
                else if (StaffActive)
                {
                    StartCoroutine(StaffShootAnim());
                }
                else if (KnifeActive)
                {
                    StartCoroutine(SwingKnifeAnim());
                }
            }
            else if (!IsAttacking)
            {
                if (GetComponentInChildren<BoxCollider2D>() != null)
                {
                    if (SwordActive || KnifeActive)
                    {
                        GetComponentInChildren<BoxCollider2D>().enabled = false;
                    }
                }
            }
            else if (IsAttacking)
            {
                if (GetComponentInChildren<BoxCollider2D>() != null)
                {
                    if (SwordActive || KnifeActive)
                    {
                        GetComponentInChildren<BoxCollider2D>().enabled = true;
                        DamageEnemies();
                    }
                }
            }
        }
    }

    void DamageEnemies()
    {
        if (SwordActive)
        {
            bool a = false;

            for (int i = 0; i < EnemiesThatWasHit.Length; i++)
            {
                if (EnemiesThatWasHit[i] == EnemyToHit)
                {
                    a = true;
                    i = EnemiesThatWasHit.Length;
                }
            }

            if (!a)
            {
                if(EnemyToHit != null)
                {
                    if(EnemyToHit.GetComponent<EnemyScript>() != null)
                    {
                        EnemyToHit.GetComponent<EnemyScript>().GetHit(this.gameObject);
                    }
                }

                for (int i = 0; i < EnemiesThatWasHit.Length; i++)
                {
                    if (EnemiesThatWasHit[i] == null)
                    {
                        EnemiesThatWasHit[i] = EnemyToHit;
                        i = EnemiesThatWasHit.Length;
                    }
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

    IEnumerator DrawBowAnim()
    {
        IsAttacking = true;
        bool a = false;
        while (!a)
        {
            for (int i = 0; i < Arrows.Length || a == true; i++)
            {
                if (Arrows[i] == null)
                {
                    a = true;
                }
            }
        }

        Instantiate(ArrowToSpawn, this.transform.position, this.transform.rotation);

        yield return new WaitForSeconds(0.5f);
        IsAttacking = false;
    }

    IEnumerator StaffShootAnim()
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

    IEnumerator SwingKnifeAnim()
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
}