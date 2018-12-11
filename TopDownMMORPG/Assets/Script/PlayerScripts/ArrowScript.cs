using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    public GameObject Hit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Hit = collision.gameObject;
    }
}
