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

        // If both are the same, just return the base
        if (start == end)
        {
            finalArea.Add(tilemap.GetTile(start));
            return finalArea;
        }

        // If not, then use DDA line algo

        Vector2Int delta = end - start;

        // Find out the number of steps
        int steps = 0;
        if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
        {
            steps = Mathf.Abs(delta.x);
        }
        else
        {
            steps = Mathf.Abs(delta.y);
        }

        // Find out the increments for each
        Vector2 increments = new Vector2(delta.x, delta.y) / (float)steps;

        // And then just step through em
        float currX = start.x;
        float currY = start.y;

        finalArea.Add(tilemap.GetTile(start));
        for (int i = 0; i < steps; ++i)
        {
            currX += increments.x;
            currY += increments.y;
            finalArea.Add(tilemap.GetTile(Mathf.RoundToInt(currX), Mathf.RoundToInt(currY)));
        }

        /*
        int deltaX = end.x - start.x;
        int deltaY = end.y - start.y;



        // If vertical, don't use this, just go downwards
        if (deltaX == 0)
        {
            finalArea.Add(tilemap.GetTile(start));
            for (int y = start.y; y != end.y; y += (start.y > end.y) ? (-1) : (1))
            {
                finalArea.Add(tilemap.GetTile(start.x, y));
            }
        }
        else
        {
            // If not, then use the algo
            float deltaErr = Mathf.Abs((float)deltaY / (float)deltaX);
            float err = 0.0f;
            int currY = start.y;
            
            for (int x = start.x; x != end.x; x += (start.x > end.x) ? (-1) : (1))
            {
                finalArea.Add(tilemap.GetTile(x, currY));
                err += deltaErr;
                if (err >= 0.5f)
                {
                    currY += (deltaY > 0) ? 1 : -1;
                    deltaErr -= 1.0f;
                }
                
            }
            finalArea.Add(tilemap.GetTile(end));
        }
        */
        
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

