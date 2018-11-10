using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerScript : MonoBehaviour {

    public TilemapScript tilemap;

    bool isEndPointClicking = false;


    Vector2Int startPoint;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast (ray, out hit, 1000))
            {
                if (hit.collider.GetComponent<TileScript>())
                {
                    TileScript tile = hit.collider.GetComponent<TileScript>();
                    tile.Print();
                    if (!isEndPointClicking)
                    {
                        startPoint = new Vector2Int(tile.x, tile.y);
                        isEndPointClicking = true;
                    }
                    else
                    {
                        isEndPointClicking = false;
                        Vector2Int endPoint = new Vector2Int(tile.x, tile.y);
                        foreach(TileScript theTile in tilemap.GetTiles())
                        {
                            theTile.GetComponent<Renderer>().material.color = Color.white;
                        }
                        List<TileScript> testTiles = TargetAreaClass.GetLine(tilemap, startPoint, endPoint);
                        for (int i = 0; i < testTiles.Count; ++i)
                        {
                            testTiles[i].GetComponent<Renderer>().material.color = Color.Lerp(Color.green, Color.grey, (float)i / testTiles.Count);
                        }
                    }
                }
            }
        }
	}
}
