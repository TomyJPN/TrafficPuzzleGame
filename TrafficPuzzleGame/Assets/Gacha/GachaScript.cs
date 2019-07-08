using System.Collections;
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
    public GameObject PopResult; //ガチャ結果ポップアップ単発
    public GameObject PopResults; //10連用結果画面
    public GameObject ResultImage; //ガチャ画像
    public GameObject[] ResultImages; //10連
    private Sprite ResultImageSprite;

    public Text UserCoin; //常在コイン数表示用テキスト
    public Text UserCoin2; //コイン数詳細表示用テキスト
    public Text GachaResult; //ガチャ結果表示(仮)
    public Text GList; //リスト画面表示用
    public Text PList; //提供割合表示

    //読み込み関連
    public string[] textMessage; //テキストの加工前の一行を入れる変数
    public string[,] RList; //Rのリスト
    public string[,] SRList;　//SRのリスト
    public string[,] SSRList; //SSRのリスト
    public string[,] proList; //確率のリスト
    private int rowLength; //テキスト内の行数を取得する変数
    private int columnLength; //テキスト内の列数を取得する変数

    int Have; //所持コイン
    int Rand; //乱数用変数
    int RL;//Rの個数
    int SRL;//SRの個数
    int SSRL;//SSrの個数
    double Rpro;//Rの確率管理
    double SRpro;//SRの確率管理
    double SSRpro;//SSRの確率管理
    
    // Start is called before the first frame update
    void Start()
    {
        imageLoadR();
        imageLoadSR();
        imageLoadSSR();
        ProLoad();
        PList.text += "SSR : " + proList[0, 0] + "%" + "\n" + " SR : " + proList[1, 0] + "%" + "\n" + "  R : " + proList[2, 0] + "%";
        Have = 90;//テスト用初期コイン
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
        RL = rowLength;

        //2次配列を定義
        RList = new string[rowLength, columnLength];

        for (int i = 0; i < rowLength; i++)
        {

            string[] tempWords = textMessage[i].Split('\t'); //textMessageをカンマごとに分けたものを一時的にtempWordsに代入

            for (int n = 0; n < columnLength; n++)
            {
                RList[i, n] = tempWords[n]; //2次配列textWordsにカンマごとに分けたtempWordsを代入していく
                Debug.Log(i.ToString() + "," + n.ToString() +","+ RList[i, n]);
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
        SRL = rowLength;

        //2次配列を定義
        SRList = new string[rowLength, columnLength];

        for (int i = 0; i < rowLength; i++)
        {

            string[] tempWords = textMessage[i].Split('\t'); //textMessageをカンマごとに分けたものを一時的にtempWordsに代入

            for (int n = 0; n < columnLength; n++)
            {
                SRList[i, n] = tempWords[n]; //2次配列textWordsにカンマごとに分けたtempWordsを代入していく
                Debug.Log(i.ToString() + "," + n.ToString() +","+ SRList[i, n]);
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
        SSRL = rowLength;

        //2次配列を定義
        SSRList = new string[rowLength, columnLength];

        for (int i = 0; i < rowLength; i++)
        {

            string[] tempWords = textMessage[i].Split('\t'); //textMessageをカンマごとに分けたものを一時的にtempWordsに代入

            for (int n = 0; n < columnLength; n++)
            {
                SSRList[i, n] = tempWords[n]; //2次配列textWordsにカンマごとに分けたtempWordsを代入していく
                Debug.Log(i.ToString() + "," + n.ToString() +","+ SSRList[i, n]);
            }
        }
    }

    public void ProLoad()
    {
        TextAsset textasset = new TextAsset(); //テキストファイルのデータを取得するインスタンスを作成
        textasset = Resources.Load("Probability", typeof(TextAsset)) as TextAsset; //Resourcesフォルダから対象テキストを取得
        string TextLines = textasset.text; //テキスト全体をstring型で入れる変数を用意して入れる

        textMessage = TextLines.Split('\n');

        //行数と列数を取得
        columnLength = textMessage[0].Split('\t').Length;
        rowLength = textMessage.Length;

        //2次配列を定義
        proList = new string[rowLength, columnLength];

        for (int i = 0; i < rowLength; i++)
        {

            string[] tempWords = textMessage[i].Split('\t'); //textMessageをカンマごとに分けたものを一時的にtempWordsに代入

            for (int n = 0; n < columnLength; n++)
            {
                proList[i, n] = tempWords[n]; //2次配列textWordsにカンマごとに分けたtempWordsを代入していく
                Debug.Log(i.ToString() + "," + n.ToString() +","+ proList[i, n]);
            }
        }
        SSRpro = double.Parse(proList[0,0]);
        Debug.Log(SSRpro);
        SRpro = double.Parse(proList[1,0]);
        Debug.Log(SRpro);
        Rpro = double.Parse(proList[2, 0]);
        Debug.Log(Rpro);

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
        GList.text = "----SSR----" + "\n";
        for (int i = 0; i < SSRL ; i++) {
            GList.text += SSRList[i,1];
            GList.text += "   ";
            if(i%2 == 1) GList.text += "\n";

        }
        GList.text += "-----SR----" + "\n";
        for (int i = 0; i < SRL ; i++)
        {
            GList.text += SRList[i, 1];
            GList.text += "   ";
            if (i % 2 == 1) GList.text += "\n";
        }
        GList.text += "\n" + "------R----" + "\n";
        for (int i = 0; i < RL ; i++)
        {
            GList.text += RList[i, 1];
            GList.text += "   ";
            if (i % 2 == 1) GList.text += "\n";
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
        Debug.Log("Rand = "+Rand);
        PopResult.SetActive(true);
        ResultImage.SetActive(true);
        if(Rand <= SSRpro*10)
        {
            Rand = Random.Range(0, SSRL-1);
            Debug.Log("Images/" + SSRList[Rand, 2] + "を読み込み");
            ResultImageSprite = Resources.Load<Sprite>("Images/" + SSRList[Rand, 2]);
            ResultImage.GetComponent<Image>().sprite = null;
            ResultImage.GetComponent<Image>().sprite = ResultImageSprite;
            GachaResult.text = "1 :   SSR :" + SSRList[Rand, 1];
        }
        else if (Rand > SSRpro*10 && Rand <= (SRpro+SRpro)*10)
        {
            Rand = Random.Range(0, SRL-1);
            Debug.Log("Images/" + SRList[Rand, 2] + "を読み込み");
            ResultImageSprite = Resources.Load<Sprite>("Images/" + SRList[Rand, 2]);
            ResultImage.GetComponent<Image>().sprite = null;
            ResultImage.GetComponent<Image>().sprite = ResultImageSprite;
            GachaResult.text = "1 :   SR :" + SRList[Rand, 1];
        }
        else
        {
            Rand = Random.Range(0, RL-1);
            Debug.Log("Images/" + RList[Rand, 2] + "を読み込み");
            ResultImageSprite = Resources.Load<Sprite>("Images/" + RList[Rand,2]);
            ResultImage.GetComponent<Image>().sprite = null;
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
        
        for (int i = 0 ; i < 9; i++)
        {
            Rand = Random.Range(1, 1000);
            Debug.Log("Rand = " + Rand);
            if (Rand <= SSRpro*10)
            {
                Rand = Random.Range(0, SSRL-1);
                Debug.Log("Images/" + SSRList[Rand, 2] + "を読み込み");
                ResultImageSprite = Resources.Load<Sprite>("Images/" + SSRList[Rand, 2]);
                ResultImages[i].GetComponent<Image>().sprite = null;
                ResultImages[i].GetComponent<Image>().sprite = ResultImageSprite;
            }
            else if (Rand > SSRpro*10 && Rand <= (SSRpro+SRpro)*10)
            {
                Rand = Random.Range(0, SRL-1);
                Debug.Log("Images/" + SRList[Rand, 2] + "を読み込み");
                ResultImageSprite = Resources.Load<Sprite>("Images/" + SRList[Rand, 2]);
                ResultImages[i].GetComponent<Image>().sprite = null;
                ResultImages[i].GetComponent<Image>().sprite = ResultImageSprite;
            }
            else
            {
                Rand = Random.Range(0, RL-1);
                Debug.Log("Images/" + RList[Rand, 2] + "を読み込み");
                ResultImageSprite = Resources.Load<Sprite>("Images/" + RList[Rand, 2]);
                ResultImages[i].GetComponent<Image>().sprite = null;
                ResultImages[i].GetComponent<Image>().sprite = ResultImageSprite;
            }
        }
        Rand = Random.Range(0,1000);
        Debug.Log("Rand = " + Rand);
        if (Rand <= SSRpro*10)
        {
            Rand = Random.Range(0, SSRL-1);
            Debug.Log("Images/" + SSRList[Rand, 2] + "を読み込み");
            ResultImageSprite = Resources.Load<Sprite>("Images/" + SSRList[Rand, 2]);
            ResultImages[9].GetComponent<Image>().sprite = null;
            ResultImages[9].GetComponent<Image>().sprite = ResultImageSprite;
        }
        else
        {
            Rand = Random.Range(0, SRL-1);
            Debug.Log("Images/" + SRList[Rand, 2] + "を読み込み");
            ResultImageSprite = Resources.Load<Sprite>("Images/" + SRList[Rand, 2]);
            ResultImages[9].GetComponent<Image>().sprite = null;
            ResultImages[9].GetComponent<Image>().sprite = ResultImageSprite;
        }
        PopResult.SetActive(true);
        PopResults.SetActive(true);
        
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

    public void PopList_Close()//リストpop閉じる
    {
        Debug.Log("Close_PopList");
        PopList.SetActive(false);
    }

    public void POPPro_Close()//確立pop閉じる
    {
        Debug.Log("Close_PopProbability");
        PopPro.SetActive(false);
    }
    public void POPError_Close()//Error用Pop閉じる
    {
        Debug.Log("Error_Close");
        ErrorMessage.SetActive(false);
    }
    public void G()//テスト用コイン増殖
    {
        Debug.Log("コイン増殖");
        Have += 1000;
        UserCoin.text = Have.ToString() + "G";
    }
    public void G_Jet_Pro()//テスト用コイン消滅
    {
        Debug.Log("コイン消滅");
        Have = 0;
        UserCoin.text = Have.ToString() + "G";
    }

    public void POPResult_Close()//ガチャ結果閉じる
    {
        Debug.Log("Close_Popresult");
        PopResult.SetActive(false);
        if (ResultImage.activeSelf == true) ResultImage.SetActive(false);
        if (PopResults.activeSelf == true) PopResults.SetActive(false);
        GachaResult.text = null;
    }
}
