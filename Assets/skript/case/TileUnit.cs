using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "set", menuName = "ScriptableObjects/tillSet", order = 1)]
public class TileUnit : ScriptableObject
{
    public List<Tile> Tiles;

}
