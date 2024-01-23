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
            Name = str;
            Point = new List<int>();
            hex = gHex;
        }
    }
    List<Hex> gamePaths;
    List<float> pathSize;


    public GameObject InfoPanel;
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
    public Tilemap ViewMap;
    public Tile ViewTile;

    public Tilemap tilemap1;
    public Tilemap tilemap2;
    public Tile[] tiles;
    Hex gHex, oldHex;

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
        oldHex = new Hex(0);
        pathSlider = new List<Slider>();
        ViewMap.SetTile(new Vector3Int(oldHex.q, oldHex.r, 0), ViewTile);
        InfoPanel.SetActive(false);
    }
    #region Looker
    bool uiLook, sliderLook, sliderLoop;

    public void UiLook(bool key)
    {

    }
    public void SliderLook(bool key)
    {

    }
    #endregion
    #region Slider
    List<int> sliderPath;
    List<Slider> pathSlider;
    void UpdateSlidder()
    {
        if (!sliderLoop)
        {
            sliderLoop = true;
            for(int i = 0; i < sliderPath.Count; i++)
            {
                if(pathSize[sliderPath[i]] != pathSlider[i].value)
                {
                    pathSize[sliderPath[i]] = pathSlider[i].value;
                    ListSlider.GetChild(i).GetChild(2).gameObject.GetComponent<Text>().text = "" + - pathSize[sliderPath[i]];
                    //pathSize[num] = ListSlider.GetChild(i).GetChild(0).gameObject.GetComponent<Slider>().value;
                    //ListSlider.GetChild(i).GetChild(2).gameObject.GetComponent<Text>().text = "" + ((plus) ? pathSize[num] : 1 - pathSize[num]);
                    //if (selectPath != num)
                    //{
                    //    selectPath = num;
                    //    selectNavPoint = ListMapP.GetChild(num);
                    //}
                    ViewPathPosition();
                }
            }

            sliderLoop = false;
        }
    }
    #endregion

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
                if (Input.GetMouseButtonDown(0) && !uiLook)
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
                if (Input.GetMouseButtonDown(0) && !uiLook)
                {
                    NewPath();
                }
                else
                //else if (Input.GetMouseButtonDown(2))
                //{
                //    SwitchMood("MovePoint");
                //}
                //else
                if (Input.GetMouseButtonDown(1))
                {
                    SwitchMood("CreatePoint");
                    pathLine.gameObject.SetActive(false);
                    InfoPanel.SetActive(false);
                }
                //RemovePath();
                break;
            case ("MovePoint"):
                if (Input.GetMouseButtonDown(0))
                    PointMove();
                else if (Input.GetMouseButtonDown(1))
                    SwitchMood("CreatePoint");
                //RemovePath();
                break;

        }
    }
    private void FixedUpdate()
    {

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos = new Vector3(mousePos.x-0.5f, mousePos.y - 0.5f, 0);
        cellPosition = WorldGrid.WorldToCell(mousePos);
        gHex = new Hex(cellPosition[0], cellPosition[1]);
        if(gHex != oldHex)
        {
            oldHex = gHex;
            CorWorld.text = $"{cellPosition}";
            ViewMap.ClearAllTiles();
            ViewMap.SetTile(new Vector3Int(oldHex.q, oldHex.r,0), ViewTile);
             // ViewMap.SetTile(Hex.Conv(oldHex), ViewTile);
             if(mood == "EditPoint")
                pathLine.SetPosition(1, cellPosition);

        }
        if (sliderLook)
            UpdateSlidder();
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
            go.transform.GetChild(2).GetComponent<Button>().onClick.RemoveAllListeners();
        }
        else
        {
            go = Instantiate(origSlider);
            pathSlider.Add(go.transform.GetChild(0).gameObject.GetComponent<Slider>());
        }

        return go;
    }

    void ViewPointPath(GamePoint p)
    {
        Debug.Log(-1);
        for (int i = ListSlider.childCount; i < p.Point.Count; i++)
        {
            void SetButton(Button button, int i)
            {
                button.onClick.AddListener(() => RemovePath(i));
            }
            GameObject go = Instantiate(origSlider);
            go.transform.SetParent(ListSlider);
            SetButton(go.transform.GetChild(1).gameObject.GetComponent<Button>(), i);
        }

        Debug.Log(-1);
        for (int i = p.Point.Count; i < ListSlider.childCount; i++)
        {
            ListSlider.GetChild(i).SetParent(StorageSlider);
        }


        Debug.Log(-1);
        for (int i = 0; i < p.Point.Count; i++)
        {
            int num = p.Point[i];
            Debug.Log(i);
            Transform transf = ListSlider.GetChild(i);

            Debug.Log(i);
            //  SetSlider(slid, i, gamePaths[num].q ==selectObject);
            pathSlider[i].value = pathSize[num];
            transf.GetChild(2).gameObject.GetComponent<Text>().text = "" + pathSize[num];
        }
    }
    void ViewPoint()
    {

        GamePoint p = GamePoints[selectObject];
        NameText.text = p.Name;
        ViewPointPath(p);
       
        /*
         загрузка информации о содержимом точки
        как минукм указывать тип авопонста и кол-во блоков дороги
         */
    }
    void EditSlider(int i, bool plus)
    {
        GamePoint p = GamePoints[selectObject];
        int num = p.Point[i];
        pathSize[num] = ListSlider.GetChild(i).GetChild(0).gameObject.GetComponent<Slider>().value;
        ListSlider.GetChild(i).GetChild(2).gameObject.GetComponent<Text>().text = ""+ ((plus) ? pathSize[num] : 1 - pathSize[num]);
        if (selectPath != num)
        {
            selectPath = num;
            selectNavPoint = ListMapP.GetChild(num);
        }
        ViewPathPosition();
    }


    void NewPoint()
    {
        int i = GamePoints.FindIndex(x => x.hex == gHex);
        if(i != -1)
        {
            SwitchMood("EditPoint");
            selectObject = i;
            ViewPoint();

            pathLine.gameObject.SetActive(true);
            pathLine.SetPosition(0, cellPosition);
            InfoPanel.SetActive(true);
        }
        else
        {
            GamePoint point = new GamePoint("point", gHex);
          //  point.CenterTayp = 0;
           // point.hex = gHex;

            GamePoints.Add(point);

            tilemap1.SetTile(new Vector3Int(oldHex.q, oldHex.r, 0)//Hex.Conv(gHex)
                , tiles[0]);
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
            tilemap1.SetTile(new Vector3Int(p.hex.q, p.hex.r,0), null);
        }
    }

    void NewPath()
    {
        int i2 = GamePoints.FindIndex(x => x.hex == gHex);
        if (i2 != -1)
        {
          //  Debug.Log($"{  GamePoints[selectObject].hex} == {gHex}");
            // GamePoint p2 = GamePoints[i2];
            GamePoint p1 = GamePoints[selectObject];
            List<int> num = new List<int>();
            for(int i = 0; i < p1.Point.Count; i++)
            {
                if(gamePaths[p1.Point[i]].q == selectObject)
                {
                    num.Add(gamePaths[p1.Point[i]].r);
                }
                else if (gamePaths[p1.Point[i]].r == selectObject)
                {

                    num.Add(gamePaths[p1.Point[i]].q);
                }
            }

            int i1 = num.FindIndex(x => x == i2);
            if(i1 == -1)
            { 
                Hex hex = new Hex(selectObject, i2);
                int i = gamePaths.FindIndex(x => x == new Hex(-1));
                if (i == -1)
                {
                    i = gamePaths.Count;
                    //hex.q = i;
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
                ViewPoint();
            }
            //   GamePoints[selectObject].Point.Add();
        }
    }

    int selectPath;
    Transform selectNavPoint;

    Vector3 Lerp(Vector3 start, Vector3 end, float percent)
    {
        return (start + percent * (end - start));
    }

    void ViewPathPosition()
    {

        GamePoint p1 = GamePoints[gamePaths[selectPath].q];
        GamePoint p2 = GamePoints[gamePaths[selectPath].r];
        Vector3Int v1 = Hex.ConV(p1.hex);
        Vector3Int v2 = Hex.ConV(p2.hex);

        Vector3 v3 = Lerp(v1, v2, pathSize[selectPath]);
        //new Vector3(v1[0] + v2[0] * pathSize[selectPath], v1[1] + v2[1] * pathSize[selectPath], 0);

        selectNavPoint.position = v3;
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
        selectObject = i;
        selectNavPoint = transf;
        ViewPathPosition();

        GamePoint p1 = GamePoints[gamePaths[i].q];
        GamePoint p2 = GamePoints[gamePaths[i].r];

        Vector3Int v1 = Hex.ConV(p1.hex);
        Vector3Int v2 = Hex.ConV(p2.hex);

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
