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
  public GameObject sika; //これはヤバイ1
  int sign;
  public bool isClear;


  string[] textMessage; //テキストの加工前の一行を入れる変数
  string[,] SignList; //標識のリスト

  [SerializeField]
  Image signImage;
  [SerializeField]
  Text signNameText;
  [SerializeField]
  Text signDescriptionText;

  [SerializeField]
  GameObject rakuseki;  //これはヤバイ2
  [SerializeField]
  GameObject hole;

  [SerializeField]
  Image wakabaSkin;
  [SerializeField]
  SpriteRenderer carSkin;

  private int rowLength; //テキスト内の行数を取得する変数
  private int columnLength; //テキスト内の列数を取得する変数
  public int stage;
  int[,] SignBtnSetting = new int[,] { 
    {
      (int)signName.tyusha,
      (int)signName.sinnyu,
      (int)signName.high,
      (int)signName.ichiji,
      (int)signName.doubutsu
    },
    {
      (int)signName.sinnyu,
      (int)signName.jidosha,
      (int)signName.low,
      (int)signName.suberi,
      (int)signName.rakuseki
    }
  };

  enum signName {
    tyusha,
    sinnyu,
    high,
    low,
    jidosha,
    ichiji,
    suberi,
    rakuseki,
    doubutsu
  }

  public bool choosing;  //標識選択中か

  [SerializeField]
  GameObject[] Signs;

  [SerializeField]
  GameObject clearUI;

  void Start() {

    if (Manager.Instance.templyGameStage == 0) {
      stage = 0;
      GameObject.Find("map2").SetActive(false);
    }
    else if (Manager.Instance.templyGameStage == 1) {
      stage = 0;
      GameObject.Find("map2").SetActive(false);
      GameObject.Find("Canvas_Scenario").SetActive(false);
    }
    else if (Manager.Instance.templyGameStage == 2) {
      stage = 1;
      GameObject.Find("map1").SetActive(false);
      GameObject.Find("Canvas_Scenario").SetActive(false);
    }

    Mode = 0;
    LoadSigns();
    SetSignBtn();
    SetSkin();

  }

  void SetSkin() {
    Debug.Log("スキンは");
    Debug.Log(Manager.Instance.GetCarSkin().rank);
    Debug.Log(Manager.Instance.GetCarSkin().num);
    if (Manager.Instance.GetNowSkin().rank == 2) {
      wakabaSkin.sprite = Resources.Load<Sprite>("Images/" + Manager.Instance.getRList()[Manager.Instance.GetNowSkin().num, 2]);
    }
    else if (Manager.Instance.GetNowSkin().rank == 1) {
      wakabaSkin.sprite = Resources.Load<Sprite>("Images/" + Manager.Instance.getSRList()[Manager.Instance.GetNowSkin().num, 2]);
    }
    else if (Manager.Instance.GetNowSkin().rank == 0) {
      wakabaSkin.sprite = Resources.Load<Sprite>("Images/" + Manager.Instance.getSSRList()[Manager.Instance.GetNowSkin().num, 2]);
    }

    if (Manager.Instance.GetCarSkin().rank == 2) {
      carSkin.sprite = Resources.Load<Sprite>("Images/" + Manager.Instance.getRList()[Manager.Instance.GetCarSkin().num, 2]);
    }
    else if (Manager.Instance.GetCarSkin().rank == 1) {
      carSkin.sprite = Resources.Load<Sprite>("Images/" + Manager.Instance.getSRList()[Manager.Instance.GetCarSkin().num, 2]);
    }
    else if (Manager.Instance.GetCarSkin().rank == 0) {
      carSkin.sprite = Resources.Load<Sprite>("Images/" + Manager.Instance.getSSRList()[Manager.Instance.GetCarSkin().num, 2]);
    }
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
      Signs[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Signs/" + SignList[SignBtnSetting[stage,i],1]);
      Signs[i].GetComponent<signBtn>().setType(SignBtnSetting[stage, i]);
      Debug.Log("[設定]"+SignList[SignBtnSetting[stage, i], 0]);
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

  public void SetSign(int n) {
    choosing = true;
    targetView.SetActive(true);
    signImage.sprite= Resources.Load<Sprite>("Signs/" + SignList[n, 1]);
    signNameText.text = SignList[n, 0];
    signDescriptionText.text = SignList[n,2];
    sign = n;
    Debug.Log(n);
  }

  public void praceSign(int id) {
    Debug.Log("praceSign");
    if (stage == 0) {
      if (sign == (int)signName.sinnyu || sign == (int)signName.ichiji || sign == (int)signName.doubutsu) {
        targetView.SetActive(false);
        if (sign == (int)signName.doubutsu) {
          sika.SetActive(true);
        }
        isClear = true;
      }
      else if (sign==(int)signName.high) {
        GameObject.Find("speedChanger").GetComponent<SpeedChanger>().newSpeed=3f;
      }
      else {
        OnFalseBtn();
      }
    }else if (stage == 1) {
      if(id==0 && sign == (int)signName.rakuseki) {
        rakuseki.SetActive(false);
        hole.SetActive(true);
      }
      if(id==1 && sign == (int)signName.jidosha) {
        isClear = true;
      }
    }
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

  public void goTitle() {
    SceneManager.LoadScene("TitleScene");
  }


}