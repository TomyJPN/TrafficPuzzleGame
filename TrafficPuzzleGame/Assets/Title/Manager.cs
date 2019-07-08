using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// アプリ起動中常に動くシングルトンマネージャークラス
/// </summary>
public class Manager : SingletonMonoBehaviour<Manager> {
  int coinNum;
  int excangeTicket;

  List<bool> isRItemHaveList;
  List<bool> isSRItemHaveList;
  List<bool> isSSRItemHaveList;

  //読み込み関連
  //読み込み関連
  string[] textMessage; //テキストの加工前の一行を入れる変数
  string[,] RList; //Rのリスト
  string[,] SRList; //SRのリスト
  string[,] SSRList; //SSRのリスト
  string[,] proList; //確率のリスト

  private int rowLength; //テキスト内の行数を取得する変数
  private int columnLength; //テキスト内の列数を取得する変数
  int RL;//Rの個数
  int SRL;//SRの個数
  int SSRL;//SSrの個数

  public void Awake() {
    if (this != Instance) {
      Destroy(this);
      return;
    }

    DontDestroyOnLoad(this.gameObject);
  }

  void Start() {
    imageLoadR();
    imageLoadSR();
    imageLoadSSR();

    coinNum = 10000;  //とりあえず
    excangeTicket = 0;

    isRItemHaveList = new List<bool>();
    for(int i = 0; i < RL; i++) {
      isRItemHaveList.Add(false);
    }
    isSRItemHaveList = new List<bool>();
    for (int i = 0; i < SRL; i++) {
      isSRItemHaveList.Add(false);
    }
    isSSRItemHaveList = new List<bool>();
    for (int i = 0; i < SSRL; i++) {
      isSSRItemHaveList.Add(false);
    }
  }
  /// <summary>
  /// レアリストの二次元配列を返します
  /// </summary>
  public string[,] getRList() {
    return RList;
  }

  /// <summary>
  /// SRリストの二次元配列を返します
  /// </summary>
  public string[,] getSRList() {
    return SRList;
  }

  /// <summary>
  /// SSRリストの二次元配列を返します
  /// </summary>
  public string[,] getSSRList() {
    return SSRList;
  }

  /// <summary>
  /// レアアイテムの取得状況をboolリストで返します
  /// </summary>
  public List<bool> getIsRItemHaveList() {
    return isRItemHaveList;
  }

  /// <summary>
  /// Sレアアイテムの取得状況をboolリストで返します
  /// </summary>
  public List<bool> getIsSRItemHaveList() {
    return isSRItemHaveList;
  }

  /// <summary>
  /// SSレアアイテムの取得状況をboolリストで返します
  /// </summary>
  public List<bool> getIsSSRItemHaveList() {
    return isSSRItemHaveList;
  }

  /// <summary>
  /// コイン数を取得します
  /// </summary>
  public int getCoinNum() {
    return coinNum;
  }
  /// <summary>
  /// コイン数を更新します
  /// </summary>
  public void setCoinNum(int num) {
    coinNum = num;
  }

  /// <summary>
  /// 引き換えチケット数を取得します
  /// </summary>
  public int getExcangeTicket() {
    return excangeTicket;
  }

  /// <summary>
  /// 引き換えチケット数を更新します
  /// </summary>
  public void setExcangeTicket(int num) {
    excangeTicket=num;
  }

  /// <summary>
  /// 指定要素番号のレアアイテム所持リストを有効にします
  /// </summary>
  public void setIsRItemHave(int num) {
    isRItemHaveList[num] = true;
  }

  /// <summary>
  /// 指定要素番号のSレアアイテム所持リストを有効にします
  /// </summary>
  public void setIsSRItemHave(int num) {
    isSRItemHaveList[num] = true;
  }

  /// <summary>
  /// 指定要素番号のSSレアアイテム所持リストを有効にします
  /// </summary>
  public void setIsSSRItemHave(int num) {
    isSSRItemHaveList[num] = true;
  }

  void imageLoadR() {
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

    for (int i = 0; i < rowLength; i++) {

      string[] tempWords = textMessage[i].Split('\t'); //textMessageをカンマごとに分けたものを一時的にtempWordsに代入

      for (int n = 0; n < columnLength; n++) {
        RList[i, n] = tempWords[n]; //2次配列textWordsにカンマごとに分けたtempWordsを代入していく
        Debug.Log(i.ToString() + "," + n.ToString() + "," + RList[i, n]);
      }
    }

  }
  void imageLoadSR() {
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

    for (int i = 0; i < rowLength; i++) {

      string[] tempWords = textMessage[i].Split('\t'); //textMessageをカンマごとに分けたものを一時的にtempWordsに代入

      for (int n = 0; n < columnLength; n++) {
        SRList[i, n] = tempWords[n]; //2次配列textWordsにカンマごとに分けたtempWordsを代入していく
        Debug.Log(i.ToString() + "," + n.ToString() + "," + SRList[i, n]);
      }
    }
  }
  void imageLoadSSR() {
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

    for (int i = 0; i < rowLength; i++) {

      string[] tempWords = textMessage[i].Split('\t'); //textMessageをカンマごとに分けたものを一時的にtempWordsに代入

      for (int n = 0; n < columnLength; n++) {
        SSRList[i, n] = tempWords[n]; //2次配列textWordsにカンマごとに分けたtempWordsを代入していく
        Debug.Log(i.ToString() + "," + n.ToString() + "," + SSRList[i, n]);
      }
    }
  }
}
