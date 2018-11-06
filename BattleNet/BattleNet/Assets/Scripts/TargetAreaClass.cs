using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eDIRECTION
{
    UP,
    DOWN,
    LEFT,
    RIGHT
}

public class TargetAreaClass {
    
    public static List<TileScript> STRAIGHT(TilemapScript tilemap, Vector2Int startPos, eDIRECTION dir, int length, int width)
    {
        List<TileScript> finalArea = new List<TileScript>();


        /*
        for (int x = startPos.x; x < tilemap.GetTileAmt().x && ; ++x)
        {

        }
        */

        return finalArea;
    }

}

