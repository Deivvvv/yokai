using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class world : MonoBehaviour
{
    public Vector2Int ChunkSize;

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
     обявляем матрицу 2-5 шириной. генерием персин, на основое кол-ва регионов, разделяем територии на регионы

    используя одномерынй шум для x и y кординаты прогнозируем соотвествие региональзого зонирования
    используя толщину горного массива + шум гороной толщиный образуем горы на стыке
     */

    void Perlin(int xSize, int ySize)
    {
       // float scale = Mathf.Sqrt(xChunk * xChunk + yChunk * yChunk);

        int xOrg = (int)Random.Range(0, 1512);
        int yOrg = (int)Random.Range(0, 1512);

        Texture2D xc = new Texture2D(xSize, ySize);

        int i = 0;
        float y = 0.0F;
        while (y < xc.height)
        {
            float x = 0.0F;
            while (x < xc.width)
            {
                float xCoord = xOrg + x;// / xc.width * (scale * AddScale[0]);
                float yCoord = yOrg + y;// / xc.height * (scale * AddScale[0]);

                float sample = Mathf.PerlinNoise(xCoord, yCoord);
                Vector3Int D = new Vector3Int((int)x, (int)y, 50);
                Color C1 = new Color(WorldWater + 1, 0, 0);

                AddTile(0, 0, WorldWater, D);
                if (sample > 0.7f)
                {

                }
                else
                {
                    AddTile(1, 1, WorldBiom, D);
                    C1 = new Color(WorldWater + 1, (WorldBiom) + 1, 0);
                    // C1 = new Color(0, WorldBiom + 1, 0);
                }

                pix[i] = C1;
                x++;
                i++;
            }
            y++;
        }

    }
    public void Generate()
    {
        for (int iz = 0; iz < mapData.level.Length; iz++)
        {
            mapData.level[iz].ClearAllTiles();
        }
        int cof1 = 0;

        if (Seed != 0)
        {
            Random.seed = Seed;
        }

        if (WorldWater == -1)
        {
            cof1 = Random.Range(0, mapData.DataTile[0].Data.Length);
            WorldWater = cof1;
        }

        if (WorldBiom == -1)
        {
            cof1 = Random.Range(0, mapData.DataTile[1].Data.Length);
            WorldBiom = cof1;
        }

        // AddBiom = new int[2];
        bool turn = false;
        //for (int ix = 0; ix < AddBiom.Length; ix++)
        //{
        //    AddBiom[ix] = -1;
        //}
        if (AddBiom[0] == -1)
        {
            while (turn == false)
            {
                cof1 = Random.Range(0, mapData.DataTile[1].Data.Length);
                if (cof1 != WorldBiom)
                {
                    AddBiom[0] = cof1;
                    turn = true;
                }
            }
            turn = false;
        }
        if (AddBiom[1] == -1)
        {
            while (turn == false)
            {
                cof1 = Random.Range(0, mapData.DataTile[1].Data.Length);
                if (cof1 != WorldBiom)
                {
                    if (AddBiom[0] != cof1)
                    {

                        AddBiom[1] = cof1;
                        turn = true;
                    }
                }
            }
        }

        Texture2D xc = new Texture2D(xChunk, yChunk);
        //Texture2D xc1 = new Texture2D(xChunk, yChunk);
        //Texture2D xc2 = new Texture2D(xChunk, yChunk);
        //  xc.filterMode = FilterMode.Point;
        Color[] pix = new Color[xc.width * xc.height];
        Color[] pix1 = new Color[xc.width * xc.height];
        Color[] pix2 = new Color[xc.width * xc.height];






        float scale = Mathf.Sqrt(xChunk * xChunk + yChunk * yChunk);


        //WaterGen
        int xOrg = (int)Random.Range(0, 1512);
        int yOrg = (int)Random.Range(0, 1512);

        int i = 0;
        float y = 0.0F;
        while (y < xc.height)
        {
            float x = 0.0F;
            while (x < xc.width)
            {
                float xCoord = xOrg + x / xc.width * (scale * AddScale[0]);
                float yCoord = yOrg + y / xc.height * (scale * AddScale[0]);

                float sample = Mathf.PerlinNoise(xCoord, yCoord);
                Vector3Int D = new Vector3Int((int)x, (int)y, 50);
                Color C1 = new Color(WorldWater + 1, 0, 0);

                AddTile(0, 0, WorldWater, D);
                if (sample > 0.7f)
                {

                }
                else
                {
                    AddTile(1, 1, WorldBiom, D);
                    C1 = new Color(WorldWater + 1, (WorldBiom) + 1, 0);
                    // C1 = new Color(0, WorldBiom + 1, 0);
                }

                pix[i] = C1;
                x++;
                i++;
            }
            y++;
        }

        //Add Biom
        xOrg = (int)Random.Range(0, 1512);
        yOrg = (int)Random.Range(0, 1512);

        i = 0;
        y = 0.0F;
        while (y < xc.height)
        {
            float x = 0.0F;
            while (x < xc.width)
            {
                float xCoord = xOrg + x / xc.width * (scale * AddScale[1]);
                float yCoord = yOrg + y / xc.height * (scale * AddScale[1]);

                float sample = Mathf.PerlinNoise(xCoord, yCoord);

                Color C1 = new Color(pix[i].r, pix[i].g, 0);
                if (pix[i].g > 0)
                {
                    Vector3Int D = new Vector3Int((int)x, (int)y, 50);
                    if (sample > 0.7f)
                    {
                        AddTile(2, 1, AddBiom[0], D);

                        C1 = new Color(pix[i].r, pix[i].g, (AddBiom[0] + 1f));
                    }
                    else if (sample < 0.3f)
                    {
                        AddTile(2, 1, AddBiom[1], D);
                        C1 = new Color(pix[i].r, pix[i].g, (AddBiom[1] + 1f));
                    }
                }
                pix[i] = C1;
                x++;
                i++;
            }
            y++;
        }
        xc.SetPixels(pix);
        xc.Apply();

        //forest genertion
        i = 0;
        y = 0.0F;
        while (y < xc.height)
        {
            float x = 0.0F;
            while (x < xc.width)
            {
                float xCoord = xOrg + x / xc.width * (scale * AddScale[2]);
                float yCoord = yOrg + y / xc.height * (scale * AddScale[2]);

                float sample = Mathf.PerlinNoise(xCoord, yCoord);

                Color C1 = new Color(0, 0, 0);
                if (pix[i].g > 0)
                {
                    if (sample > AllForestCof)
                    {

                        Vector3Int D = new Vector3Int((int)x, (int)y, 50);
                        if (pix[i].b == 0)
                        {
                            cof1 = (int)Random.Range(50 * ForestCof[WorldBiom], 100);


                            if (50 < cof1)
                            {
                                // AddForest(WorldBiom, D);

                                C1 = new Color((WorldBiom + 1f), 0, 0);
                            }

                        }
                        else
                        {
                            int id = (int)(pix[i].b - 1);

                            cof1 = (int)Random.Range(50 * ForestCof[id], 100);

                            if (50 < cof1)
                            {
                                C1 = new Color(pix[i].b, 0, 0);
                                //Debug.Log(pix[i]);
                                //Debug.Log(C1);
                                //Debug.Log((pix[i].b) * 255);
                                //Debug.Log(id);
                                //AddForest(id, D);
                            }
                        }
                    }
                }

                pix1[i] = C1;
                x++;
                i++;
            }
            y++;
        }
        //xc1.SetPixels(pix);
        //xc1.Apply();


        gameObject.GetComponent<MapRedactor>().TranfMap(WorldBiom, pix, pix1, pix2, xChunk);

        //for (int iz = 0; iz < mapData.level.Length; iz++)
        //{

        //    mapData.level[iz].RefreshAllTiles();
        //}
    }
}
