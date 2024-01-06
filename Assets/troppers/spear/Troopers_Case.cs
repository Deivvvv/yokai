using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Troppers", menuName = "ScriptableObjects/troop", order = 1)]
public class Troopers_Case : ScriptableObject
{
    public List<Sprite> stay;
    public List<Sprite> stayFirst;

    public List<Sprite> spech;
    public List<Sprite> attak;
}
