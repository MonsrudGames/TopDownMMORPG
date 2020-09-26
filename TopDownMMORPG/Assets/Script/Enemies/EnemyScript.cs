using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyScript : MonoBehaviour
{

    [Header("EnemyStats")]
    public float Health = 100;
    [Range(1, 30)]
    public float DetectionRange;
    public float MovementSpeed;
    public float Damage;

    float TimeSinceLastAttack;

    bool CanDie;
    
    Rigidbody2D rb;
    GameObject Player;
    [HideInInspector]
    public GameObject _HealthSlider;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player");

        CanDie = true;
    }

    private void Update()
    {
        TimeSinceLastAttack += 1 * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if(_HealthSlider != null)
        {
            if (_HealthSlider.GetComponent<Slider>() != null)
            {
                _HealthSlider.GetComponent<Slider>().SetValueWithoutNotify(Health);
            }

            if (Vector3.Distance(Player.transform.position, this.transform.position) <= DetectionRange && Vector3.Distance(Player.transform.position, this.transform.position) >= 1.3f)
            {
                Move(Player.transform.position);
                DetectionRange = 30;
            }
            else
            {
                DetectionRange = 9;
            }

            if(Vector3.Distance(Player.transform.position, this.transform.position) <= 1.5f && TimeSinceLastAttack >= 2f)
            {
                StartCoroutine(DamagePlayer());
            }
        }

        if(Health <= 0f && CanDie)
        {
            GameObject.Destroy(_HealthSlider.gameObject);
            GameObject.Destroy(this.gameObject);
        }
    }

    IEnumerator DamagePlayer()
    {
        yield return new WaitForSeconds(0.2f);
        if (Vector3.Distance(Player.transform.position, this.transform.position) <= 1.5f && TimeSinceLastAttack >= 2f)
        {
            Player.GetComponent<PlayerScript>().ReciveDamage(this.gameObject, Damage);
            TimeSinceLastAttack = 0;
        }
    }

    void Move(Vector3 MovePos)
    {
        rb.AddForce(Vector3.ClampMagnitude((Player.transform.position - this.transform.position), 1f) * (MovementSpeed * 100f));
    }

    public void GetHit(GameObject DamageDeltBy)
    {
        if(DamageDeltBy != null)
        {
            GetDamaged(DamageDeltBy);
        }
    }

    void GetDamaged(GameObject DamageDeltBy)
    {
        StartCoroutine(DamageColorChange());
        AddForce(DamageDeltBy);
        TakeHealth();
    }

    void TakeHealth()
    {
        Health -= 34f;
    }

    IEnumerator DamageColorChange()
    {
        GetComponentInChildren<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.2f);
        GetComponentInChildren<SpriteRenderer>().color = Color.white;
    }

    void AddForce(GameObject DamageDeltBy)
    {
        Vector2 ForceDir;
        ForceDir = (this.transform.position - DamageDeltBy.transform.position);

        if (ForceDir.x > 0)
            ForceDir = new Vector2(1, ForceDir.y);
        if (ForceDir.y > 0)
            ForceDir = new Vector2(ForceDir.x, 1);

        if (ForceDir.x < 0)
            ForceDir = new Vector2(-1, ForceDir.y);
        if (ForceDir.y < 0)
            ForceDir = new Vector2(ForceDir.x, -1);

        ForceDir *= 100f;

        rb.AddForce(ForceDir, ForceMode2D.Impulse);
    }
}
