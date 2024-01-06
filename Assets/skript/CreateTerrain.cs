using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
//using BuildConstructor;

public class CreateTerrain : MonoBehaviour
{
    public List<Tile> wallCase;

    public Text SizeWorld;
    public Text CorWorld;
    public Button ResetBut;
    public Button ResetButExit;

    public GameObject SizeWorldWindow;
    public GameObject OrigButton;
    public GameObject OrigButtonIcon;

    public GameObject OrigTileGrid;
    public GridLayout WorldGrid;
    public Tilemap WorldGridMap;
    public Tilemap WallMap;

    public Transform WorldLevelsWindow;
    public Transform TileGrid;
    public Transform TileGridColor;
    public Transform TileGridVar;

    public Transform ButtonStorage;

    public Color[] Colors;
    public TileBase basikTile;
    int[] selectTile = new int[4];

    public List<TilesLib> TilesLibs;
    WorldLayer sar;

    // Start is called before the first frame update
    List<Vector2Int> sizes;
    Vector3Int cellPosition;
    bool uiLook;

    public void LoadKey(bool add)
    {
        uiLook = add;
    }
    GameObject GetButton()
    {
        GameObject go = null;
        if (ButtonStorage.childCount > 0)
            go = ButtonStorage.GetChild(0).gameObject;
        else
            go = Instantiate(OrigButtonIcon);

        return go;
    }
    void Start()
    {
        void Reset()
        { 
            SizeWorldWindow.SetActive(true);
        }
        void ResetEx()
        {
            if (sar != null)
                SizeWorldWindow.SetActive(false);
        }
        ResetBut.onClick.AddListener(() => Reset());
        ResetButExit.onClick.AddListener(() => ResetEx());
       // sar.
        sizes = new List<Vector2Int>();
        sizes.Add(new Vector2Int(11, 11));
        sizes.Add(new Vector2Int(15, 15));
        sizes.Add(new Vector2Int(21, 21));

        GameObject go;

        void SetButton(Button button, int a, string mood)
        {
            switch (mood)
            {
                case ("Create"):
                    button.onClick.AddListener(() => CreateWorld(a));
                    break;
                case ("tileSet"):
                    button.onClick.AddListener(() => LoadTile(a));
                    break;
            }
        }
        for (int i =0;i<sizes.Count;i++)
        {
            go = Instantiate(OrigButton);
            go.transform.SetParent(SizeWorldWindow.transform.GetChild(1));
            SetButton(go.GetComponent<Button>(), i, "Create");
            go.transform.GetChild(0).gameObject.GetComponent<Text>().text = $"{sizes[i]}";
        }

       // WorldGridMap = new List<Tilemap>(new Tilemap[TilesLibs.Count]);
        for (int i = 0; i < TilesLibs.Count; i++)
        {
            go = Instantiate(OrigButton);
            go.transform.SetParent(WorldLevelsWindow);
            SetButton(go.GetComponent<Button>(), i, "tileSet");
            go.transform.GetChild(0).gameObject.GetComponent<Text>().text = $"{TilesLibs[i].Name}";

           // go = Instantiate(OrigTileGrid);
          //  go.transform.SetParent(WorldGrid.transform);
           // WorldGridMap[i] = go.GetComponent<Tilemap>();
        }

        LoadTile(0);
        gameObject.GetComponent<CreateTerrain>().enabled = false;
        CreateWorld(0);
    }


    void LoadTile(int a)
    {
        selectTile[0] = a;
        int tSize = TilesLibs[selectTile[0]].Tiles.Count;
        GameObject go;
        for (int i = 0; i < WorldLevelsWindow.childCount; i++)
        {
            go = WorldLevelsWindow.GetChild(i).gameObject;
            go.GetComponent<Image>().color = (a ==i)?Colors[0] : Colors[1];
        }

        void SetButton(Button button, int a)
        {
            button.onClick.AddListener(() => LoadTileColor(a));
        }
        for (int i = TileGrid.childCount; i < tSize; i++)
        {
            go = GetButton();
            go.transform.SetParent(TileGrid);
            SetButton(go.GetComponent<Button>(), i);
        }
        for (int i = tSize; i < TileGrid.childCount; i++)
        {
            go = TileGrid.GetChild(i).gameObject;
            go.transform.SetParent(ButtonStorage);
            go.GetComponent<Button>().onClick.RemoveAllListeners();
        }
        //TileGrid.gameObject.GetComponent<GridLayoutGroup>().cellSize = TilesLibs[selectTile[0]].Size
        for (int i = 0; i < tSize; i++)
        {
            go = TileGrid.GetChild(i).gameObject;
            Tile tile = TilesLibs[selectTile[0]].Tiles[i].Tiles[0].Tiles[0];
            go.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tile.sprite;
        }
        LoadTileColor(0);
    }
    void LoadTileColor(int a)
    {
        selectTile[1] = a;
        int tSize = TilesLibs[selectTile[0]].Tiles[selectTile[1]].Tiles.Count;
        GameObject go;
        for (int i = 0; i < TileGrid.childCount; i++)
        {
            go = TileGrid.GetChild(i).gameObject;
            go.GetComponent<Image>().color = (a == i) ? Colors[0] : Colors[1];
        }
        void SetButton(Button button, int a)
        {
            button.onClick.AddListener(() => LoadTileVar(a));
        }
        for (int i = TileGridColor.childCount; i < tSize; i++)
        {
            go = GetButton();
            go.transform.SetParent(TileGridColor);
            SetButton(go.GetComponent<Button>(), i);
        }
        for (int i = tSize; i < TileGridColor.childCount; i++)
        {
            go = TileGridColor.GetChild(i).gameObject;
            go.transform.SetParent(ButtonStorage);
            go.GetComponent<Button>().onClick.RemoveAllListeners();
        }
        //TileGrid.gameObject.GetComponent<GridLayoutGroup>().cellSize = TilesLibs[selectTile[0]].Size
        for (int i = 0; i < tSize; i++)
        {
            go = TileGridColor.GetChild(i).gameObject;
            Tile tile = TilesLibs[selectTile[0]].Tiles[selectTile[1]].Tiles[i].Tiles[0];
            go.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tile.sprite;
        }
        LoadTileVar(0);
    }
    void LoadTileVar(int a)
    {
        selectTile[2] = a;
        int tSize = TilesLibs[selectTile[0]].Tiles[selectTile[1]].Tiles[selectTile[2]].Tiles.Count;
        GameObject go;
        for (int i = 0; i < TileGridColor.childCount; i++)
        {
            go = TileGridColor.GetChild(i).gameObject;
            go.GetComponent<Image>().color = (a == i) ? Colors[0] : Colors[1];
        }
        void SetButton(Button button, int a)
        {
            button.onClick.AddListener(() => LoadTileUnit(a));
        }
        for (int i = TileGridVar.childCount; i < tSize; i++)
        {
            go = GetButton();
            go.transform.SetParent(TileGridVar);
            SetButton(go.GetComponent<Button>(), i);
        }
        for (int i = tSize; i < TileGridVar.childCount; i++)
        {
            go = TileGridVar.GetChild(i).gameObject;
            go.transform.SetParent(ButtonStorage);
            go.GetComponent<Button>().onClick.RemoveAllListeners();
        }
        //TileGrid.gameObject.GetComponent<GridLayoutGroup>().cellSize = TilesLibs[selectTile[0]].Size
        for (int i = 0; i < tSize; i++)
        {
            go = TileGridVar.GetChild(i).gameObject;
            Tile tile = TilesLibs[selectTile[0]].Tiles[selectTile[1]].Tiles[selectTile[2]].Tiles[i];
            go.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tile.sprite;
        }

    }
    void LoadTileUnit(int a)
    {
        selectTile[3] = a;
        GameObject go;
        for (int i = 0; i < TileGridVar.childCount; i++)
        {
            go = TileGridVar.GetChild(i).gameObject;
            go.GetComponent<Image>().color = (a == i) ? Colors[0] : Colors[1];
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!uiLook)
        {
            if (Input.GetMouseButton(0))
                EditMap(true);
            else if (Input.GetMouseButton(1))
                EditMap(false);
        }
    }
    void EditMap(bool add)
    {
        if (cellPosition[0] >= sar.size[0] ||
            cellPosition[1] >= sar.size[1] ||
            cellPosition[0] < 0 ||
            cellPosition[1] < 0)
            return;


        WorldGridMap.SetTile(cellPosition, (add)?GetTile(): basikTile);
    }

    private void FixedUpdate()
    {

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos = new Vector3(mousePos.x, mousePos.y, 0);
        cellPosition = WorldGrid.WorldToCell(mousePos);
        CorWorld.text = $"{cellPosition}";
    }

    void CreateWorld(int a)
    {
        SizeWorld.text = $"{sizes[a]}";
        SizeWorldWindow.SetActive(false);
        if(sar != null)
            for(int i = 0; i < WorldGrid.transform.childCount; i++)
            {
                WorldGridMap.ClearAllTiles();
            }
        sar = new WorldLayer(sizes[a]);
        Tilemap tm = WorldGridMap;// WorldGrid.GetChild(0).gameObject.GetComponent<Tilemap>();
        for (int i = 0; i < sar.size[0]; i++)
            for (int j = 0; j < sar.size[1]; j++)
            {
                tm.SetTile(new Vector3Int(i, j, 0), basikTile);
            }

        gameObject.GetComponent<CreateTerrain>().enabled = true;

        sar.wallFloor[0] = 1;
        sar.wallFloor[1] = 1;

        sar.wallFloor[14] = 1;
        sar.wallFloor[15] = 1;


        sar.wallFloor[34] = 1;
        sar.wallFloor[35] = 1;
        sar.wallFloor[42] = 1;
        sar.wallFloor[43] = 1;
        sar.wallFloor[44] = 1;
        sar.wallFloor[45] = 1;
        sar.wallFloor[46] = 1;


        sar.wallFloor[61] = 1;
        sar.wallFloor[sar.wallFloor.Length-1] = 1;
        ReadWall();
    }

    void ReadWall()
    {
        bool getInt(int a) { return sar.wallFloor[a] == 1; }
        int s = sar.size[0];
        int deCoder(int t, int b)
        {
            switch (b)
            {
                case (0):
                    switch (t)
                    {
                        case (0):
                            t = 1;
                            break;
                        case (1):
                            t = 4;
                            break;
                        case (2):
                            t = 2;
                            break;
                        case (3):
                            t = 10;
                            break;
                    }
                    break;
                case (1):
                    switch (t)
                    {
                        case (0):
                            t = 3;
                            break;
                        case (1):
                            t = 2;
                            break;
                        case (2):
                            t = 6;
                            break;
                        case (3):
                            t = 11;
                            break;
                    }
                    break;
                case (2):
                    switch (t)
                    {
                        case (0):
                            t = 9;
                            break;
                        case (1):
                            t = 6;
                            break;
                        case (2):
                            t = 8;
                            break;
                        case (3):
                            t = 12;
                            break;
                    }
                    break;
                case (3):
                    switch (t)
                    {
                        case (0):
                            t = 7;
                            break;
                        case (1):
                            t = 8;
                            break;
                        case (2):
                            t = 4;
                            break;
                        case (3):
                            t = 13;
                            break;
                    }
                    break;
                //case (-1):
                //    t = 0;
                //    break;
            }
            return t;
        }

        int scan(int a, int b )
        {
            int t = 5;
            bool[] cell = new bool[4];
            switch (b)
            {
                case (0):
                    cell[0] = getInt(a - 1);
                    cell[1] = getInt(a - 1 - s);
                    cell[2] = getInt(a - s);
                    break;
                case (1):
                    cell[0] = getInt(a - s);
                    cell[1] = getInt(a - s + 1);
                    cell[2] = getInt(a + 1);
                    break;
                case (2):
                    cell[0] = getInt(a + 1);
                    cell[1] = getInt(a + 1 + s);
                    cell[2] = getInt(a + s);
                    break;
                case (3):
                    cell[0] = getInt(a + s);
                    cell[1] = getInt(a + s - 1);
                    cell[2] = getInt(a - 1);
                    break;
            }
            if (!cell[0] && !cell[1] && !cell[2])  t= 0;
            else if (!cell[0]) t = 1;
            else if (!cell[2]) t= 2;
            else if (!cell[1]) t= 3;

            if (t != 5)
                t = deCoder(t,b);

            return t;
        }
        int cutScan(int a, int b, bool x)
        {
            int t = 5;
            switch (b)
            {
                case (0):
                    if (x)
                    {
                        if (!getInt(a - s))
                            t = deCoder(2,b);
                    }
                    else
                    {

                        if (!getInt(a - 1))
                            t = deCoder(1, b);
                    }
                    break;
                case (1):
                    if (x)
                    {
                        if (!getInt(a - s))
                            t = deCoder(1, b);
                    }
                    else
                    {

                        if (!getInt(a + 1))
                            t = deCoder(2, b);
                    }
                    break;
                case (2):
                    if (x)
                    {
                        if (!getInt(a + s))
                            t = deCoder(2, b);
                    }
                    else
                    {

                        if (!getInt(a +1))
                            t = deCoder(1, b);
                    }
                    break;
                case (3):
                    if (x)
                    {
                        if (!getInt(a + s))
                            t = deCoder(1, b);
                    }
                    else
                    {

                        if (!getInt(a - 1))
                            t = deCoder(2, b);
                    }
                    break;
            }

            return t;
        }

        int[] numD = new int[sar.wallFloor.Length*4]; 
        Vector3Int[] vc = new Vector3Int[sar.wallFloor.Length*4];

        //углы
        int us = s - 1;
        if (sar.wallFloor[0] == 1)
        {
            numD[0] = 5;
            numD[1] =  cutScan(0, 1, false);
            numD[2] = scan(0, 2);
            numD[3] = cutScan(0, 3, true);
        }


        if (sar.wallFloor[us] == 1)
        {
          //  int us = s - 1;
            numD[us] = cutScan(us, 0, false);
            numD[us + 1] =  5;
            numD[us + 2] = cutScan(us, 2, true);
            numD[us + 3] = scan(us, 3);
        }

        us = sar.wallFloor.Length - 1;
        if (sar.wallFloor[us] == 1)
        {
            int usl = us * 4;
            numD[usl] = scan(us, 0);
            numD[usl + 1] = cutScan(us, 1, true);
            numD[usl + 2] =  5;
            numD[usl + 3] = cutScan(us, 3, false);
        }


        us = sar.wallFloor.Length - s;
        if (sar.wallFloor[us] == 1)
        {
            int usl = us * 4;
            numD[usl] = cutScan(us, 0, true);
            numD[usl + 1] = scan(us, 1);
            numD[usl + 2] = cutScan(us, 2, false);
            numD[usl + 3] =  5;
        }


        //грани
        for (int i = 1; i < s - 1; i++)
        {
            int usl = i * 4;
            if (sar.wallFloor[i] == 1)
            {
                numD[usl] = cutScan(i, 0, false);
                numD[usl + 1] = cutScan(i, 1, false);
                numD[usl + 2] = scan(i, 2);
                numD[usl + 3] = scan(i, 3);
            }

            us = sar.wallFloor.Length - s +i;
            usl = us * 4;
            if (sar.wallFloor[us] == 1)
            {
                numD[usl] = scan(us, 0);
                numD[usl + 1] = scan(us, 1);
                numD[usl + 2] = cutScan(us, 2, false);
                numD[usl + 3] = cutScan(us, 3, false);
            }
        }


        for (int i = 1; i < sar.size[1] - 1; i++)
        {
            us = i * s;
            int usl = us  * 4;
            if (sar.wallFloor[us] == 1)
            {
                numD[usl] = cutScan(us, 0, true);
                numD[usl + 1] = scan(us, 1);
                numD[usl + 2] = scan(us, 2);
                numD[usl + 3] = cutScan(us, 3, true);
            }

            us += (s -1);
            usl = (us) * 4;
            if (sar.wallFloor[us] == 1)
            {
                numD[usl] = scan(us, 0);
                numD[usl + 1] = cutScan(us, 1, true);
                numD[usl + 2] = cutScan(us, 2, true); 
                numD[usl + 3] = scan(us, 3);
            }

        }

        //полотно
        for (int i = 0; i < s; i++)
            for (int j = 0; j < sar.size[1]; j++)
            {
                int d = i * s + j;
                if (sar.wallFloor[d] == 1)
                {
                    int usl = d * 4;
                    int[] u = new int[2];
                    u[0] = 1 + j * 2;
                    u[1] = 1 + i * 2;
                    vc[usl] = new Vector3Int(u[0], u[1], 0);
                    vc[usl + 1] = new Vector3Int(u[0] + 1, u[1], 0);
                    vc[usl + 2] = new Vector3Int(u[0] + 1, u[1] + 1, 0);
                    vc[usl + 3] = new Vector3Int(u[0], u[1] + 1, 0);
                }
            }

        for (int i = 1; i < s-1; i++)
            for (int j = 1; j <  sar.size[1]-1; j++)
            {
                int d = i * s + j;
                if (sar.wallFloor[d] == 1)
                {
                    int usl = d * 4;

                    numD[usl] = scan(d, 0);
                    numD[usl + 1] = scan(d, 1);
                    numD[usl + 2] = scan(d, 2);
                    numD[usl + 3] = scan(d, 3);
                }

            }
        TileBase[] maps = new TileBase[numD.Length];
        for (int i = 0; i < numD.Length; i++)
        {if (numD[i] != 0)
              //  Debug.Log($"{i} :{numD[i]}");
            maps[i] = wallCase[numD[i]];
        }
        WallMap.SetTiles(vc, maps);
    }

    TileBase GetTile()
    {
        return TilesLibs[selectTile[0]].Tiles[selectTile[1]].Tiles[selectTile[2]].Tiles[selectTile[3]];
    }
}





