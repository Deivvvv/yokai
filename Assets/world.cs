using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class Region
{
    public string Name;

    public int Size;
    public List<int> Chunk = new List<int>();
}
class Chunk
{
    public float[] data;
}
class Provinse
{
    class RData
    {
        List<intM> Cor = new List<intM>();
        List<int> Units;
    }

    public intM Position;
    
    List<RData> R;

    public Provinse(int x,int y)
    {
        Position = new intM(x, y);
    }
    public void AddR(int x, int y, int z)
    {
        if (z > R.Count+1)
            z = R.Count +1;

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
        int[] Position = { x, y };
        /* ������� ���������
         * XL 7 - 9
         * L 5 - 7
         * M 3 - 5
         * s 1 - 3
         */

    }
    void CreatePath()//��������� ���� �� ������ ���������, ��������� ���������� ��������� �� ���������
    {

    }
    public void Generate()
    {
        if (Seed != 0)
        {
            Random.seed = Seed;
        }
        /*
         ��������� 0 �� ����� ����, ��������� � ��� ������ ������, ������ ��� ������� ��������
         ����� ��� �� ������� � ���������� �������� � ����
         */
        int[] pathSize = {3,7 };//���������� �� ������ ����
        int regionSize = 2;
        

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
