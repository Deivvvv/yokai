using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

class RData
{
    public List<Hex> Cor = new List<Hex>();
    public List<Hex> PotCor;
    List<int> Units = new List<int>();
    public List<Hex> UseCor = new List<Hex>();
    public RData (List<Hex> hex)
    {
        PotCor = hex;
    }
    public void SetPotencial(List<Hex> hex)
    {
        Cor = hex;
    }
   public void AddPotencial(List<Hex> hex)
    {
        for (int i=0;i< hex.Count; i++)
        {
            Debug.Log(hex[i].ToString());
            int j = PotCor.FindIndex(x => x == hex[i]);
            if(j != -1)
            {
                j = UseCor.FindIndex(x => x == hex[i]);
                if (j == -1)
                {
                    j = Cor.FindIndex(x => x == hex[i]);
                    if (j == -1)
                        Cor.Add(hex[i]);
                }
            }

        }
    }
    public Hex RUse(int unit)
    {
        Debug.Log(Cor.Count);
        int i = Random.Range(0, Cor.Count);
        Debug.Log(i);
        Hex hex = Cor[i];
        Debug.Log(hex.ToString());
        Use(hex, unit);
        Debug.Log(unit);
        return hex;
    }
    public void Use(Hex hex, int unit)
    {
        Units.Add(unit);
        UseCor.Add(hex);
        Cor.Remove(hex);
        //PotCor.Remove(hex);
    }
}
class ProdCase
{
    public int Tayp;
    List<intM>  Units;

    public ProdCase(int t, int u, int s)
    {
        Tayp = t;
        Units = new List<intM>();
        Add(u, s);

    }
    public void Add(int u, int s)
    {
        int i = Units.FindIndex(x => x.Min == u);
        if (i != -1)
            Units[i].Max += s;
        else
            Units.Add(new intM(u, s));
    }
    public void Set(int u, int s)
    {
        int i = Units.FindIndex(x => x.Min == u);
        if (i != -1)
            Units[i].Max = s;
        else
            Units.Add(new intM(u, s));
    }
}
//class Build
//{
//    public string Name;
//    public string Tayp;

//    List<ProdCase> ProdList;
//    public Build(string name)
//    {
//        Name = name;
//        ProdList = new List<ProdCase>();
//        Ressurse = new List<ResCase>();
//    }
//    public struct ResCase
//    {
//        public int Res;
//        List<string> Name;
//            int[] Size;

//        public ResCase(int r,string[] s, int[] i)
//        {
//            Res = r;
//            Name = new List<string>(s);
//            Size = i;
//        }
//    }


//    public List<ResCase> Ressurse;
//    void AddProduct(intM Tayp, int size)
//    {
//        int i = ProdList.FindIndex(x => x.Tayp == Tayp.Min);
//        if (i != 0)
//        {
//            ProdList[i].Add(Tayp.Max, size);
//        }
//        else
//            ProdList.Add(new ProdCase(Tayp.Min, Tayp.Max, size));
//        //ProdCase p = new ProdCase(Tayp);
//        //p.Size = size; 
//    }
//    void AddProd(string str)
//    {
//        string[] com = str.Split(':');
//        for(int i = 0; i < com.Length; i +=2)
//        {
//            string[] com1 = com[i+1].Split('|');
//            string[] strs = new string[com1.Length];
//            int[] size = new int[com1.Length];
//            for (int j = 0; j < com1.Length; j++)
//            {
//                string[] com2 = com1[j].Split('_');
//                strs[j] = com2[0];
//                size[j] = int.Parse( com2[1]);
//            }
//               // ResCase r = new ResCase(Storage.GetRes(com[i]), strs, size);

//        }
//    }
//}
//static class Storage
//{
//    public static List<RData> R;

//    static List< Build> unitsList;
//    #region units
//    public static  void AddBuild(Build build)
//    {
//        unitsList.Add(build);
//    }

//    #endregion
//}
class Region
{
    public string Name;

    public int Size;
    //public List<int> Chunk = new List<int>();

    List<Provinse> provinses = new List<Provinse>();
    public Region(string name)
    {
        Name = name;
    }
    public void AddProv(string tayp, string size, int pathSize)
    {
        List<string> str = new List<string>();
        List<int> id = new List<int>();
        string[] com = tayp.Split('|');
        switch (com[0])
        {
            case ("Castle"):
                {
                    str.Add("Castle"); id.Add(0);
                    str.Add("Mine|Wood"); id.Add(1);
                    str.Add("Mine|Stone"); id.Add(1);
                    str.Add("Mine|Main"); id.Add(3);
                }
                break;
            case ("Chitadel"):
                str.Add("Chitadel"); id.Add(0);
                str.Add("Mine|Wood"); id.Add(1);
                str.Add("Mine|Stone"); id.Add(1);
                break;
            case ("Fort"):
                str.Add("Fort"); id.Add(0);
                break;
            case ("Mine"):
                switch (com[1])
                {
                    case ("Main")://фракицонный ресурс
                        //str.Add("Mine|Gold"); id.Add(1);
                        break;
                    case ("NoMain")://фракицонный ресурс
                        /* пример входных данных Mine|NoMain|1 - первый герсуср вгрупее редких ресурсов, исключаая фракионный рессурс
                         * взять фракицонынй ресурс
                         * вычесть из списка ресурсов
                         * создать шахту с указанным номером редкого ресурса
                         switch(com[2]){
                        case 
                        }
                         */
                        break;
                    //case ("Gold"):
                    //    str.Add("Mine|Gold"); id.Add(1);
                    //    break;
                    //case ("Wood"):
                    //    str.Add("Mine|Wood"); id.Add(1);
                    //    break;

                    default:
                        str.Add(tayp); id.Add(1);
                        break;
                }

                break;
            case ("Train"):
                int x = 0;
                switch (com[1])
                {
                    case ("L"):
                        x = Random.Range(1, 3);
                        break;
                    case ("M"):
                        x = Random.Range(3, 5);
                        break;
                    case ("H"):
                        x = Random.Range(4, 6);
                        break;
                    default:
                        x = (int)com[1][1];
                        break;
                    case ("L1"):
                        x = 1;
                        break;
                    case ("L2"):
                        x = 2;
                        break;
                    case ("L3"):
                        x = 3;
                        break;
                    case ("L4"):
                        x = 4;
                        break;
                    case ("L5"):
                        x = 5;
                        break;
                }
                str.Add("Trainer|" + x); id.Add(5);

                break;
        }

        /* размеры провинцый
         * XL 7 - 9
         * L 5 - 7
         * M 3 - 5
         * s 1 - 3
         */
        int s = 0;
        switch (size)
        {
            case ("S"):
                s = Random.Range(1, 4);
                break;
            case ("M"):
                s = Random.Range(3, 6);
                break;
            case ("L"):
                s = Random.Range(5, 8);
                break;
            case ("XL"):
                s = Random.Range(7, 10);
                break;
        }
        Provinse prov = new Provinse(pathSize);

      //  for(int i=0;i<id.Count;i++)
           // prov.AddRR()
    }
    public void Combine()
    {
            // сортировка територии по pathsize от меньшего к большему
            // по окночанию сортировки начать составление теиторйи прикрепля их по приотирету
            //по окончанию сборки сгенерирывать мартшутные цепочки
            //по окончанию маршрутных чеочек сформировать ограничители(горы, вода, тупики)
            //финальным проходом, сформировать точки интереса и декорации
       // provinses.
    }
}
class Provinse
{
    class UnitCase
    {
        public string Name;
        public List<Hex> hex;

        public UnitCase(string name)
        {
            Name = name;
            hex = new List<Hex>();
        }
    }
    public int PathSize;
   

    public intM Position;

    List<RData> R;
    //List<string> Unit = new List<string>();
    List<UnitCase> Unit = new List<UnitCase>();

    public Provinse(int p)
    {
        PathSize = p;
        R = new List<RData>();
        RData rData = new RData(Hex.Ring(Hex.zero, 1));
        R.Add(rData);
        rData.SetPotencial(rData.PotCor);
        Debug.Log(rData.Cor.Count);
       // Position = new intM(x, y);
    }
    public void AddRR(string unit, int path)
    {
        if (path <= 0)
            path = 0;
        else
            path--;

        RData NewR()
        {
            RData rData = new RData(Hex.Ring(Hex.zero, R.Count+1));
            RData oldData = R[R.Count - 1];
            Debug.Log(oldData.UseCor.Count);
            for (int i = 0; i < oldData.UseCor.Count; i++)
                rData.AddPotencial(Hex.Ring(oldData.UseCor[i], 1));

            Debug.Log(rData.PotCor.Count);
            R.Add(rData);
            return rData;
        }

        Debug.Log(path);
        int u = Unit.FindIndex(x => x.Name == unit);
        if(u == -1)
        {
            u = Unit.Count;
            Unit.Add(new UnitCase(unit));
        }

        Debug.Log(path);
        Debug.Log(R.Count);
        if (path >= R.Count)
            path = R.Count-1;
        else if (R[path].Cor.Count == 0)
        {
            Debug.Log(path);
            if (path != 0)
                if (R[path - 1].Cor.Count != 0)
                    path--;

            Debug.Log(path);
            if (R[path].Cor.Count == 0)
            {
                    int i =path + 1;
                    for (;i<R.Count;i++)
                        if(R[i].Cor.Count>0)
                            break;
                    path = i;
            }
        }
        // else


        Debug.Log(path);
        RData rData = R[path];
        Unit[u].hex.Add(rData.RUse(u));//unit
        Debug.Log(path);
        if (path + 1 == R.Count)
            NewR();
    }
    public List<Hex> Complite()
    {
        List<Hex> hex = new List<Hex>();
        for (int i = 0; i < Unit.Count; i++)
            for (int j = 0; j < Unit[i].hex.Count; j++)
                hex.Add(Unit[i].hex[j]);
               // Debug.Log(Unit[i].hex[j].ToString());

        return hex;
    }
}

public class world : MonoBehaviour
{
    public Vector2Int ChunkSize;

    public Tile Tills;
    public Tile Tills1;
    public Tilemap map;
    public GameObject Tx;

    public int Seed;
    // Start is called before the first frame update
    void Start()
    {
        for(int i =0;i< 5;i++) 
            for (int j = 0; j < 5; j++)
                CreateProvSize(20,new Hex(i*8,j*8));
      
        //CreateProvSize(8, new Hex(10, 13));
        //CreateProvSize(20, new Hex(-9, -6));
        //{
        //    Build build = new Build("Gold Mine");
        //   // build.AddProd("Gold:Small|250_Medium|375_Large|500_Elderado|1000");
        //    Storage.AddBuild(build);
        //}

        //{
        //    List<Hex> vd =Hex.Spiral(new Hex(0, 0), 4, 4);

        //            Vector3Int[] v = new Vector3Int[vd.Count];
        //    for (int i = 0; i < vd.Count; i++)
        //    {
        //        Vector3 vs = vd[i].ToWorld();

        //        int x = 0;
        //        int y = vd[i].r;
        //        if(y != 0)
        //        {
        //            x = y / 2;
        //            if (y < 0 && (y % 2) != 0)
        //                x--;
        //        }

        //        v[i] = HexConv(vd[i]); new Vector3Int(vd[i].q +x, y, 0);
        //        GameObject Go = Instantiate(Tx);
        //        Go.transform.position = new Vector3(v[i][0], v[i][1], 0);
        //        Go.GetComponent<TextMesh>().text = v[i].ToString();
        //        //new Vector3Int(vd[i].q, vd[i].r,0);//(int)vs[0], (int)vs[2], (int)vs[1]);
        //    }
        //    //  Hex.
        //    //  StartCoroutine(Hex.Spiral(new Hex(0, 0), 3, 7));
        //    //List<RData> r = new List<RData>();
        //    //for (int i = 1; i < 4; i++)
        //    //{
        //    //    r.Add();
        //    ////}
        //    //{
        //    //    Hex h = Hex.Spiral(new Hex(0, 0), 1, 4);
        //    //    Vector3Int v =
        //    //    map.SetTiles(v.ToArray(), Tills);
        //    //}

        //    //List<Vector3Int> v = new List<Vector3Int>();
        //    // = Hex.Spiral(new Hex(0, 0), 1, 4);

        //    //    //Storage.R = r;//= cube_ring(new Vector3Int(0, 0, 0),4);
        //    Tile[] t = new Tile[v.Length];
        //    for (int i = 0; i < v.Length; i++)
        //    {
        //        t[i] = Tills;
        //    }

        //    Debug.Log(v.Length);
        //    map.SetTiles(v, t);



        //    map.SetTile(HexConv(new Hex(-4, 4)), Tills1);
        //}
    }

 

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
     обявляем матрицу 2-5 шириной. генерием персин, на основое кол-ва регионов, разделяем територии на регионы

    используя одномерынй шум для x и y кординаты прогнозируем соотвествие региональзого зонирования
    используя толщину горного массива + шум гороной толщиный образуем горы на стыке
     */

     float[] Perlin(int xSize, int ySize)
    {
        // float scale = Mathf.Sqrt(xChunk * xChunk + yChunk * yChunk);
        float[] xc = new float[xSize*ySize];

        int xOrg = (int)Random.Range(0, 1512);
        int yOrg = (int)Random.Range(0, 1512);

        //Texture2D xc = new Texture2D(xSize, ySize);

        int i = 0;
        float y = 0.0F;
        while (y < ySize)
        {
            float yCoord = yOrg + y;// / xc.height * (scale * AddScale[0]);
            float x = 0.0F;
            while (x < xSize)
            {
                float xCoord = xOrg + x;// / xc.width * (scale * AddScale[0]);

                float sample = Mathf.PerlinNoise(xCoord, yCoord);

                xc[i] = sample;
               // pix[i] = C1;
                x++;
                i++;
            }
            y++;
        }
        return xc;
    }


    void GenerateBorder()
    {

    }

    void AddRegion()//сдоет логическую область игры
    {

    }
    void CreateProvSize(int s, Hex mHex)
    {
        Provinse prov = new Provinse(s);

        for(int i =0;i< s; i++)
        {
            prov.AddRR("Land", 2);
        }
       List<Hex> hex = prov.Complite();

        Vector3Int[] v = new Vector3Int[hex.Count];
        Tile[] t = new Tile[v.Length];
        for (int i = 0; i < v.Length; i++)
        {
            Debug.Log(hex[i].ToString());
            v[i] = Hex.ConV(hex[i] + mHex);
            t[i] = Tills;
            GameObject Go = Instantiate(Tx);
            Go.transform.position = new Vector3(v[i][0] +0.5f, v[i][1] + 0.5f, 0);
            Go.GetComponent<TextMesh>().text = v[i].ToString();
        }

        map.SetTiles(v, t);
        map.SetTile(Hex.ConV( mHex), Tills1);

    }
    void CreatePath()//формирует путь до другой територии, используя указзаноое растояние от требуемых
    {

    }
    public void Generate()
    {
        if (Seed != 0)
        {
            Random.InitState(Seed);
            //Random.seed = Seed;
        }
        /*
         принемаем 0 за центр мира, размещаем в нем мироой камень, дерево или великую пропасть
         делим мир на регионы и провиныцыи входящие в него
         */
        int[] pathSize = {3,7 };//расстояние до центра мира
        int regionSize = 2;

        {
            /*
             создаем регион 
            в регионе генерируем провинции

             */

            Region reg = new Region("albedo");
            reg.AddProv("Castle", "XL",0);
            reg.AddProv("Fort", "M", 7);
            reg.AddProv("Mine|NoMain", "M", 2);
            reg.AddProv("Mine|NoMain", "S", 3);
            reg.AddProv("Mine|Gold", "M", 2);
            reg.AddProv("Mine|Gold", "M", 3);
            reg.AddProv("Train|L1", "S", 1);
            reg.AddProv("Train|Low", "M", 2);
            reg.AddProv("Train|Low", "M", 2);

        }
        

       // path  Random.Range(3, 7);

        //Texture2D xc = new Texture2D(xChunk, yChunk);
        //float[] globalMap = Perlin(ChunkSize[0], ChunkSize[1]);//3/5

        AddRegion();

        /*
         создаем регионы
        опрделяем удаленность от города к городу
        размеры регионов(вкоеиваем по одному
        кол-во важных точек
         */



        /*
         регион: розелмия
        центральная провинция (столица, где стоит замок игрока)
        лагерь (т0) , 
         */


    }
}
