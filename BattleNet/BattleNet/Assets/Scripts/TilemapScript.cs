using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;

public class GlobalTilemapVariables
{
    public static Vector2Int MAXTILES = new Vector2Int(20, 20);
}

[System.Serializable]
public class TilesInfoList
{
    public Vector2Int tileAmts = new Vector2Int(1, 1);

    public TilesData tilesData;

    public List<int> tilesIdList = new List<int>();
    
}

public class TilemapScript : MonoBehaviour {

    [Header("Tile Prefab Modifications")]
    public Vector3 tileScale = new Vector3(1.0f, 0.01f, 1.0f);

    /*
    [Header("Tile Prefab")]
    public GameObject tilePrefab;
    


    [Header("Tile Amounts")]
    [Range(0, 20)]
    public int xTilesAmt = 1;
    [Range(0, 20)]
    public int yTilesAmt = 1;
    */

    [Header("Tiles Data")]
    //public TilesData tilesData;
    
    public TilesInfoList tilesInfo;

    // Contains all the tiles
    private List<GameObject> tiles = new List<GameObject>();

    // Use this for initialization
	void Start () {

        // Create the tiles
        RegenerateTiles();



        /*
        List<TileScript> testTiles = TargetAreaClass.GetRectangle(this, new Vector2Int(0, 0), GetTileAmt() - new Vector2Int(1,1));
        Debug.Log("Test 1");
        foreach (TileScript tile in testTiles)
            tile.Print();

        testTiles = TargetAreaClass.GetLine(this, new Vector2Int(4, 4), new Vector2Int(1,1));

        Debug.Log("Test 2");
        foreach (TileScript tile in testTiles)
        {
            tile.GetComponent<Renderer>().material.color = Color.green;
        }

    */



    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnDrawGizmos()
    {
        for (int y = 0; y < tilesInfo.tileAmts.y; ++y)
        {
            for (int x = 0; x < tilesInfo.tileAmts.x; ++x)
            {
                Handles.DrawWireCube(ConvertCoordToPosition(x, y), tileScale);
                Handles.Label(ConvertCoordToPosition(x, y), GetTileIDFromCoord(x, y).ToString());
            }
        }
    }

    // Call to delete all tiles and make em again
    public void RegenerateTiles()
    {
        // Destroy old tiles first
        foreach(GameObject tile in tiles)
        {
            Destroy(tile);
        }
        tiles.Clear();

        // Create the new tiles
        for (int y = 0; y < tilesInfo.tileAmts.y; ++y)
        {
            for (int x = 0; x < tilesInfo.tileAmts.x; ++x)
            {
                GameObject newTile = Instantiate(GetTilePrefabFromCoord(x,y));

                newTile.transform.SetParent(this.transform);

                newTile.transform.position = ConvertCoordToPosition(x,y);
                newTile.transform.localScale = tileScale;

                TileScript newTileScript = newTile.GetComponent<TileScript>();

                newTileScript.x = x;
                newTileScript.y = y;
                newTileScript.tilemap = this;

                tiles.Add(newTile);

            }
        }
    }

    // Converts a x,y coord into a position
    public Vector3 ConvertCoordToPosition(int x, int y)
    {
        float xPos = tileScale.x * (x + 0.5f - tilesInfo.tileAmts.x / 2.0f);
        float yPos = tileScale.z * ((tilesInfo.tileAmts.y -y) + 0.5f - tilesInfo.tileAmts.y / 2.0f);
        return new Vector3(xPos, 0, yPos);
    }

    // Gets the index for the ID list based on the x,y  coord
    public int GetTileIDIndexFromCoord(int x, int y)
    {
        return x + y * tilesInfo.tileAmts.x;
    }

    // Get the tileID based on the x,y coord
    public int GetTileIDFromCoord(int x, int y)
    {
        return tilesInfo.tilesIdList[GetTileIDIndexFromCoord(x, y)];
    }

    // Get the desired prefab based on the x,y coord
    public GameObject GetTilePrefabFromCoord(int x, int y)
    {
        int tileID = GetTileIDFromCoord(x,y);
        return tilesInfo.tilesData.tileList[tileID];
    }

    public Vector2Int GetTileAmt()
    {
        return tilesInfo.tileAmts;
    }
    
    // Call to get the tile at the coord
    public TileScript GetTile(int x, int y)
    {
        if (x < 0 || x >= tilesInfo.tileAmts.x ||
            y < 0 || y >= tilesInfo.tileAmts.y)
        {
            return null;
        }
        else
        {
            GameObject tileGO = tiles[x + (y * tilesInfo.tileAmts.x)];
            if (tileGO == null) return null;
            else return tileGO.GetComponent<TileScript>();
        }
    }
    public TileScript GetTile(Vector2Int coord)
    {
        return GetTile(coord.x, coord.y);
    }

    public List<TileScript> GetTiles()
    {
        List<TileScript> finalTiles = new List<TileScript>();
        foreach(GameObject tileGO in tiles)
        {
            finalTiles.Add(tileGO.GetComponent<TileScript>());
        }
        return finalTiles;
    }

}
