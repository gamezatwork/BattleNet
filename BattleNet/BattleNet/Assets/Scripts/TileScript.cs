﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour {

    public int x = -1;
    public int y = -1;

    public TilemapScript tilemap;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Print()
    {
        Debug.Log("Tile " + x.ToString() + "," + y.ToString());
    }
}
