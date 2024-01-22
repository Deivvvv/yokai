using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldLayer 
{
    public string Name;
    public int Height;
    public Tilemap[] layers;
    public int[] wallFloor;
    //list ingameobject
    //

    public Vector3 poz;//точка в пространстве
    public Vector2Int size;//размер сетки этажа
    public List<Vector3Int> item;//кординаты и ид объекта

    public WorldLayer(Vector2Int v)
    {
        size = v;
        wallFloor = new int[v[0] * v[1]];
    }
}
