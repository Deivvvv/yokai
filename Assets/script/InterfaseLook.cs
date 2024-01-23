using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InterfaseLook : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{
    public string Mood;
    private CreateTerrain mapRedactor;
    public WorldEditor worldEditor;

    void Start()
    {
        //GameObject GO = GameObject.Find("system");
        //mapRedactor = GO.GetComponent<CreateTerrain>();
    }
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if(worldEditor != null)
        switch (Mood)
        {
            case ("UiLook"):
                worldEditor.UiLook(true);
                break;
            case ("Slider"):
                worldEditor.SliderLook(true);
                break;
            }
        if (mapRedactor != null)
            mapRedactor.LoadKey(true);
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        if (worldEditor != null)
            switch (Mood)
            {
                case ("UiLook"):
                    worldEditor.UiLook(false);
                    break;
                case ("Slider"):
                    worldEditor.SliderLook(false);
                    break;
            }
        if (mapRedactor != null)
            mapRedactor.LoadKey(false);
    }
}
