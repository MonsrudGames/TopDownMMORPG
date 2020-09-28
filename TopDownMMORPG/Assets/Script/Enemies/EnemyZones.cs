using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyZones : MonoBehaviour
{

    public zone[] _zones;

    void Start()
    {
        _zones = GameObject.FindObjectsOfType<zone>();
    }
}