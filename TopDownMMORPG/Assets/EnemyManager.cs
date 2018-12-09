using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    GameObject _canvas;

    public GameObject _slider;

    public GameObject[] Enemies;
    public Slider[] EnemyHealthSliders;

    void Start()
    {
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        _canvas = GameObject.Find("Canvas");
        
    }
    
    void Update()
    {
        for (int i = 0; i < EnemyHealthSliders.Length; i++)
        {
            EnemyHealthSliders[i].GetComponent<Slider>().value = 100;
            EnemyHealthSliders[i].transform.position = Enemies[i].transform.position;
        }
    }
}