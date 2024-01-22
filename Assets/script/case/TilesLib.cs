using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "lib", menuName = "ScriptableObjects/TilesLib", order = 1)]
public class TilesLib : ScriptableObject
{
    public string Name;
    public List<TilesColorPalit> Tiles;
}
