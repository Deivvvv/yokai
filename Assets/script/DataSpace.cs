using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DataSpace
{

    static class Storage
    {
        public static List<string> LandMod;//модификаторы ресрурсов

        // public static List<sprite> ResSprite;
        public static List<string> GlobalRes;

        public static int Add(string tayp,string str)
        {
            int i = -1;
            switch (tayp)
            {
                case ("Land"):
                    i = LandMod.Count;
                    LandMod.Add(str);
                    break;
                case ("Res"):
                    i = GlobalRes.Count;
                    GlobalRes.Add(str);
                    break;
            }

            return i;
        }
        public static void Set(string tayp, List<string> str)
        {
            switch (tayp)
            {
                case ("Land"):
                    LandMod = str;
                    break;
                case ("Res"):
                    GlobalRes = str;
                    break;
            }
        }
        public static int Find(string tayp, string str)
        {
            int i = -1;
            switch (tayp)
            {
                case ("Land"):
                    i = LandMod.FindIndex(x => x == str);
                    break;
                case ("Res"):
                    i = GlobalRes.FindIndex(x => x == str);
                    break;
            }

            return i;
        }
        //public static int GetSize(string tayp)
        //{
        //    int i = -1;
        //    switch (tayp)
        //    {
        //        case ("Land"):
        //            return LandMod.Count;
        //            break;
        //        case ("Res"):
        //            return GlobalRes.Count;
        //            break;
        //    }

        //    return i;
        //}
        public static List<string> Get(string tayp)
        {
            List<string> listt = new List<string>();
            switch (tayp)
            {
                case ("Land"):
                    listt = LandMod;
                    break;
                case ("Res"):
                    listt = GlobalRes;
                    break;
            }

            return listt;
        }

    }
}
