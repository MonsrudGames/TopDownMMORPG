using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    GameObject _canvas;

    public GameObject EnemyUI;

    public GameObject[] Enemies;
    public GameObject[] EnemyUIs;

    void Start()
    {
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        _canvas = GameObject.Find("EnemyCanvas");
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
}