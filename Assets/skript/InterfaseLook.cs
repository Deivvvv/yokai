using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InterfaseLook : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{
    private CreateTerrain mapRedactor;

    void Start()
    {
        GameObject GO = GameObject.Find("system");
        mapRedactor = GO.GetComponent<CreateTerrain>();
    }
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        mapRedactor.LoadKey(true);
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        mapRedactor.LoadKey(false);
    }
}
