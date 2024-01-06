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
        Debug.Log("look");
        mapRedactor.LoadKey(true);
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        Debug.Log("onlook");
        mapRedactor.LoadKey(false);
    }
}
