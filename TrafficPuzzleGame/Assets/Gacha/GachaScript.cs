using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GachaScript : MonoBehaviour {
  public GameObject PopCoin; //所持コイン数詳細表示用ポップアップ
  public GameObject PopList; //ガチャ内容ポップアップ
  public GameObject CoinB; //常在コイン数表示オブジェクト管理
  public GameObject PopPro; //排出確立用ポップアップ
  public GameObject ErrorMessage; //コイン不足エラーメッセージ
  public GameObject PopResult; //ガチャ結果ポップアップ単発
  public GameObject New;
  public GameObject PopResults; //10連用結果画面
  public GameObject ResultImage; //ガチャ画像
  public GameObject[] ResultImages; //10連
  public GameObject[] NewImage;
  private Sprite ResultImageSprite;

  public Text UserCoin; //常在コイン数表示用テキスト
  public Text UserCoin2; //コイン数詳細表示用テキスト
  public Text GachaResult; //獲得交換券を表示
  public Text GList; //リスト画面表示用
  public Text PList; //提供割合表示

  //読み込み関連
  private string[] textMessage; //テキストの加工前の一行を入れる変数
  private string[,] RList; //Rのリスト
  private string[,] SRList; //SRのリスト
  private string[,] SSRList; //SSRのリスト
  private string[,] proList; //確率のリスト
  private int rowLength; //テキスト内の行数を取得する変数
  private int columnLength; //テキスト内の列数を取得する変数

  int Have; //所持コイン
  int Rand; //乱数用変数
  int RL;//Rの個数
  int SRL;//SRの個数
  int SSRL;//SSrの個数
  int Ticket;
  int TicketSum;
  double Rpro;//Rの確率管理
  double SRpro;//SRの確率管理
  double SSRpro;//SSRの確率管理
  public List<bool> isRItemHaveList;
  public List<bool> isSRItemHaveList;
  public List<bool> isSSRItemHaveList;


  //ガチャ演出
  public GameObject AnimeSSR;
  public GameObject AnimeSR;
  public GameObject AnimeR;

  // Start is called before the first frame update
  void Start() {
    RList = Manager.Instance.getRList();
    SRList = Manager.Instance.getSRList();
    SSRList = Manager.Instance.getSSRList();
    isRItemHaveList = Manager.Instance.getIsRItemHaveList();
    isSRItemHaveList = Manager.Instance.getIsSRItemHaveList();
    isSSRItemHaveList = Manager.Instance.getIsSSRItemHaveList();
    Ticket = Manager.Instance.getExcangeTicket();
    RL = RList.GetLength(0);
    Debug.Log("RL:" + RL);
    SRL = SRList.GetLength(0);
    Debug.Log("SRL:" + SRL);
    SSRL = SSRList.GetLength(0);
    Debug.Log("SSRL:" + SSRL);

    ProLoad();
    PList.text += "SSR : " + proList[0, 0] + "%" + "\n" + " SR : " + proList[1, 0] + "%" + "\n" + "  R : " + proList[2, 0] + "%";
    Have = Manager.Instance.getCoinNum();
    UserCoin.text = Have.ToString() + "G";
  }



  public void ProLoad() {
    TextAsset textasset = new TextAsset(); //テキストファイルのデータを取得するインスタンスを作成
    textasset = Resources.Load("Probability", typeof(TextAsset)) as TextAsset; //Resourcesフォルダから対象テキストを取得
    string TextLines = textasset.text; //テキスト全体をstring型で入れる変数を用意して入れる

    textMessage = TextLines.Split('\n');

    //行数と列数を取得
    columnLength = textMessage[0].Split('\t').Length;
    rowLength = textMessage.Length;

    //2次配列を定義
    proList = new string[rowLength, columnLength];

    for (int i = 0; i < rowLength; i++) {

      string[] tempWords = textMessage[i].Split('\t'); //textMessageをカンマごとに分けたものを一時的にtempWordsに代入

      for (int n = 0; n < columnLength; n++) {
        proList[i, n] = tempWords[n]; //2次配列textWordsにカンマごとに分けたtempWordsを代入していく
        Debug.Log(i.ToString() + "," + n.ToString() + "," + proList[i, n]);
      }
    }
    SSRpro = double.Parse(proList[0, 0]);
    Debug.Log(SSRpro);
    SRpro = double.Parse(proList[1, 0]);
    Debug.Log(SRpro);
    Rpro = double.Parse(proList[2, 0]);
    Debug.Log(Rpro);

  }


  // Update is called once per frame
  void Update() {

  }

  public void MyCoin()//所持コイン
  {
    if (PopList.activeSelf == true || PopPro.activeSelf == true) {
      return;
    }
    Debug.Log("Click-COIN");
    CoinB.SetActive(false);
    UserCoin2.text = "無償コイン : " + UserCoin.text + "\n" + "有償コイン : 0G";
    PopCoin.SetActive(true);
  }

  public void GachaList()//内容一覧
  {
    if (PopPro.activeSelf == true || PopCoin.activeSelf == true) {
      return;
    }
    GList.text = "----SSR----" + "\n";
    for (int i = 0; i < SSRL; i++) {
      GList.text += SSRList[i, 1];
      GList.text += "   ";
      if (i % 2 == 1) GList.text += "\n";

    }
    GList.text += "-----SR----" + "\n";
    for (int i = 0; i < SRL; i++) {
      GList.text += SRList[i, 1];
      GList.text += "   ";
      if (i % 2 == 1) GList.text += "\n";
    }
    GList.text += "\n" + "------R----" + "\n";
    for (int i = 0; i < RL; i++) {
      GList.text += RList[i, 1];
      GList.text += "   ";
      if (i % 2 == 1) GList.text += "\n";
    }
    Debug.Log("Click-LIST");
    PopList.SetActive(true);
  }

  public void Probability()//確率
  {
    if (PopList.activeSelf == true || PopCoin.activeSelf == true) {
      return;
    }
    Debug.Log("Click-PROB");
    PopPro.SetActive(true);
  }

  public void Once()//単発
  {
    TicketSum = 0;
    POPResult_Close();
    Debug.Log("Click-ONCE");
    if (PopList.activeSelf == true || PopCoin.activeSelf == true || PopPro.activeSelf == true) {
      return;
    }
    if (Have < 100) {
      ErrorMessage.SetActive(true);
      return;
    }
    Have -= 100;
    UserCoin.text = Have.ToString() + "G";
    Rand = Random.Range(1, 1000);
    Debug.Log("Rand = " + Rand);
    PopResult.SetActive(true);
    ResultImage.SetActive(true);
    if (Rand <= SSRpro * 10) {
      Rand = Random.Range(0, SSRL);
      Debug.Log("Images/" + SSRList[Rand, 2] + "を読み込み");
      ResultImageSprite = Resources.Load<Sprite>("Images/" + SSRList[Rand, 2]);
      ResultImage.GetComponent<Image>().sprite = null;
      ResultImage.GetComponent<Image>().sprite = ResultImageSprite;
      AnimeSSR.SetActive(true);
      Invoke("AnimeEnd", 3.5f);
      SSRItemCheck(Rand,1, 11);
    }
    else if (Rand > SSRpro * 10 && Rand <= (SRpro + SRpro) * 10) {
      Rand = Random.Range(0, SRL);
      Debug.Log("Images/" + SRList[Rand, 2] + "を読み込み");
      ResultImageSprite = Resources.Load<Sprite>("Images/" + SRList[Rand, 2]);
      ResultImage.GetComponent<Image>().sprite = null;
      ResultImage.GetComponent<Image>().sprite = ResultImageSprite;
      AnimeSR.SetActive(true);
      Invoke("AnimeEnd", 3.5f);
      SRItemCheck(Rand,1, 11);
    }
    else {
      Rand = Random.Range(0, RL);
      Debug.Log("Images/" + RList[Rand, 2] + "を読み込み");
      ResultImageSprite = Resources.Load<Sprite>("Images/" + RList[Rand, 2]);
      ResultImage.GetComponent<Image>().sprite = null;
      ResultImage.GetComponent<Image>().sprite = ResultImageSprite;
      AnimeR.SetActive(true);
      Invoke("AnimeEnd", 3.5f);
      RItemCheck(Rand,1 , 11);
    }
    Ticket += TicketSum;
    Manager.Instance.setExcangeTicket(Ticket);
    Ticket = Manager.Instance.getExcangeTicket();
    Debug.Log("交換券所持枚数 : " + Ticket + "枚");
    Debug.Log("今回の獲得枚数 : " + TicketSum + "枚");
    GachaResult.text = "獲得交換券 : " + TicketSum;
    }

  public void TenTimes()//10連
  {
    int maxLank=2; //アニメーション演出用，最高レア度
    POPResult_Close();
    Debug.Log("Click-TENS");
    if (PopList.activeSelf == true || PopCoin.activeSelf == true || PopPro.activeSelf == true) {
      return;
    }
    if (Have < 1000) {
      ErrorMessage.SetActive(true);
      return;
    }
    Have -= 1000;
    UserCoin.text = Have.ToString() + "G";

    for (int i = 0; i < 9; i++) {
      Rand = Random.Range(1, 1000);
      Debug.Log("Rand = " + Rand);
      if (Rand <= SSRpro * 10) {
        Rand = Random.Range(0, SSRL);
        Debug.Log("Images/" + SSRList[Rand, 2] + "を読み込み");
        ResultImageSprite = Resources.Load<Sprite>("Images/" + SSRList[Rand, 2]);
        ResultImages[i].GetComponent<Image>().sprite = null;
        ResultImages[i].GetComponent<Image>().sprite = ResultImageSprite;
        maxLank = 3;
        SSRItemCheck(Rand,10, i);
      }
      else if (Rand > SSRpro * 10 && Rand <= (SSRpro + SRpro) * 10) {
        Rand = Random.Range(0, SRL);
        Debug.Log("Images/" + SRList[Rand, 2] + "を読み込み");
        ResultImageSprite = Resources.Load<Sprite>("Images/" + SRList[Rand, 2]);
        ResultImages[i].GetComponent<Image>().sprite = null;
        ResultImages[i].GetComponent<Image>().sprite = ResultImageSprite;
        SRItemCheck(Rand,10, i);
      }
      else {
        Rand = Random.Range(0, RL);
        Debug.Log("Images/" + RList[Rand, 2] + "を読み込み");
        ResultImageSprite = Resources.Load<Sprite>("Images/" + RList[Rand, 2]);
        ResultImages[i].GetComponent<Image>().sprite = null;
        ResultImages[i].GetComponent<Image>().sprite = ResultImageSprite;
        RItemCheck(Rand,10, i);
      }
    }
    Rand = Random.Range(0, 1000);
    Debug.Log("Rand = " + Rand);
    if (Rand <= SSRpro * 10) {
      Rand = Random.Range(0, SSRL);
      Debug.Log("Images/" + SSRList[Rand, 2] + "を読み込み");
      ResultImageSprite = Resources.Load<Sprite>("Images/" + SSRList[Rand, 2]);
      ResultImages[9].GetComponent<Image>().sprite = null;
      ResultImages[9].GetComponent<Image>().sprite = ResultImageSprite;
      SRItemCheck(Rand,10, 9);
    }
    else {
      Rand = Random.Range(0, SRL);
      Debug.Log("Images/" + SRList[Rand, 2] + "を読み込み");
      ResultImageSprite = Resources.Load<Sprite>("Images/" + SRList[Rand, 2]);
      ResultImages[9].GetComponent<Image>().sprite = null;
      ResultImages[9].GetComponent<Image>().sprite = ResultImageSprite;
      SRItemCheck(Rand,10, 9);
    }
    PopResult.SetActive(true);
    PopResults.SetActive(true);

    //アニメーション演出
    if (maxLank == 3) {
      AnimeSSR.SetActive(true);
    }
    else {
      AnimeSR.SetActive(true);
    }
    Invoke("AnimeEnd", 3.5f);

    Ticket += TicketSum;
    Manager.Instance.setExcangeTicket(Ticket);
    Ticket = Manager.Instance.getExcangeTicket();
    Debug.Log("交換券所持枚数 : " + Ticket + "枚");
    Debug.Log("今回の獲得枚数 : " + TicketSum + "枚");
    GachaResult.text = "獲得交換券 : " + TicketSum;
    }

  public void Pause()//ポーズ画面
  {
    SceneManager.LoadScene("TitleScene");
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
        for (int i = 0;i < 10; i++)
        {
            NewImage[i].SetActive(false);
        }
    New.SetActive(false);
  }
  //ガチャ演出終了動作
  void AnimeEnd() {
    AnimeSSR.SetActive(false);
    AnimeSR.SetActive(false);
    AnimeR.SetActive(false);
  }
  void RItemCheck(int num,int type,int ln)
  {
        if (isRItemHaveList[num] == false)
        {
            Manager.Instance.setIsRItemHave(num);
            Debug.Log("R : "+num+" をゲット");
            if (type == 1)
            {
                New.SetActive(true);
                Debug.Log("new" + "がtrue");
            }
            else
            {
                NewImage[ln].SetActive(true);
                Debug.Log(ln + "がtrue");
            }
            ReLoad();
        }
        else
        {
            TicketSum += 10;
            Debug.Log(num + "が重複");
        }
    
  }
    void SRItemCheck(int num ,int type, int ln)
    {
        if (isSRItemHaveList[num] == false)
        {
            Manager.Instance.setIsSRItemHave(num);
            Debug.Log("SR : " + num + " をゲット");
            if (type == 1)
            {
                New.SetActive(true);
                Debug.Log("new" + "がtrue");
            }
            else
            {
                NewImage[ln].SetActive(true);
                Debug.Log(ln + "がtrue");
            }
            ReLoad();
        }
        else
        {
            TicketSum += 50;
            Debug.Log(num + "が重複");
        }

    }
    void SSRItemCheck(int num, int type, int ln)
    {
        if (isSSRItemHaveList[num] == false)
        {
            Manager.Instance.setIsSSRItemHave(num);
            Debug.Log("SSR : " + num + " をゲット");
            if(type == 1){
                New.SetActive(true);
                Debug.Log("new" + "がtrue");
            }
            else
            {
                NewImage[ln].SetActive(true);
                Debug.Log(ln + "がtrue");
            }
            ReLoad();
        }
        else
        {
            TicketSum += 100;
            Debug.Log(num + "が重複");
        }

    }
    void ReLoad()
    {
        isRItemHaveList = Manager.Instance.getIsRItemHaveList();
        isSRItemHaveList = Manager.Instance.getIsSRItemHaveList();
        isSSRItemHaveList = Manager.Instance.getIsSSRItemHaveList();
    }

}
