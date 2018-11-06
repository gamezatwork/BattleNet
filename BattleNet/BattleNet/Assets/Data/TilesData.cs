using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tiles")]
public class TilesData : ScriptableObject {

    // A list of all the possible tiles
    public List<GameObject> tileList = new List<GameObject>();

}
