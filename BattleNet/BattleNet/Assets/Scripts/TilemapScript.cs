using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TilemapScript : MonoBehaviour {

    [Header("Tile Prefab")]
    public GameObject tilePrefab;

    [Header("Tile Amounts")]
    [Range(0, 20)]
    public int xTilesAmt = 1;
    [Range(0, 20)]
    public int yTilesAmt = 1;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
