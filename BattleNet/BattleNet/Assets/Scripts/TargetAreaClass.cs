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
    
    private static bool _IsTileLocationWithinValidArea(Vector2Int tileAmts, Vector2Int loc)
    {
        return loc.x >= 0 && loc.x < tileAmts.x && loc.y >= 0 && loc.y < tileAmts.y;
    }

    public static List<TileScript> GetLine(TilemapScript tilemap, Vector2Int start, Vector2Int end)
    {
        if (tilemap == null) return null;

        TilesInfoList tileInfo = tilemap.tilesInfo;

        if (!_IsTileLocationWithinValidArea(tileInfo.tileAmts, start) || !_IsTileLocationWithinValidArea(tileInfo.tileAmts, end))
        {
            return null;
        }

        List<TileScript> finalArea = new List<TileScript>();

        

        finalArea.RemoveAll(t => t == null);

        return finalArea;
    }

    public static List<TileScript> GetRectangle(TilemapScript tilemap, Vector2Int topLeft, Vector2Int bottomRight)
    {
        if (tilemap == null) return null;

        TilesInfoList tileInfo = tilemap.tilesInfo;

        if (!_IsTileLocationWithinValidArea(tileInfo.tileAmts, topLeft) || !_IsTileLocationWithinValidArea(tileInfo.tileAmts, bottomRight))
        {
            return null;
        }
        
        List<TileScript> finalArea = new List<TileScript>();
        
        for (int y = topLeft.y; y <= bottomRight.y; ++y)
        {
            for (int x = topLeft.x; x <= bottomRight.x; ++x)
            {
                finalArea.Add(tilemap.GetTile(new Vector2Int(x, y)));
            }
        }

        finalArea.RemoveAll(t => t == null);

        return finalArea;
    }

    /*
    public static List<TileScript> GetStraightLine(TilemapScript tilemap, Vector2Int startPos, eDIRECTION dir, int length, int width)
    {
        List<TileScript> finalArea = new List<TileScript>();

    

        return finalArea;
    }
    */
}

