using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{

    //                <>

    public GameObject[] Equipments;
    public GameObject ActiveEquipment;
    public bool ActiveWeapon;

    public GameObject ArrowToSpawn;
    public GameObject[] Arrows;

    public bool IsAttacking;

    SpriteRenderer _SpriteRenderer;

    public GameObject[] EnemiesAroundPlayer;

    void Start()
    {
        _SpriteRenderer = GetComponent<SpriteRenderer>();
        ActiveEquipment = Equipments[0];
        ActiveWeapon = true;
    }

    void Update()
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
            if(equipment != null) {
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
            if(ActiveEquipment == Equipments[0])
            {
                StartCoroutine(SwingSwordAnim());
            }
            else if(ActiveEquipment == Equipments[1])
            {
                StartCoroutine(DrawBowAnim());
            }

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
    }

    void DamageEnemies()
    {
        if (ActiveEquipment = Equipments[0])
        {
            foreach (GameObject enemy in EnemiesAroundPlayer)
            {
                if (enemy != null)
                {
                    float TimeUntilDamageApply = 0;

                    Vector3 vec1 = enemy.transform.position;
                    Vector3 vec2 = this.transform.position;
                    Vector3 vec3 = this.transform.position + new Vector3(10f, 0);

                    float lenghtA = Mathf.Sqrt(Mathf.Pow(vec2.x - vec1.x, 2) + Mathf.Pow(vec2.y - vec1.y, 2));
                    float lenghtB = Mathf.Sqrt(Mathf.Pow(vec3.x - vec2.x, 2) + Mathf.Pow(vec3.y - vec2.y, 2));
                    float lenghtC = Mathf.Sqrt(Mathf.Pow(vec3.x - vec1.x, 2) + Mathf.Pow(vec3.y - vec1.y, 2));

                    float calc = ((lenghtA * lenghtA) + (lenghtB * lenghtB) - (lenghtC * lenghtC)) / (2 * lenghtA * lenghtB);

                    float angle = Mathf.Acos(calc) * Mathf.Rad2Deg;

                    if (enemy.transform.position.y > this.transform.position.y)
                    {
                        angle -= angle * 2f;
                    }

                    angle -= 180f;

                    angle = -angle;

                    Debug.Log("Angle = " + angle + " | on Entety: " + enemy.transform.name);

                    TimeUntilDamageApply = (0.0020f * angle);

                    enemy.GetComponent<EnemyScript>().GetHit(this.gameObject, TimeUntilDamageApply);
                }
            }
        }
        else if (ActiveEquipment == Equipments[1])
        {
            for (int i = 0; i < Arrows.Length; i++)
            {
                if(Arrows[i].GetComponent<ArrowScript>().Hit != null)
                {
                    if (Arrows[i].GetComponent<ArrowScript>().Hit.GetComponent<EnemyScript>() != null)
                    {
                        Arrows[i].GetComponent<ArrowScript>().Hit.GetComponent<EnemyScript>().GetHit(this.gameObject, 0.05f);
                        GameObject.Destroy(Arrows[i]);
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, 1.5f);
    }
}
