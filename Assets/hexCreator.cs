using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hexCreator : MonoBehaviour
{
    UiHexConstructor ui;
    //стандартная высота 4 чанка, стандартная ширина 4 триса(кольца), формула круга +1 +2 повтор, стандартная длина 1, чанки могут как спускатьсяю так и вощзвышаться из-за формулы трисов, где длина равна 2 ширинам триса
    #region ChunkRenderer
    public class Chunk
    {
        public ChunkTris[] ChunkTriss = new ChunkTris[6];

        public class ChunkTris
        {
            public ChinkLevel[] ChinkLevels;

            public ChunkTris (int size)
            {
                ChinkLevels = new ChinkLevel[size];
                ChinkLevels[0].Tris = new int[1];
                for(int i = 1; i < size; i++)
                {
                    ChinkLevels[i].Tris = new int[ChinkLevels[i].Tris.Length + ((i % 2 == 1)? 2:1)];

                }
            }
            public class ChinkLevel
            {
                public int[] Tris;
            }
        }
    }

    public static Vector3Int[] AXIAL_DIRECTIONS = new Vector3Int[] {
        new Vector3Int(1, 0, 0),      // 0
        new Vector3Int(0, 1, 0),      // 1
        new Vector3Int(-1, 1, 0),     // 2
        new Vector3Int(-1, 0, 0),     // 3
        new Vector3Int(0, -1, 0),     // 4
        new Vector3Int(1, -1, 0),     // 5
    };
    public static Vector3[] AXIAL_DIRECTIONS_CUSTOM;


    int worldChunkHeight = 4;
    int worldChunkSize = 4;
    float worldCellSize = 2;
    // Start is called before the first frame update
    void SetSettingWorld()
    {
        //Vector3Int[] vI = AXIAL_DIRECTIONS;
        //Vector3[] v = new Vector3[vI.Length];
        //for (int i = 0; i < v.Length; i++)
        //    v[i] =vI *( worldCellSize / 2);

        //AXIAL_DIRECTIONS_CUSTOM = v;
    }
    Vector3[] GetRootVector3(Vector3 center, int size, int side)
    {
        //расмотреть варинт без привзяки к центру
        Vector3[] v = new Vector3[size];
        Vector3 dir = AXIAL_DIRECTIONS[side];
        Vector3 curent = center + dir;
        v[0] = curent;
        for (int i = 0; i < size; i++)
            v[0] = center + dir * size;
        //{
        //    curent += dir;
        //    v[i] = curent;
        //}

        return v;
    }
    int SwitchSide(int i, int size)
    {
        int id = i + size;
        if (id > 5)
            id %= 5;//остаток от 5

        return id;

    }

    public static List<Vector3> RingFrament(Vector3 center, int radius, int side)
    {
        List<Vector3> h = new List<Vector3>();
        Vector3 current = center;// + new Vector3(0, -radius,0);
        Vector3 dir = AXIAL_DIRECTIONS[side];
        // foreach (Vector3 dir in AXIAL_DIRECTIONS)
        {
            for (int i = 0; i < radius; i++)
            {
                h.Add(current);
                // yield return current;
                current = current + dir;
            }
        }
        return h;
    }

    void CreateTrisVector( int side, Vector3 vs, int size, int height, bool even)
    {
        Vector3 AddTris(int side)
        {
            Vector3 nv = new Vector3();
            switch (size)
            {
                case (0):

                    break;
            }
            return nv;
        }
        Vector3[] v = new Vector3[3];


        if (even)
        {
            //if(side )
            side = SwitchSide(side, 3);
        }


        v[0] = vs;
        if (even)
        {
            v[1] = new Vector3(vs[0] + size, vs[1], vs[0] + height);
        }
    }

    void CreateHex(Vector3 v)
    {
        for (int i = 0; i < 6; i++)//генериуем или нет зависит от доступных ячеек
        {
            for (int j = 0; j < worldChunkSize; j++)
            {

            }
        }
    }
    void CreateWorld(int size)
    {
        SetSettingWorld();


        for (int i=0;i< size;i++)
        CreateHex(new Vector3(i,0,0));
    }
    #endregion

    #region ChunkPrototip
    void CreateVAxis(int size,int Level)
    {
        GameObject root = Instantiate(ui.OrigRoot);
        List<Vector3Int> useV = new List<Vector3Int>();
        List<Vector3Int> freeV = new List<Vector3Int>();

        Vector3Int v = new Vector3Int();
        GameObject go = Instantiate(ui.OrigHex);
        go.transform.position = v;
        go.transform.SetParent(root.transform);
        UseVector(v);

        void UseVector(Vector3Int v)
        {
            Vector3Int[] AxisVector(Vector3Int v)
            {
                Vector3Int[] vs = new Vector3Int[6];
                for (int i = 0; i < vs.Length; i++)
                {
                    vs[i] = v + AXIAL_DIRECTIONS[i];
                }

                return vs;
            }
            useV.Add(v);
            freeV.Remove(v);

            Vector3Int[] vs = AxisVector(v);
            for(int i = 0; i < vs.Length; i++)
            {
                v = vs[i];
                int id = useV.FindIndex(x=> x ==v);
                if (id == -1)
                {
                    id = freeV.FindIndex(x => x == v);
                    if (id == -1)
                        freeV.Add(v);

                }
            }
        }
        Vector3Int AddVector()
        {
            int i = Random.Range(0, freeV.Count);
            Vector3Int v = freeV[i];
            UseVector(v);
           // for(int i=0;i<freeV[i].)
            return v;
        }

         Vector3 Conv(Vector3Int v)
        {
            int y = v[1];
            float x = v[1] / 2;

            //if (y < 0 && (y % 2) != 0)
            if ((y % 2) != 0)
                x +=0.5f;

            return new Vector3(v[0] + x, y, 0);
        }

        for (int i = 0; i < size; i++)
        { 
            v =AddVector();

            go = Instantiate(ui.OrigHex);
            go.transform.position = Conv(v);
           // if(v[1] %2 !=0)
            //    go.transform.position += new Vector3(-0.5f,0,0);
            go.transform.SetParent(root.transform);
        }
        root.transform.position = new Vector3(0, 0, Level);
    }
    #endregion
    void Start()
    {
        ui = gameObject.GetComponent<UiHexConstructor>();
        for (int i = 0; i < 1; i++)
        {
            int size = Random.Range(5, 25);
            CreateVAxis(size, i);
        }
       // CreateWorld(20);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
