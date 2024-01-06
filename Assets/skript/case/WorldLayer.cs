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

    public Vector3 poz;//����� � ������������
    public Vector2Int size;//������ ����� �����
    public List<Vector3Int> item;//��������� � �� �������

    public WorldLayer(Vector2Int v)
    {
        size = v;
        wallFloor = new int[v[0] * v[1]];
    }
}
