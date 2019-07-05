﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GachaScript : MonoBehaviour
{
    public GameObject PopCoin; //所持コイン数詳細表示用ポップアップ
    public GameObject PopList; //ガチャ内容ポップアップ
    public GameObject CoinB; //常在コイン数表示オブジェクト管理
    public GameObject PopPro; //排出確立用ポップアップ
    public GameObject ErrorMessage; //コイン不足エラーメッセージ
    public GameObject PopResult; //ガチャ結果ポップアップ
    public GameObject ResultImage; //ガチャ画像
    private Sprite ResultImageSprite;

    public Text UserCoin; //常在コイン数表示用テキスト
    public Text UserCoin2; //コイン数詳細表示用テキスト
    public Text GachaResult; //ガチャ結果表示(仮)

    //読み込み関連
    public string[] textMessage; //テキストの加工前の一行を入れる変数
    public string[,] RList; //テキストの複数列を入れる2次元は配列
    public string[,] SRList;
    public string[,] SSRList;
    private int rowLength; //テキスト内の行数を取得する変数
    private int columnLength; //テキスト内の列数を取得する変数

    int Have; //所持コイン
    int Rand; //乱数
    
    // Start is called before the first frame update
    void Start()
    {
        imageLoadR();
        imageLoadSR();
        imageLoadSSR();
        Have = 90;
        UserCoin.text = Have.ToString() + "G";
    }

    void imageLoadR()
    {
        TextAsset textasset = new TextAsset(); //テキストファイルのデータを取得するインスタンスを作成
        textasset = Resources.Load("GachaList/RList", typeof(TextAsset)) as TextAsset; //Resourcesフォルダから対象テキストを取得
        string TextLines = textasset.text; //テキスト全体をstring型で入れる変数を用意して入れる

        textMessage = TextLines.Split('\n');

        //行数と列数を取得
        columnLength = textMessage[0].Split('\t').Length;
        rowLength = textMessage.Length;

        //2次配列を定義
        RList = new string[rowLength, columnLength];

        for (int i = 0; i < rowLength; i++)
        {

            string[] tempWords = textMessage[i].Split('\t'); //textMessageをカンマごとに分けたものを一時的にtempWordsに代入

            for (int n = 0; n < columnLength; n++)
            {
                RList[i, n] = tempWords[n]; //2次配列textWordsにカンマごとに分けたtempWordsを代入していく
                Debug.Log(i.ToString() + "," + n.ToString() + RList[i, n]);
            }
        }

    }
    void imageLoadSR()
    {
        TextAsset textasset = new TextAsset(); //テキストファイルのデータを取得するインスタンスを作成
        textasset = Resources.Load("GachaList/SRList", typeof(TextAsset)) as TextAsset; //Resourcesフォルダから対象テキストを取得
        string TextLines = textasset.text; //テキスト全体をstring型で入れる変数を用意して入れる

        textMessage = TextLines.Split('\n');

        //行数と列数を取得
        columnLength = textMessage[0].Split('\t').Length;
        rowLength = textMessage.Length;

        //2次配列を定義
        SRList = new string[rowLength, columnLength];

        for (int i = 0; i < rowLength; i++)
        {

            string[] tempWords = textMessage[i].Split('\t'); //textMessageをカンマごとに分けたものを一時的にtempWordsに代入

            for (int n = 0; n < columnLength; n++)
            {
                SRList[i, n] = tempWords[n]; //2次配列textWordsにカンマごとに分けたtempWordsを代入していく
                Debug.Log(i.ToString() + "," + n.ToString() + SRList[i, n]);
            }
        }
    }
    void imageLoadSSR()
    {
        TextAsset textasset = new TextAsset(); //テキストファイルのデータを取得するインスタンスを作成
        textasset = Resources.Load("GachaList/SSRList", typeof(TextAsset)) as TextAsset; //Resourcesフォルダから対象テキストを取得
        string TextLines = textasset.text; //テキスト全体をstring型で入れる変数を用意して入れる

        textMessage = TextLines.Split('\n');

        //行数と列数を取得
        columnLength = textMessage[0].Split('\t').Length;
        rowLength = textMessage.Length;

        //2次配列を定義
        SSRList = new string[rowLength, columnLength];

        for (int i = 0; i < rowLength; i++)
        {

            string[] tempWords = textMessage[i].Split('\t'); //textMessageをカンマごとに分けたものを一時的にtempWordsに代入

            for (int n = 0; n < columnLength; n++)
            {
                SSRList[i, n] = tempWords[n]; //2次配列textWordsにカンマごとに分けたtempWordsを代入していく
                Debug.Log(i.ToString() + "," + n.ToString() + SSRList[i, n]);
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void MyCoin()//所持コイン
    {
        if(PopList.activeSelf == true || PopPro.activeSelf == true)
        {
            return;
        }
        Debug.Log("Click-COIN");
        CoinB.SetActive(false);
        UserCoin2.text = "無償コイン : " + UserCoin.text + "\n" + "有償コイン : 0G";
        PopCoin.SetActive(true);
    }

    public void GachaList()//内容一覧
    {
        if(PopPro.activeSelf == true || PopCoin.activeSelf == true)
        {
            return;
        }
        Debug.Log("Click-LIST");
        PopList.SetActive(true);
    }

    public void Probability()//確率
    {
        if (PopList.activeSelf == true || PopCoin.activeSelf == true)
        {
            return;
        }
        Debug.Log("Click-PROB");
        PopPro.SetActive(true);
    }

    public void Once()//単発
    {
        Debug.Log("Click-ONCE");
        if (PopList.activeSelf == true || PopCoin.activeSelf == true || PopPro.activeSelf == true)
        {
            return;
        }
        if(Have < 100)
        {
            ErrorMessage.SetActive(true);
            return;
        }
        Have -= 100;
        UserCoin.text = Have.ToString() + "G";
        Rand = Random.Range(1, 1000);
        PopResult.SetActive(true);
        if(Rand <= 5)
        {
            Rand = Random.Range(0, 1);
            Debug.Log("Images/" + SSRList[Rand, 2] + "を読み込み");
            ResultImageSprite = Resources.Load<Sprite>("Images/" + SSRList[Rand, 2]);
            ResultImage.GetComponent<Image>().sprite = ResultImageSprite;
            GachaResult.text = "1 :   SSR :" + SSRList[Rand, 1];
        }
        else if (Rand > 5 && Rand <= 55)
        {
            Rand = Random.Range(0, 1);
            Debug.Log("Images/" + SRList[Rand, 2] + "を読み込み");
            ResultImageSprite = Resources.Load<Sprite>("Images/" + SRList[Rand, 2]);
            ResultImage.GetComponent<Image>().sprite = ResultImageSprite;
            GachaResult.text = "1 :   SR :" + SRList[Rand, 1];
        }
        else
        {
            Rand = Random.Range(0, 26);
            Debug.Log("Images/" + RList[Rand, 2] + "を読み込み");
            ResultImageSprite = Resources.Load<Sprite>("Images/" + RList[Rand,2]);
            ResultImage.GetComponent<Image>().sprite = ResultImageSprite;
            GachaResult.text = "1 :   R :" + RList[Rand,1];
        }

    }

    public void TenTimes()//10連
    {
        Debug.Log("Click-TENS");
        if (PopList.activeSelf == true || PopCoin.activeSelf == true || PopPro.activeSelf == true)
        {
            return;
        }
        if (Have < 1000)
        {
            ErrorMessage.SetActive(true);
            return;
        }
        Have -= 1000;
        UserCoin.text = Have.ToString() + "G";
        for (int i = 1 ; i < 10; i++)
        {
            Rand = Random.Range(1, 1000);
            GachaResult.text += ( "\n" + i.ToString() + " : ");
            if (Rand <= 5)
            {
                GachaResult.text += "SSR";
            }
            else if (Rand > 5 && Rand <= 55)
            {
                GachaResult.text += " SR";
            }
            else
            {
                GachaResult.text += "  R";
            }
        }
        Rand = Random.Range(0,1000);
        GachaResult.text += ("\n" + "10" + " : ");
        if (Rand <= 5)
        {
            GachaResult.text += "SSR";
        }
        else
        {
            GachaResult.text += " SR";
        }
        PopResult.SetActive(true);
        
    }

    public void Pause()//ポーズ画面
    {
        Debug.Log("Click-PAUSE");
    }

    public void PopCoin_Close()//コイン所持数pop閉じる
    {
        Debug.Log("Close_Coin");
        PopCoin.SetActive(false);
        CoinB.SetActive(true);
    }

    public void PopList_Close()
    {
        Debug.Log("Close_PopList");
        PopList.SetActive(false);
    }

    public void POPPro_Close()
    {
        Debug.Log("Close_PopProbability");
        PopPro.SetActive(false);
    }
    public void POPError_Close()
    {
        Debug.Log("Error_Close");
        ErrorMessage.SetActive(false);
    }
    public void G()
    {
        Debug.Log("コイン増殖");
        Have += 1000;
        UserCoin.text = Have.ToString() + "G";
    }
    public void G_Jet_Pro()
    {
        Debug.Log("コイン消滅");
        Have = 0;
        UserCoin.text = Have.ToString() + "G";
    }

    public void POPResult_Close()
    {
        Debug.Log("Close_Popresult");
        PopResult.SetActive(false);
        GachaResult.text = null;
    }
}