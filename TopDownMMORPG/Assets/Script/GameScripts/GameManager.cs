using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    Tilemap BlackMap;
    Tilemap DarkMap;
    Tilemap BlurredMap;
    Tilemap GroundMap;

    public Tile[] Tiles;
    
    void Start()
    {

        BlackMap = GameObject.Find("BlackMap").GetComponent<Tilemap>();
         DarkMap = GameObject.Find("DarkMap").GetComponent<Tilemap>();
         BlurredMap = GameObject.Find("BlurredMap").GetComponent<Tilemap>();
         GroundMap = GameObject.Find("GroundMap").GetComponent<Tilemap>();

        BlackMap.origin = BlurredMap.origin = GroundMap.origin;
        BlackMap.size = BlurredMap.size = GroundMap.size;

        DarkMap.origin = BlurredMap.origin = GroundMap.origin;
        DarkMap.size = BlurredMap.size = GroundMap.size;

        foreach (Vector3Int p in BlackMap.cellBounds.allPositionsWithin)
        {
            BlackMap.SetTile(p, Tiles[0]);
        }
        foreach (Vector3Int p in DarkMap.cellBounds.allPositionsWithin)
        {
            DarkMap.SetTile(p, Tiles[1]);
        }
        foreach (Vector3Int p in BlurredMap.cellBounds.allPositionsWithin)
        {
            BlurredMap.SetTile(p, Tiles[2]);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
