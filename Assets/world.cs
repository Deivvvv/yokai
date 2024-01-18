using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


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
                    case ("Main")://����������� ������
                        //str.Add("Mine|Gold"); id.Add(1);
                        break;
                    case ("NoMain")://����������� ������
                        /* ������ ������� ������ Mine|NoMain|1 - ������ ������� ������� ������ ��������, ��������� ���������� �������
                         * ����� ����������� ������
                         * ������� �� ������ ��������
                         * ������� ����� � ��������� ������� ������� �������
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
                str.Add("Trainer" + x); id.Add(5);

                break;
        }

        /* ������� ���������
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

    }
    public void Combine()
    {
            // ���������� ��������� �� pathsize �� �������� � ��������
            // �� ��������� ���������� ������ ����������� �������� ��������� �� �� ����������
            //�� ��������� ������ ������������� ���������� �������
            //�� ��������� ���������� ������ ������������ ������������(����, ����, ������)
            //��������� ��������, ������������ ����� �������� � ���������
       // provinses.
    }
}
class Chunk
{
    public float[] data;
}
class Provinse
{
    public int PathSize;
    class RData
    {
        List<intM> Cor = new List<intM>();
        List<int> Units;
    }

    public intM Position;
    
    List<RData> R;

    public Provinse(int p)
    {
        PathSize = p;
       // Position = new intM(x, y);
    }
    public void AddR(int x, int y, int z)
    {
        if (z > R.Count+1)
            z = R.Count +1;

    }
    public void AddRR(int unit)
    {

    }
}

public class world : MonoBehaviour
{
    public Vector2Int ChunkSize;

    //List<>


    string[] RegionText = {"��������", "�����" };

    public int Seed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
     �������� ������� 2-5 �������. �������� ������, �� ������� ���-�� ��������, ��������� ��������� �� �������

    ��������� ���������� ��� ��� x � y ��������� ������������ ����������� ������������� �����������
    ��������� ������� ������� ������� + ��� ������� �������� �������� ���� �� �����
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

    void AddRegion()//����� ���������� ������� ����
    {

    }
    void CreateProvSize(int s, int x, int y)
    {
        Provinse prov = new Provinse(x, y);

        for(int i =0;i< s; i++)
        {
            prov.AddRR(s);
        }

    }
    void CreatePath()//��������� ���� �� ������ ���������, ��������� ���������� ��������� �� ���������
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
         ��������� 0 �� ����� ����, ��������� � ��� ������ ������, ������ ��� ������� ��������
         ����� ��� �� ������� � ���������� �������� � ����
         */
        int[] pathSize = {3,7 };//���������� �� ������ ����
        int regionSize = 2;

        {
            /*
             ������� ������ 
            � ������� ���������� ���������

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
         ������� �������
        ��������� ����������� �� ������ � ������
        ������� ��������(��������� �� ������
        ���-�� ������ �����
         */



        /*
         ������: ��������
        ����������� ��������� (�������, ��� ����� ����� ������)
        ������ (�0) , 
         */


    }
}
