using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    GameObject _canvas;

    public GameObject _slider;

    public GameObject[] Enemies;
    public GameObject[] EnemyHealthSliders;

    void Start()
    {
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        _canvas = GameObject.Find("EnemyCanvas");
        for (int i = 0; i < Enemies.Length; i++)
        {
            if(EnemyHealthSliders[i] == null)
            {
                GameObject CurrentEnemySlider = Instantiate(_slider, _canvas.transform);
                EnemyHealthSliders[i] = CurrentEnemySlider;
            }
        }
        
    }
    
    void Update()
    {
        for (int i = 0; i < EnemyHealthSliders.Length && i < Enemies.Length; i++)
        {
            if(Enemies[i] != null && EnemyHealthSliders[i] != null)
            {
                EnemyHealthSliders[i].transform.position = Enemies[i].transform.position + new Vector3(0, Enemies[i].GetComponent<CircleCollider2D>().bounds.extents.y);
                Enemies[i].GetComponent<EnemyScript>()._HealthSlider = EnemyHealthSliders[i];

            }else if(EnemyHealthSliders[i] == null && Enemies[i] != null)
            {
                GameObject CurrentEnemySlider = Instantiate(_slider, _canvas.transform);
                EnemyHealthSliders[i] = CurrentEnemySlider;
                
                EnemyHealthSliders[i].transform.position = Enemies[i].transform.position + new Vector3(0, Enemies[i].GetComponent<CircleCollider2D>().bounds.extents.y);
            }else if(Enemies[i] == null && EnemyHealthSliders[i] == null)
            {
            }
        }
    }
}