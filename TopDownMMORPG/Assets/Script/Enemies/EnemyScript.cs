using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyScript : MonoBehaviour
{

    Rigidbody2D rb;

    public float Health = 100;
    public GameObject _HealthSlider;

    public bool CanDie;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if(_HealthSlider != null)
        {
            if (_HealthSlider.GetComponent<Slider>() != null)
            {
                _HealthSlider.GetComponent<Slider>().SetValueWithoutNotify(Health);
            }
        }

        if(Health <= 0f && CanDie)
        {
            GameObject.Destroy(_HealthSlider.gameObject);
            GameObject.Destroy(this.gameObject);
        }
    }

    public void GetHit(GameObject DamageDeltBy, float time)
    {
        StartCoroutine(GetDamaged(DamageDeltBy, time));
    }

    IEnumerator GetDamaged(GameObject DamageDeltBy,float time)
    {
        yield return new WaitForSeconds(time);
        StartCoroutine(DamageColorChange());
        AddForce(DamageDeltBy);
        StartCoroutine(TakeDamage(0.2f));
    }

    IEnumerator TakeDamage(float time)
    {
        yield return new WaitForSeconds(time);
        Health -= 34f;
    }

    IEnumerator DamageColorChange()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.2f);
        GetComponent<SpriteRenderer>().color = Color.white;
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
