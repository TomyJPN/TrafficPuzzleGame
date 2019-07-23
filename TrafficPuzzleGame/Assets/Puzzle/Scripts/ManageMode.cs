using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManageMode : MonoBehaviour {
  public int Mode;
  public GameObject eventCol;
  public GameObject syogaiCol;
  public GameObject targetView;
  public GameObject spawner;
  public GameObject sika;
  int sign;

  string[] textMessage; //テキストの加工前の一行を入れる変数
  string[,] SignList; //標識のリスト

  private int rowLength; //テキスト内の行数を取得する変数
  private int columnLength; //テキスト内の列数を取得する変数
  int stage=0;
  int[,] SignBtnSetting = new int[,] { {0,1,2,5,8 } };

  public bool choosing;  //標識選択中か

  [SerializeField]
  GameObject[] Signs;

  [SerializeField]
  GameObject clearUI;

  void Start() {
    Mode = 0;
    LoadSigns();
    SetSignBtn();
  }

  void Update() {
  }

  void LoadSigns() {
    TextAsset textasset = new TextAsset(); //テキストファイルのデータを取得するインスタンスを作成
    textasset = Resources.Load("SingList", typeof(TextAsset)) as TextAsset; //Resourcesフォルダから対象テキストを取得
    string TextLines = textasset.text; //テキスト全体をstring型で入れる変数を用意して入れる

    textMessage = TextLines.Split('\n');

    //行数と列数を取得
    columnLength = textMessage[0].Split('\t').Length;
    rowLength = textMessage.Length;

    //2次配列を定義
    SignList = new string[rowLength, columnLength];

    for (int i = 0; i < rowLength; i++) {

      string[] tempWords = textMessage[i].Split('\t'); //textMessageをカンマごとに分けたものを一時的にtempWordsに代入

      for (int n = 0; n < columnLength; n++) {
        SignList[i, n] = tempWords[n]; //2次配列textWordsにカンマごとに分けたtempWordsを代入していく
        Debug.Log(i.ToString() + "," + n.ToString() + "," + SignList[i, n]);
      }
    }

  }
  void SetSignBtn() {
    for(int i = 0; i < 5; i++) {
      Signs[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Signs/" + SignList[SignBtnSetting[0,i],1]);
      Signs[i].GetComponent<signBtn>().setType(SignBtnSetting[0, i]);
      Debug.Log("[設定]"+SignList[SignBtnSetting[0, i], 0]);
    }
  }
  public string[,] getSignList() {
    return SignList;
  }

  public void Trying() {
    Mode = 1;
  }

  public void OnTrueBtn() {
    if (sign == 1 || sign == 3 || sign == 5) {
      eventCol.SetActive(true);
      syogaiCol.SetActive(false);
      targetView.SetActive(false);
      Invoke("deleteSpawner", 1.5f);
      if (sign == 3) {
        sika.SetActive(true);
      }
    }
    else {
      OnFalseBtn();
    }
  }
  public void OnFalseBtn() {
    targetView.SetActive(false);
    Debug.Log("間違い");
  }

  void deleteSpawner() {
    spawner.SetActive(false);
  }

  public void SetSign(int n) {
    choosing = true;
    targetView.SetActive(true);
    sign = n;
    Debug.Log(n);
  }

  public void setSignImage(SpriteRenderer spriteRenderer) {
    Debug.Log(SignList[sign, 1]);
    float targetSize = spriteRenderer.bounds.size.x;
    spriteRenderer.sprite = null;
    spriteRenderer.sprite = Resources.Load<Sprite>("Signs/" + SignList[sign,1]) ;
    choosing = false;
    targetView.SetActive(false);
  }

  public void clear() {
    Debug.Log("クリア！");
    clearUI.SetActive(true);
    Invoke("goTitle", 3f);
  }

  void goTitle() {
    SceneManager.LoadScene("TitleScene");
  }


}