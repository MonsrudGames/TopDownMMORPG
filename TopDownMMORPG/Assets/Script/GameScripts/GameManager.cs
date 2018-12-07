using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public Tilemap DarkMap;
    public Tilemap BlurredMap;
    public Tilemap GroundMap;

    public Tile DarkTile;
    public Tile BlurredTile;
    
    void Start()
    {

        DarkMap.origin = BlurredMap.origin = GroundMap.origin;
        DarkMap.size = BlurredMap.size = GroundMap.size;

        foreach (Vector3Int p in DarkMap.cellBounds.allPositionsWithin)
        {
            DarkMap.SetTile(p, DarkTile);
        }
        foreach (Vector3Int p in BlurredMap.cellBounds.allPositionsWithin)
        {
            BlurredMap.SetTile(p, BlurredTile);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
