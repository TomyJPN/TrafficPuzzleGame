using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scenerio : MonoBehaviour
{
    public GameObject  score_object = null;
    public string[] vs; //シナリオ格納
    public Text uiText;
    public Text nameText;
    public GameObject wakaba;
    public GameObject image;

    public string[] textMessage; //テキストの加工前の一行を入れる変数
    public string[,] textWords; //テキストの複数列を入れる2次元は配列

    private int rowLength; //テキスト内の行数を取得する変数
    private int columnLength; //テキスト内の列数を取得する変数

    int CL; //行番号

    // Start is called before the first frame update
    void Start()
    {
        CL = 0;
        TextAsset textasset = new TextAsset(); //テキストファイルのデータを取得するインスタンスを作成
        textasset = Resources.Load("Test", typeof(TextAsset)) as TextAsset; //Resourcesフォルダから対象テキストを取得
        string TextLines = textasset.text; //テキスト全体をstring型で入れる変数を用意して入れる

        textMessage = TextLines.Split('\n');

        //行数と列数を取得
        columnLength = textMessage[0].Split('\t').Length;
        rowLength = textMessage.Length;

        //2次配列を定義
        textWords = new string[rowLength, columnLength];

        for (int i = 0; i < rowLength; i++)
        {

            string[] tempWords = textMessage[i].Split('\t'); //textMessageをカンマごとに分けたものを一時的にtempWordsに代入

            for (int n = 0; n < columnLength; n++)
            {
                textWords[i, n] = tempWords[n]; //2次配列textWordsにカンマごとに分けたtempWordsを代入していく
                Debug.Log(i.ToString()+","+n.ToString()+textWords[i, n]);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (wakaba.activeSelf == false && Input.GetMouseButtonDown(0))
        {
            wakaba.SetActive(true);
            image.SetActive(true);
        }
        if (CL < rowLength && Input.GetMouseButtonDown(0))
        {
            Textupdate();
        }
    }

    void Textupdate()
    {
        Debug.Log("CL:" + CL);
        if (CL >= rowLength - 1)
        {
            End();
            return;
        }
        else
        {
            nameText.text = textWords[CL, 1];
            uiText.text = textWords[CL, 2];
            CL++;
        }
    }

    void End()
    {
        uiText.text = "The End...";
    }
}
