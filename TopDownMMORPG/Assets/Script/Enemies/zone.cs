using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zone : MonoBehaviour
{
    public Vector3 zoneCenterPos;
    public Vector3 ZoneSize;

    public int ZoneLevel;

    public int EnemiesInZone;
    public int MaxEnemiesInZone;
    public GameObject[] EnemyGroupTypes;

    public float RespawnRate;
    public float TimeSinceLastEnemyRespawn;

    void awake()
    {
        MaxEnemiesInZone = (int)((ZoneSize.x * ZoneSize.y) / 100f);
        zoneCenterPos = this.transform.position;
        ZoneSize = this.transform.localScale;
    }

    void Update()
    {
        TimeSinceLastEnemyRespawn += 1 * Time.deltaTime;
    }

    void OnDrawGizmosSelected()
    {
        zoneCenterPos = this.transform.position;
        ZoneSize = this.transform.localScale;

        MaxEnemiesInZone = (int)((ZoneSize.x * ZoneSize.y) / 100f);

        // Draw a semitransparent blue cube at the transforms position
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(zoneCenterPos, ZoneSize);
    }
}
