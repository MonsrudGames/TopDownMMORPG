using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyScript : MonoBehaviour
{

    Rigidbody2D rb;

    float Health = 100;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if(Health <= 0f)
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    public void GetDamaged(GameObject DamageDeltBy)
    {
        StartCoroutine(DamageColorChange());
        AddForce(DamageDeltBy);
        TakeDamage();
    }

    void TakeDamage()
    {
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

        ForceDir *= 10f;

        rb.AddForce(ForceDir, ForceMode2D.Impulse);
    }
}
