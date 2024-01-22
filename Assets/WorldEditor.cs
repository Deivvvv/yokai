using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class WorldEditor : MonoBehaviour
{
    class GamePoint
    {
        public Hex hex;
        public string Name;
        public int CenterTayp;
        public List<int> Point;

        public GamePoint(string str, Hex gHex)
        {
            Name = Name;
            Point = new List<int>();
            hex = gHex;
        }
    }
    List<Hex> gamePaths;
    List<float> pathSize;


    public GameObject origSlider;

    public Text MoodText;
    public InputField NameText;
    public Transform StorageSlider;


    public GameObject origNavPoint;
    public GameObject origLine;

    public Transform ListMapP;
    public Transform ListNavP;
    public Transform ListLine;
    public Transform ListSlider;


    int selectObject;
    LineRenderer pathLine;

    List<GamePoint> GamePoints = new List<GamePoint>();

    Vector3Int cellPosition;
    public Text CorWorld;
    public GridLayout WorldGrid;
    public Tilemap tilemap1;
    public Tilemap tilemap2;
    public Tile[] tiles;
    Hex gHex;

    Vector3 mousePos;
    string mood;
    // Start is called before the first frame update
    void Start()
    {
        pathLine = Instantiate(origLine).GetComponent<LineRenderer>();
        selectObject = -1;
        SwitchMood("CreatePoint");
        gamePaths = new List<Hex>();
        pathSize = new List<float>();
    }
    void SwitchMood(int i)
    {
        string str = "";
        switch (i)
        {
            case (0):
                str = "CreatePoint";
                break;
            case (1):
                str = "CreatePath";
                break;
            case (2):
                str = "EditPoint";
                break;
            case (3):
                str = "MovePoint";
                break;
        }
        SwitchMood(str);
    }
    void SwitchMood(string str)
    {
            MoodText.text = str;
        mood = str;
    }


    // Update is called once per frame
    void Update()
    {
        switch (mood)
        {
            case ("CreatePoint"):
                if (Input.GetMouseButtonDown(0))
                {
                    NewPoint();
                }
                else if (Input.GetMouseButtonDown(2))
                {
                    SwitchMood("MovePoint");
                }
                else if (Input.GetMouseButtonDown(1))
                    RemovePoint();

                    break;
            //case ("CreatePath"):
            //    if (Input.GetMouseButtonDown(0))
            //        NewPath();
            //    else if (Input.GetMouseButtonDown(1))
            //        RemovePoint();
            //    break;
            case ("EditPoint"):
                //if (Input.GetMouseButtonDown(0))
                //{
                //    NewPoint();
                //}
                //else if (Input.GetMouseButtonDown(2))
                //{
                //    SwitchMood("MovePoint");
                //}
                //else
                if (Input.GetMouseButtonDown(1))
                    SwitchMood(0);
                //RemovePath();
                break;
            case ("MovePoint"):
                if (Input.GetMouseButtonDown(0))
                    PointMove();
                else if (Input.GetMouseButtonDown(1))
                    SwitchMood(0);
                //RemovePath();
                break;

        }
    }
    private void FixedUpdate()
    {

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos = new Vector3(mousePos.x, mousePos.y, 0);
        cellPosition = WorldGrid.WorldToCell(mousePos);
        gHex = new Hex(cellPosition[0], cellPosition[1]);
        CorWorld.text = $"{cellPosition}";
        //if (pathMood)
        //{
        //    pathLine.SetPosition(1, mousePos);
        //}
    }


    GameObject GetSlider()
    {
        GameObject go = null;
        if (StorageSlider.childCount > 0)
        {
            go = StorageSlider.GetChild(0).gameObject;
            go.transform.GetChild(1).GetComponent<Slider>().onValueChanged.RemoveAllListeners();
            go.transform.GetChild(2).GetComponent<Button>().onClick.RemoveAllListeners();
        }
        else
            go = Instantiate(origSlider);


        return go;
    }

    void ViewPoint()
    {

        GamePoint p = GamePoints[selectObject];
        NameText.text = p.Name;


        for(int i = 0; i < p.Point.Count; i++)
        {
            int num = p.Point[i];
            void SetButton(Button button, int i)
            {
                button.onClick.AddListener(() => RemovePath(i));
            }
            void SetSlider(Slider slider, int i, bool plus)
            {
                slider.onValueChanged.AddListener(delegate { EditSlider(i, plus); });
            }
            GameObject go = GetSlider();
            go.transform.SetParent(ListSlider);
            SetButton(go.transform.GetChild(2).gameObject.GetComponent<Button>(),i);
            Slider slid = go.transform.GetChild(1).gameObject.GetComponent<Slider>();
            slid.value = pathSize[num];
            SetSlider(slid, i, gamePaths[num].q ==selectObject);
            go.transform.GetChild(2).gameObject.GetComponent<Text>().text = "" + pathSize[num];
        }
        /*
         загрузка информации о содержимом точки
        как минукм указывать тип авопонста и кол-во блоков дороги
         */
    }
    void EditSlider(int i, bool plus)
    {
        GamePoint p = GamePoints[selectObject];
        int num = p.Point[i];
        pathSize[num] = ListSlider.GetChild(i).GetChild(1).gameObject.GetComponent<Slider>().value;
        ListSlider.GetChild(i).GetChild(0).gameObject.GetComponent<Text>().text = ""+ ((plus) ? pathSize[num] : 1 - pathSize[num]);
    }


    void NewPoint()
    {
        int i = GamePoints.FindIndex(x => x.hex == gHex);
        if(i != -1)
        {
            SwitchMood(2);
            selectObject = i;
            ViewPoint();
        }
        else
        {
            GamePoint point = new GamePoint("point", gHex);
          //  point.CenterTayp = 0;
           // point.hex = gHex;

            GamePoints.Add(point);

            tilemap1.SetTile(Hex.Conv(gHex), tiles[0]);
            //GameObject go = Instantiate(origMapPoint);
            //go.transform.position = mousePos;
            //go.transform.SetParent(ListMapP);
        }
        
    }
    void RemovePoint()
    {
        int i = GamePoints.FindIndex(x => x.hex == gHex);
        if (i != -1)
        {
            List<Hex> list = new List<Hex>();
            List<int> num = new List<int>();
            GamePoint p = GamePoints[i];

            for (int j = 0; j < gamePaths.Count;j++) 
            {
                if (gamePaths[j].q == selectObject)
                {
                    list.Add(new Hex(j, gamePaths[j].r));
                }
                else if (gamePaths[j].r == selectObject)
                {
                    list.Add(new Hex(j, gamePaths[j].q));
                }
            }

            for(int j = 0; j < list.Count; j++)
            {
                num = new List<int>();
                GamePoint p1 = GamePoints[list[j].r];
                p1.Point.RemoveAt(list[j].q);


                gamePaths[list[j].r] = new Hex(-1);
                //for (int k = 0; k < p1.Point.Count; k++)
                //{
                //    if (p1.Point[k] == i)
                //        num.Add(k);
                //    else if (p1.Point[k] > i)
                //        p1.Point[k]--;
                //}

                //for (int k = num.Count - 1; k > -1; k--)
                //    p1.Point.RemoveAt(num[k]);

            }
            GamePoints.RemoveAt(i);
            //отчистка тайловой карты
            tilemap1.SetTile(Hex.Conv(gHex), null);
        }
    }

    void NewPath()
    {
        int i2 = GamePoints.FindIndex(x => x.hex == gHex);
        if (i2 != -1)
        {
           // GamePoint p2 = GamePoints[i2];
            GamePoint p1 = GamePoints[selectObject];
            int i1 = p1.Point.FindIndex(x => x == selectObject);
            if(i1 == -1)
            { 
                Hex hex = new Hex(i1, i2);
                int i = gamePaths.FindIndex(x => x == new Hex(-1));
                if (i == -1)
                {
                    i = gamePaths.Count;
                    gamePaths.Add(hex);
                    pathSize.Add(0.5f);
                }
                else
                {
                    gamePaths[i] = hex;
                    pathSize[i] = 0.5f;
                }

                p1.Point.Add(i);
                GamePoints[i2].Point.Add(i);

                ViewPath(i);
            }
            //   GamePoints[selectObject].Point.Add();
        }
    }
    void ViewPath(int i)
    {
        Transform transf = null;
        LineRenderer rend = null;
        if(i >= ListLine.childCount)
        {
            GameObject go = Instantiate(origNavPoint);
            go.transform.SetParent(ListNavP);
            transf = go.transform;

            go = Instantiate(origLine);
            go.transform.SetParent(ListLine);
            rend = go.GetComponent<LineRenderer>();
        }
        else
        {
            transf = ListMapP.GetChild(i);
            rend = ListLine.GetChild(i).gameObject.GetComponent<LineRenderer>();
        }
        GamePoint p1 = GamePoints[gamePaths[i].q];
        GamePoint p2 = GamePoints[gamePaths[i].r];

        Vector3Int v1 = Hex.Conv(p1.hex);
        Vector3Int v2 = Hex.Conv(p2.hex);
        Vector3 v3 = new Vector3(v1[0] + v2[0] * pathSize[i], v1[1] + v2[1] * pathSize[i], 0);

        transf.position = v3;
        rend.SetPosition(0, v1);
        rend.SetPosition(1, v2);
    }
    void RemovePath(int i)
    {
        //    GamePoint p1 = GamePoints[gamePaths[i].q];
        //    GamePoint p2 = GamePoints[gamePaths[i].r];

        GamePoint p1 = GamePoints[selectObject];
        int id = p1.Point[i];
        Hex hex = gamePaths[id];
        int x = (hex.q == selectObject) ? hex.r : hex.q;
        GamePoints[x].Point.Remove(id);
        p1.Point.RemoveAt(i);

        ListNavP.GetChild(x).gameObject.SetActive(false);
        ListLine.GetChild(x).gameObject.GetComponent<LineRenderer>().SetPositions(null);
        gamePaths[id] = new Hex(-1);
        ViewPoint();

    }

    void PointMove()
    {

    }
}
