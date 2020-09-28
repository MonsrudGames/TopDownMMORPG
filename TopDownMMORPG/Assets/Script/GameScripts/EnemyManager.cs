using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    GameObject _canvas;

    public GameObject EnemyUI;

    EnemyZones _enemyZones;
    GameObject[] _enemyGroups;

    public GameObject[] Enemies;
    public GameObject[] EnemyUIs;

    void Start()
    {
        _enemyZones = GameObject.Find("EnemyZones").GetComponent<EnemyZones>();
        _canvas = GameObject.Find("EnemyCanvas");

        StartCoroutine(Spawn());
        
    }

    void FixedUpdate()
    {
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < Enemies.Length; i++)
        {
            if(EnemyUIs[i] == null)
            {
                GameObject CurrentEnemyUI = Instantiate(EnemyUI, _canvas.transform);
                EnemyUIs[i] = CurrentEnemyUI;
            }
        }
    }
    
    void Update()
    {
        for (int i = 0; i < EnemyUIs.Length && i < Enemies.Length; i++)
        {
            if(Enemies[i] != null && EnemyUIs[i] != null)
            {
                EnemyUIs[i].transform.position = Enemies[i].transform.position + new Vector3(0, Enemies[i].GetComponent<CircleCollider2D>().bounds.extents.y);
                Enemies[i].GetComponent<EnemyScript>()._enemyUI = EnemyUIs[i];

            }else if(EnemyUIs[i] == null && Enemies[i] != null)
            {
                GameObject CurrentEnemyUI = Instantiate(EnemyUI, _canvas.transform);
                EnemyUIs[i] = CurrentEnemyUI;
                
                EnemyUIs[i].transform.position = Enemies[i].transform.position + new Vector3(0, Enemies[i].GetComponent<CircleCollider2D>().bounds.extents.y);
            }else if(Enemies[i] == null && EnemyUIs[i] == null)
            {
            }
        }
    }

    void SpawnEnemies()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        foreach (zone _zone in _enemyZones._zones)
        {
            if(_zone.TimeSinceLastEnemyRespawn  >= _zone.RespawnRate && _zone.EnemiesInZone < _zone.MaxEnemiesInZone)
            {
                GameObject Enemygroup = Instantiate(_zone.EnemyGroupTypes[Random.Range(0, _zone.EnemyGroupTypes.Length)], _zone.zoneCenterPos + new Vector3(Random.Range(-(_zone.ZoneSize.x / 2), _zone.ZoneSize.x / 2), Random.Range(-(_zone.ZoneSize.y / 2), _zone.ZoneSize.y / 2)),Quaternion.Euler(Vector3.zero));
                
                foreach (EnemyScript enemy in Enemygroup.GetComponentsInChildren<EnemyScript>())
                {
                    enemy.zoneOfOrigin = _zone;
                }
                
                _zone.EnemiesInZone += Enemygroup.GetComponentsInChildren<EnemyScript>().Length;
            }
        }
        yield return new WaitForSeconds(3f);
        SpawnEnemies();
    }
}