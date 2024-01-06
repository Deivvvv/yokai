using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "color", menuName = "ScriptableObjects/TilesColor", order = 1)]
public class TilesColorPalit : ScriptableObject
{
    public string Name;
    public List<TileUnit> Tiles;
}
