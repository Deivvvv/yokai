using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DataSpace;

public class TableFortuna : MonoBehaviour
{
    public Sprite[] rollVi;
    public Button RollButton;
    public GameObject RollObject;
    public Transform DicesList;
     Text[] DiceText;
    Image[] DiceUI;

    public GameObject TaroObject;
    public Transform TaroList;
     Text[] TaroText;
    Text[] TaroTextNum;

    // List<Taro> taros

    int[] dice;
    // Start is called before the first frame update
    void Start()
    {
       // Storage.Start();
        dice = new int[5];
        DiceText = new Text[dice.Length];
        DiceUI = new Image[dice.Length];
        TaroText = new Text[10];
        TaroTextNum = new Text[10];
        for (int i = 0; i < dice.Length; i++)
        {
            GameObject go = Instantiate(RollObject);
            go.transform.SetParent(DicesList);
            DiceText[i] = go.transform.GetChild(0).gameObject.GetComponent<Text>();
            DiceUI[i] = go.GetComponent<Image>();
        }
        for (int i = 0; i < 10; i++)
        {
            GameObject go = Instantiate(TaroObject);
            go.transform.SetParent(TaroList);
            TaroTextNum[i] = go.transform.GetChild(0).gameObject.GetComponent<Text>();
            TaroText[i] = go.transform.GetChild(1).gameObject.GetComponent<Text>();
        }
        RollButton.onClick.AddListener(() => Roll());
        Roll();
    }

    void Roll()
    {
        for (int i = 0; i < dice.Length; i++)
        {
            dice[i] = Random.Range(0, 5);
            DiceUI[i].sprite = rollVi[dice[i]];
           // DiceText[i].text = "" + (dice[i]+1);
        }
     //   ViewTaro();
    }

    public static string NumberToRoman(int number)
    {

        if (number == 0) return "N";

        int[] values = new int[] { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };
        string[] numerals = new string[]
        { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };
        string result = "";


        for (int i = 0; i < values.Length; i++)
            while (number >= values[i])
            {
                number -= values[i];
                result +=numerals[i];
            }

        return result;
    }

    //void ViewTaro()
    //{
    //    List<int> oldNum = new List<int>();
    //    for (int i = 0; i < dice.Length; i++) 
    //    {
    //        oldNum.Add(Storage.GetTaroQ(dice[i]));
    //        oldNum.Add(Storage.GetTaroQ(dice[i]));
    //    }


    //    List<int> num = new List<int>();
    //    while (oldNum.Count > 0)
    //    {
    //        int a = Random.Range(0, oldNum.Count);
    //        num.Add(oldNum[a]);
    //        oldNum.RemoveAt(a);
    //    }
    //    string[] names = new string[num.Count];
    ////    for (int i = 0; i < names.Length; i++)
    // //       names[i] = Storage.GetTaroS(num[i]);

    //    for (int i = 0; i < TaroList.childCount; i++)
    //    {
    //        TaroTextNum[i].text = NumberToRoman(num[i] + 1);
    //        TaroText[i].text =  names[i];
    //    }
    //}
}
