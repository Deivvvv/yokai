using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


using UnityEngine.UI;

public  struct intM
{
    public int Min,Max;

    public intM(int a)
    {
        Min = Max = a;
    }
    public intM(int a, int b)
    {
        Min = a;
        Max = b;
    }
}
public class BuildConstructor : MonoBehaviour
{
    public GameObject Camera;//камера для движения по сировому пространстранству
    public Text LocationName;
    public Transform levelInfo;//кнопки для идикации активного слоя
    public class NawPoint
    {
        //глобальная точка навигации. описывает перемщение между этажами
        //под клас для хранения навигации внутри этажа
    }
    public class WorldLevel 
    {
        List<int> Layers;//id global layars
    }
    List<WorldLayer> Layers;
    WorldLevel[] world;
    List<NawPoint> path;
    Vector2Int WorldHeight = new Vector2Int(0, 5);
    int selectlayer = 0;
    int openLayer = -1;

    // Start is called before the first frame update
    void Start()
    {
        int w = 5;
        world = new WorldLevel[w];
    }

    // Update is called once per frame
    void OpenLayer()
    {
        WorldLayer lay = Layers[openLayer];
        LocationName.text = lay.Name;
        Camera.transform.position = new Vector3(lay.poz[0] + (lay.size[0]/2) , lay.poz[1] + (lay.size[1] / 2), lay.poz[2]);
        selectlayer = lay.Height;
        ViewLayer();
    }

    void Update()
    {
        Debug.Log("компануюм комнаты при помощи спеицального иснтумента с кординаой пиксельной сеткой");
        if (Input.GetKeyDown("Esc"))
        {
            if (openLayer != -1)
            {
                openLayer = -1;
                return;
            }
        }
        else if (openLayer == -1)
        {
            if (Input.GetKeyDown("Q"))
            {
                SwitchLevel(false);

            }
            else if (Input.GetKeyDown("E") )
            {
                SwitchLevel(true);
            }
        }

        if (Input.GetKeyDown("LeftArrow"))
        {
            openLayer--;
            if (openLayer < 0)
                openLayer = Layers.Count - 1;
            OpenLayer();
        }
        else if (Input.GetKeyDown("RightArrow"))
        {
            openLayer++;
            if (openLayer >= Layers.Count)
                openLayer = 0;
            OpenLayer();
        }
        else if (Input.GetMouseButtonDown(0))
        {
            void ray()
            {
                // Bit shift the index of the layer (8) to get a bit mask
                int layerMask = 1 << 8;

                // This would cast rays only against colliders in layer 8.
                // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
                layerMask = ~layerMask;

                RaycastHit hit;
                // Does the ray intersect any objects excluding the player layer
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                    Debug.Log("Did Hit");
                }
                else
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
                    Debug.Log("Did not Hit");
                }
            }
            ray();
        }
    }

    void ViewLayer()
    {
        foreach (WorldLayer lay in Layers)
        {
            if (lay.Height > selectlayer)
            {
                // прозрачность 1 -=(selectlayer-lay.Height) *0.3
            }
            else if (lay.Height < selectlayer)
            {
                // прозрачность 1 -=(lay.Height - selectlayer) *0.5
            }
            else
            {
                // прозрачность 1

            }
        }
    }
    void SwitchLevel(bool up)
    {
        if (up)
        {
            if (WorldHeight[1] < selectlayer)
                selectlayer++;
        }
        else
        {
            if (0 < selectlayer)
                selectlayer--;
        }

        ViewLayer();
    }
}


public class BuildUnit
{
    public Tilemap[] layers;

    /*
     слои 
    пол базовый
    пол декор
    мабель зад
    мебель зад декор
    зад жители (тех)
    мебель середина
    мебель перед - мебель посуда
    жители перед (тех)
    жители ходящие (тех)
     */

}

class BuildUnits
{

}
