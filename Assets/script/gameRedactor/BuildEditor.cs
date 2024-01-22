using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataSpace;

public class BuildEditor : MonoBehaviour
{
    public class BuildC
    {
        public string Name;
        List<BuildCC> Mods;

        //сумма модов
        public List<Vector2Int> Prod;
        public List<Vector3Int> UProd;
        public List<Vector3Int> Garnizon;
    }
    public class BuildCC
    {
        public bool Use;
        public bool AutoBuild;
        public string Name;
        public List<ModCase> LandMod;
        public List<ModCasePLus> BuildP;
        public List<ModCasePLus> BuildM;
        public List<Vector2Int> Prod;
        public List<Vector3Int> UProd;
        public List<Vector3Int> Garnizon;


        public List<Vector2Int> Sum;
    }

    public class ModCase
    {
        public List<int> P = new List<int>();
        public List<int> M = new List<int>();
    }
    public class ModCasePLus
    {
        public int Num;

        public List<ModCasePLus> Mod;
    }
    public BuildEditorUI ui;
    public BuildC build;

    /*
     модификатор региона
    требование к строению -= строение_1.1.(указание на мод )
     */

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ViewLand()
    {
        List<string> names = new List<string>(Storage.Get("Land").ToArray());
        List<int> nums = new List<int>();
       // for(int i=0;i<)
       // List<int> listt = Storage.Get("Land");
    }
    public void ViewBuild(BuildC build)
    {

    }
}
