using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;

[CustomPropertyDrawer(typeof(TilesInfoList))]
public class TilesInfoListPropertyDrawer : PropertyDrawer {

    public Vector2 tileElemSize = new Vector2(40.0f, 16.0f);

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        Vector2Int tileAmt = property.FindPropertyRelative("tileAmts").vector2IntValue;

        return (3 + tileAmt.y) * 16.0f;
        
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {


        Vector2Int tileAmt = property.FindPropertyRelative("tileAmts").vector2IntValue;
        //target
        TilesData data = property.FindPropertyRelative("tilesData").objectReferenceValue as TilesData;
        //Debug.Log(property.FindPropertyRelative("tilesData").objectReferenceValue.name);

        // Using BeginProperty / EndProperty on the parent property means that
        // prefab override logic works on the entire property.
        label = EditorGUI.BeginProperty(position, label, property);

        int oldIndentlevel = EditorGUI.indentLevel;

        // Draw the top label
        EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
        EditorGUI.indentLevel += 1;

        // Draw the tiles data
        position.height = 16.0f;
        position.y += 16;
        EditorGUI.PropertyField(position, property.FindPropertyRelative("tilesData"), new GUIContent("Data"));

        // Draw the tiles amount
        position.y += 16;
        //tileAmt = EditorGUI.Vector2IntField(position, "Size", tileAmt);
        EditorGUI.PropertyField(position, property.FindPropertyRelative("tileAmts"), new GUIContent("Size"));

        // Ok now we draw the mini tilemap
        position.y += 16;

        if (data == null)
        {
            EditorGUI.PrefixLabel(position, new GUIContent("No data found!"));
        }
        else if (tileAmt.x <= 0 || tileAmt.y <= 0 || tileAmt.x > GlobalTilemapVariables.MAXTILES.x || tileAmt.y > GlobalTilemapVariables.MAXTILES.y)
        {
            EditorGUI.PrefixLabel(position, new GUIContent("Tile amount invalid!"));
        }
        else
        {
            SerializedProperty idArrayProperty = property.FindPropertyRelative("tilesIdList");


            
            while (idArrayProperty.arraySize != (tileAmt.x * tileAmt.y))
            {
                if (idArrayProperty.arraySize < (tileAmt.x * tileAmt.y))
                {
                    idArrayProperty.InsertArrayElementAtIndex(idArrayProperty.arraySize);
                }
                else if (idArrayProperty.arraySize > (tileAmt.x * tileAmt.y))
                {
                    idArrayProperty.DeleteArrayElementAtIndex(idArrayProperty.arraySize - 1);
                }
            }
            

            List<string> possibleTileStringValues = new List<string>();
            List<int> possibleTileValues = new List<int>();
            for (int i = 0; i < data.tileList.Count; ++i)
            {
                possibleTileStringValues.Add(i.ToString());
                possibleTileValues.Add(i);
            }

            for (int y = 0; y < tileAmt.y; ++y)
            {
                for (int x = 0; x < tileAmt.x; ++x)
                {
                    Rect tileElemPosition = position;
                    tileElemPosition.x = position.x + x * tileElemSize.x;
                    tileElemPosition.y = position.y + y * tileElemSize.y;
                    tileElemPosition.width = tileElemSize.x;
                    tileElemPosition.height = tileElemSize.y;
                    //EditorGUI.IntField(tileElemPosition, GUIContent.none, 0);

                    int arrayIndex = x + y * tileAmt.x;
                    idArrayProperty.GetArrayElementAtIndex(arrayIndex).intValue = EditorGUI.IntPopup(tileElemPosition,
                                                                                                    idArrayProperty.GetArrayElementAtIndex(arrayIndex).intValue, 
                                                                                                    possibleTileStringValues.ToArray(), possibleTileValues.ToArray());
                }
            }

            
        }

        
        


        
        // Reset indent level
        EditorGUI.indentLevel = oldIndentlevel;

        EditorGUI.EndProperty();

    }
}
