using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyScript : MonoBehaviour
{

    Rigidbody2D rb;

    GameObject Player;
    
    [Range(1,30)]
    public float DetectionRange;

    public float MovementSpeed;

    public float Health = 100;
    public GameObject _HealthSlider;

    public bool CanDie;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    Collision2D Collision;

    private void OnCollisionStay2D(Collision2D collision)
    {
        Collision = collision;
    }

    private void FixedUpdate()
    {
        if(_HealthSlider != null)
        {
            if (_HealthSlider.GetComponent<Slider>() != null)
            {
                _HealthSlider.GetComponent<Slider>().SetValueWithoutNotify(Health);
            }

            if (Vector3.Distance(Player.transform.position, this.transform.position) <= DetectionRange && Vector3.Distance(Player.transform.position, this.transform.position) >= 1.5f)
            {
                Move(Player.transform.position);
                DetectionRange = float.PositiveInfinity;
            }
        }

        if(Health <= 0f && CanDie)
        {
            GameObject.Destroy(_HealthSlider.gameObject);
            GameObject.Destroy(this.gameObject);
        }
    }

    void Move(Vector3 MovePos)
    {
        this.transform.position += (Player.transform.position - this.transform.position).normalized * Time.deltaTime * MovementSpeed;
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

        ForceDir *= 50f;

        rb.AddForce(ForceDir, ForceMode2D.Impulse);
    }
}
