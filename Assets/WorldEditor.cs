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

        public Hex[] NightBro;

        public LineRenderer Terrain1;
        public GamePoint(string str, Hex gHex)
        {
            Name = str;
            Point = new List<int>();
            hex = gHex;
            NightBro = new Hex[6];
            for (int i = 0; i < NightBro.Length; i++)
                NightBro[i] = new Hex(-1, 5);
        }
    }
    List<Hex> gamePaths;
    List<float> pathSize;


    public GameObject WarningCrossWindow;
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
    public List<Color> gridColor;
    List<GamePoint> GamePoints = new List<GamePoint>();

    Vector3Int cellPosition;
    public Text CorWorld;
    public GridLayout WorldGrid;
    public Tilemap ViewMap;
    public Tile ViewTile;

    public Tilemap tilemap1;
    public Tilemap tilemap2;
    public Tile[] tiles;
    Hex gHex, oldHex, lookHex;

    Vector3 vFix = new Vector3(0.5f, 0.5f, 0);
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
    bool uiLook, sliderLook, sliderLoop, delLook;

    public void UiLook(bool key)
    {
        uiLook = key;
    }
    public void SliderLook(bool key)
    {
        sliderLook = key;
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
                    ListSlider.GetChild(i).GetChild(2).gameObject.GetComponent<Text>().text = "" + pathSize[sliderPath[i]];
                    //pathSize[num] = ListSlider.GetChild(i).GetChild(0).gameObject.GetComponent<Slider>().value;
                    //ListSlider.GetChild(i).GetChild(2).gameObject.GetComponent<Text>().text = "" + ((plus) ? pathSize[num] : 1 - pathSize[num]);
                    //if (selectPath != num)
                    //{
                    //    selectPath = num;
                    //    selectNavPoint = ListMapP.GetChild(num);
                    //}
                    ViewPathPosition(i);
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
                    int i = GamePoints.FindIndex(x => x.hex == gHex);
                    if (i != -1)
                    {
                        selectObject = i;
                        Debug.Log(gHex.ToString());
                        lookHex = gHex;
                        //  ViewMap.SetTile(Hex.ConV(gHex)   , tiles[0]);
                        tilemap1.color = gridColor[1];
                        SwitchMood("MovePoint");
                    }
                }
                else if (Input.GetMouseButtonDown(1))
                    RemovePoint();

                break;
            case ("CreateRoad"):
                //if (Input.GetMouseButtonDown(0))
                //    NewPath();
                //else if (Input.GetMouseButtonDown(1))
                //    RemovePoint();
                break;
            case ("EditPoint"):
                if (Input.GetMouseButtonDown(0) && !uiLook)
                {
                    NewPath();
                }
                else
                if (Input.GetMouseButtonDown(2))
                {
                    GeneratePath();
                }
                else
                if (Input.GetMouseButtonDown(1))
                {
                    SwitchMood("CreatePoint");
                    pathLine.gameObject.SetActive(false);
                    InfoPanel.SetActive(false);
                    uiLook = false;
                    sliderLook = false;
                }
                //RemovePath();
                break;
            case ("MovePoint"):
                if (Input.GetMouseButtonDown(0))
                    SetNewPostion();
                else if (Input.GetMouseButtonDown(1))
                {

                    tilemap1.color = gridColor[0];
                    cellPosition = WorldGrid.WorldToCell(Hex.ConV(lookHex));
                    // gHex = lookHex;
                    GamePoints[selectObject].hex = lookHex;
                    ViewGhost();
                    // ViewMap.ClearAllTiles();
                    SwitchMood("CreatePoint");
                }
                //RemovePath();
                break;

        } 
    }
    private void FixedUpdate()
    {

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos = new Vector3(mousePos.x, mousePos.y, 0) - vFix;
        cellPosition = WorldGrid.WorldToCell(mousePos);
        gHex = new Hex(cellPosition[0], cellPosition[1]);
        if(gHex != oldHex)
        {
            oldHex = gHex;

            CorWorld.text = $"{cellPosition}";
            ViewMap.ClearAllTiles();
            ViewMap.SetTile(new Vector3Int(oldHex.q, oldHex.r,0), ViewTile);
            // ViewMap.SetTile(Hex.Conv(oldHex), ViewTile);
            switch (mood)
            {
                case ("EditPoint"):
                    pathLine.SetPosition(1, WorldGrid.CellToWorld(cellPosition) + vFix);
                    break;
                case ("MovePoint"):
                    GamePoints[selectObject].hex = gHex;
                    ViewGhost();
                    break;
            }

        }
        if (sliderLook)
            UpdateSlidder();
    }
    void ViewGhost()
    {
        GamePoint p = GamePoints[selectObject];
        for (int i = 0; i < p.Point.Count; i++)
        {
            int p1 = p.Point[i];
            int id = (gamePaths[p1].q == selectObject) ? 0 : 1;
            ListLine.GetChild(p1).gameObject.GetComponent<LineRenderer>().SetPosition(id, WorldGrid.CellToWorld(cellPosition) + vFix);
            ViewPathPosition(p1);
            //ListLine[p1].
        }
         //   ViewPath(p.Point[i]);

       // ViewMap.ClearAllTiles();
      //  ViewMap.SetTile(Hex.ConV(gHex)  tiles[0]);
    }

    GameObject GetSlider()
    {
        GameObject go = null;
        if (StorageSlider.childCount > 0)
        {
            go = StorageSlider.GetChild(0).gameObject;
            go.transform.GetChild(1).GetComponent<Button>().onClick.RemoveAllListeners();
        }
        else
        {
            go = Instantiate(origSlider);
            pathSlider.Add(go.transform.GetChild(0).gameObject.GetComponent<Slider>());
        }

        return go;
    }

    void GeneratePath()
    {
        GamePoint p = GamePoints[selectObject];
        for(int i=0;i< p.Point.Count; i++)
        {
            Hex h1 = GamePoints[gamePaths[p.Point[i]].q].hex;
            Hex h2 = GamePoints[gamePaths[p.Point[i]].r].hex;

            //Vector3 v = Lerp(Hex.ConV(h1), Hex.ConV(h2), 0);

            int dist =1 + h1.DistanceTo(h2);

            Debug.Log(dist);
            Vector3[] list = new Vector3[dist];
            for(int j = 0; j < list.Length; j++)
            {
                list[j] = Lerp(Hex.ConV(h1), Hex.ConV(h2), (1f *j)/dist);
                Vector3Int v = WorldGrid.LocalToCell(list[j]);
                Hex h = new Hex(v[0], v[1]);
                int z = GamePoints.FindIndex(x => x.hex == h);
                
                if(z ==-1)
                    tilemap1.SetTile(v, tiles[1]);
            }
        }
    }
    void ViewPointPath(GamePoint p)
    {
        for (int i = ListSlider.childCount; i < p.Point.Count; i++)
        {
            void SetButton(Button button, int i)
            {
                button.onClick.AddListener(() => RemovePath(i));
            }
            GameObject go = GetSlider();
            go.transform.SetParent(ListSlider);
            SetButton(go.transform.GetChild(1).gameObject.GetComponent<Button>(), i);
        }

        for (int i = p.Point.Count; i < ListSlider.childCount; i++)
        {
            ListSlider.GetChild(i).SetParent(StorageSlider);
        }


        for (int i = 0; i < p.Point.Count; i++)
        {
            int num = p.Point[i];
            Transform transf = ListSlider.GetChild(i);

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


    struct Vp
    {
        public Vector3 V;
        public int Dis;
        public Vp (Vector3 v, int d)
        {
            V = v;
            Dis = d;
        }
    }
    void NewPoint()
    {

        int GetMod(Hex p1,Hex p2)
        {
            int mod = 0;
            if (p1.q == p2.q)
                mod = (p1.r > p2.r) ? 3 : 0;
            else if (p1.q > p2.q)
                mod = (p1.r > p2.r) ? 4 : 5;
            else
                mod = (p1.r > p2.r) ? 2 : 1;
            return mod;
        }
        void ConnectMood( int j, int k)
        {
            GamePoint p1 = GamePoints[k];
            GamePoint p2 = GamePoints[j];

            int d = p1.hex.DistanceTo(p2.hex);

            int mod1 = GetMod(p1.hex, p2.hex);
            int mod2 = GetMod(p2.hex, p1.hex);


            if (p1.NightBro[mod1].q != -1)
            {
                if (d < p1.NightBro[mod1].r)
                {
                    int x = p1.NightBro[mod1].q;

                    GamePoint p3 = GamePoints[x];
                    int mod3 = GetMod(p3.hex, p2.hex);
                    int d1 = p3.hex.DistanceTo(p2.hex);
                    p3.NightBro[mod3] = new Hex(-1,5);

                    p1.NightBro[mod1] = new Hex(j, d);
                    p2.NightBro[mod2] = new Hex(k, d);
                }
            }
            else
            {
                p1.NightBro[mod1] = new Hex(j, d);
                p2.NightBro[mod2] = new Hex(k, d);
            }
        }


        int i = GamePoints.FindIndex(x => x.hex == gHex);
        if(i != -1)
        {
            SwitchMood("EditPoint");
            selectObject = i;
            sliderPath = GamePoints[i].Point;
            ViewPoint();

            pathLine.gameObject.SetActive(true);
            pathLine.SetPosition(0, cellPosition + vFix);
            InfoPanel.SetActive(true);
        }
        else
        {
           

            GamePoint point = new GamePoint("point", gHex);
            point.Terrain1 = Instantiate(origLine).GetComponent<LineRenderer>();
            int p = GamePoints.Count;
            GamePoints.Add(point);

            for (int j = 0; j < p; j++)
            {
                ConnectMood(j, p);
            }

            Vector3[] vs = new Vector3[7];
            Hex[] hex = point.hex.GetNeighbours(5);
            for (int j =0;j < 6; j++)
            {
                Debug.Log(hex[j].ToString());
                //if (point.NightBro[j].q != -1)
                //{
                //    vs[j] = Lerp(Hex.ConV(point.hex), Hex.ConV(GamePoints[point.NightBro[j].q].hex), .5f);
                //}
                //else
                //{
                //    vs[i] = Hex.ConV(hex[j]);
                //}
                vs[j] = (point.NightBro[j].q != -1) ?
                (Lerp(Hex.ConV(point.hex), Hex.ConV(GamePoints[point.NightBro[j].q].hex), .5f)) :
                Hex.ConV(hex[j]);

                vs[j] = WorldGrid.CellToWorld(new Vector3Int((int)vs[j][0], (int)vs[j][1],0));
                Debug.Log(vs[j]);
                tilemap1.SetTile(new Vector3Int((int)vs[j][0], (int)vs[j][1], 0)//Hex.Conv(gHex)
                    , tiles[0]);
            }
            vs[6] = vs[0];

            point.Terrain1.positionCount = vs.Length;
            point.Terrain1.SetPositions(vs);

            //Hex[] hex = point.hex.GetNeighbours(5);//радиус по умолчанию
            //Debug.Log(hex.Length);
            //vs = new List<Vector3>(new Vector3[7]);
            //for (int j = 0; j < hex.Length; j++)
            //{
            //    vs[j] = Hex.ConV(hex[j]);
            //    Debug.Log(vs[j]);
            //}
            //vs[6] = vs[0];
            //point.Terrain2.positionCount = 7;
            //point.Terrain2.SetPositions(vs.ToArray());


            tilemap1.SetTile(new Vector3Int(oldHex.q, oldHex.r, 0)//Hex.Conv(gHex)
                , tiles[0]);
        }
        
    }
    void RemovePoint()
    {

        if (!delLook)
        {
            delLook = true;
            int i = GamePoints.FindIndex(x => x.hex == gHex);
            if (i != -1)
            {
                List<Hex> list = new List<Hex>();
                GamePoint p = GamePoints[i];
                for (int j = 0; j < p.Point.Count; j++)
                {
                    int p1 = p.Point[j];
                    Hex hex = gamePaths[p1];
                    if (hex.q == selectObject)
                    {
                        list.Add(new Hex(p1, hex.r));
                    }
                    else if (hex.r == selectObject)
                    {
                        list.Add(new Hex(p1, hex.q));
                    }
                    gamePaths[p1] = new Hex(-1);

                    ListNavP.GetChild(p1).gameObject.SetActive(false);
                    ListLine.GetChild(p1).gameObject.SetActive(false);//.GetComponent<LineRenderer>().SetPositions(new Vector3[0]);
                }

                for (int j = 0; j < list.Count; j++)
                {
                    GamePoint p1 = GamePoints[list[j].r];
                    p1.Point.Remove(list[j].q);
                }

                for (int j = 0; j < gamePaths.Count; j++)
                {
                    Hex hex = gamePaths[j];
                    if (hex.q > i)
                        hex.q--;

                    if (hex.r > i)
                        hex.r--;
                    gamePaths[j] = hex;
                }

                GamePoints.RemoveAt(i);
                //отчистка тайловой карты
                tilemap1.SetTile(Hex.ConV(p.hex), null);
            }
            delLook = false;
        }
    }

    void NewPath()
    {
        int i2 = GamePoints.FindIndex(x => x.hex == gHex);
        if (i2 != -1)
        {
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
                    ListNavP.GetChild(i).gameObject.SetActive(false);
                    ListLine.GetChild(i).gameObject.SetActive(false);
                }

                p1.Point.Add(i);
                GamePoints[i2].Point.Add(i);

                ViewPath(i);
                ViewPoint();
            }
            //   GamePoints[selectObject].Point.Add();
        }
    }

    Vector3 Lerp(Vector3 start, Vector3 end, float percent)
    {
        return (start + percent * (end - start));
    }

    Vector3[] GetLineVector(int a)
    {
        GamePoint p1 = GamePoints[gamePaths[a].q];
        GamePoint p2 = GamePoints[gamePaths[a].r];
        Vector3[] v = new Vector3[2];

        v[0] = vFix + WorldGrid.CellToWorld(Hex.ConV(p1.hex));
        v[1] = vFix + WorldGrid.CellToWorld(Hex.ConV(p2.hex));

        //v[0] = vFix + Hex.ConV(p1.hex);
        //v[1] = vFix + Hex.ConV(p2.hex);
        return v;
    }
    void ViewPathPosition(int a )
    {
        Vector3[] v = GetLineVector(a);

        Vector3 v3 = Lerp(v[0], v[1], pathSize[a]);

        ListNavP.GetChild(a).position = v3;
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
       // selectObject = i;
        ViewPathPosition(i);
        rend.SetPositions(GetLineVector(i));
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

        ListNavP.GetChild(id).gameObject.SetActive(false);
        ListLine.GetChild(id).gameObject.SetActive(false);
        gamePaths[id] = new Hex(-1);
        ViewPoint();

    }

    void SetNewPostion()
    {

        Debug.Log(gHex.ToString() + " " + lookHex.ToString());
        //lookHex = gHex;
        GamePoint p = GamePoints[selectObject];
        if (p.hex == lookHex)
            return;

        //проверка что пути не пересекаются
        //if (!crossLook)//FindNewCross()
        //{
        //    WarningCrossWindow.SetActive(true);
        //}


        tilemap1.SetTile(new Vector3Int(lookHex.q, lookHex.r, 0), null);
        lookHex = gHex;
        tilemap1.SetTile(new Vector3Int(p.hex.q, p.hex.r, 0), tiles[0]);


    }

    class Land
    {
        public int Id;
        int[] borderSize = new int[6];
        int[] borderNightbro = new int[6];

        public Land(int id)
        {
            Id = id;
        }
    }
    void StartStage2()
    {
        int nX = 0, mX = 0, nY = 0, mY =0, sX = 0, sY = 0;
        List<Hex> borders = new List<Hex>();
        for(int i = 0; i < GamePoints.Count; i++)
        {
            GamePoint p = GamePoints[i];
            if (p.hex.q > mX)
                mX = p.hex.q;
            else if (p.hex.q < nX)
                nX = p.hex.q;

            if (p.hex.r > mY)
                mY = p.hex.r;
            else if (p.hex.r < nY)
                nY = p.hex.r;
        }

        sY = mY - nY +3;
        sX = mX - nX +3;

        Vector3Int[] world = new Vector3Int[sY*sX];
        for (int i = 0; i < world.Length; i++)
            world[i] = new Vector3Int(-1, -1, -1);

        int GetNumber(Hex hex)
        {
            int i = 1 + sX + (hex.q - nX) + (hex.r - nY) * sX;

            return i;
        }


        //s2

        //int r = 5; 
        //Land[] lands = new Land[GamePoints.Count];
        //for (int i = 0; i < GamePoints.Count; i++)
        //{
        //    GamePoint p = GamePoints[i];
        //       lands[i] = new Land(i);
        //    // List<Hex> hexs = Hex.Spiral(GamePoints[i].hex, 2, r * 2);//min -2 города не могут быть ближе чем в 2 клетки 
        //    List<int> num = new List<int>();
        //    for (int j = 0; j < GamePoints.Count; j++)
        //    {
        //        Hex hex = GamePoints[j].hex;
        //        int d = hex.DistanceTo(p.hex);
        //       // if(d <= r*2)
        //    }
        //}


        //for (int i = 0; i < GamePoints.Count; i++)
        //{
        //    GamePoint p = GamePoints[i];
        //    Hex hex = p.hex;

        //    for (int j = 0; j < GamePoints.Count; j++)
        //    {

        //    }
        //        //for (int j = 0; j < p.Point.Count; j++)
        //        //{

        //        //}

        //}

    }
}
