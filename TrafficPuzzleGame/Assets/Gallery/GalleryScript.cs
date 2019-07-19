using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GalleryScript : MonoBehaviour
{
    private string[,] RList;
    private string[,] SRList;
    private string[,] SSRList;
    private List<bool> isRItemHaveList;
    private List<bool> isSRItemHaveList;
    private List<bool> isSSRItemHaveList;

    int RL;
    int SRL;
    int SSRL;

    public GameObject IconPrefab;
    public GameObject[] ContentObject;
    public GameObject[] ScrollView;

    public GameObject Detail;
    public Text SName;
    public Image Image;
    public Text Ticket;
    public Text Exchange;
    private int Price;
    private int DNum;
    private int ET;//交換チケット枚数
    public GameObject Trading;
    public GameObject Pause;
    public GameObject SkinSet;

    private Sprite Sprite;//画像読み込み用sprite

    // Start is called before the first frame update
    void Start()
    {
        //データの読み込み
        RList = Manager.Instance.getRList();
        SRList = Manager.Instance.getSRList();
        SSRList = Manager.Instance.getSSRList();
        isRItemHaveList = Manager.Instance.getIsRItemHaveList();
        isSRItemHaveList = Manager.Instance.getIsSRItemHaveList();
        isSSRItemHaveList = Manager.Instance.getIsSSRItemHaveList();
        Ticket.text = Manager.Instance.getExcangeTicket().ToString() + "枚";
        ET = Manager.Instance.getExcangeTicket();

        RL = RList.GetLength(0);
        Debug.Log("[ギャラリー]  RL : " + RL);
        SRL = SRList.GetLength(0);
        Debug.Log("[ギャラリー] SRL : " + SRL);
        SSRL = SSRList.GetLength(0);
        Debug.Log("[ギャラリー]SSRL : " + SSRL);
        Write();
        
    }

    public void Write()
    {
        for (int i = 0; i < SSRL; i++)
        {
            GameObject item = Instantiate(IconPrefab);
            item.transform.SetParent(ContentObject[0].transform, false);

            item.GetComponent<IconScript>().No = i;
            Debug.Log("No. " + item.GetComponent<IconScript>().No);
            item.GetComponent<IconScript>().Rarity = 0;
            item.GetComponent<IconScript>().SkinName = SSRList[i, 1];
            item.GetComponent<IconScript>().FileName = SSRList[i, 2];
            if (isSSRItemHaveList[i] == true)
            {
                Debug.Log("[ギャラリー]Images/" + SSRList[i, 2] + "を読み込み");
                Sprite = Resources.Load<Sprite>("Images/" + SSRList[i, 2]);
                item.GetComponent<Image>().sprite = null;
                item.GetComponent<Image>().sprite = Sprite;
            }

        }
        for (int i = 0; i < SRL; i++)
        {
            GameObject item = Instantiate(IconPrefab);
            item.transform.SetParent(ContentObject[1].transform, false);

            item.GetComponent<IconScript>().No = i;
            Debug.Log("No. " + item.GetComponent<IconScript>().No);
            item.GetComponent<IconScript>().Rarity = 1;
            item.GetComponent<IconScript>().SkinName = SRList[i, 1];
            item.GetComponent<IconScript>().FileName = SRList[i, 2];
            if (isSRItemHaveList[i] == true)
            {
                Debug.Log("[ギャラリー]Images/" + SRList[i, 2] + "を読み込み");
                Sprite = Resources.Load<Sprite>("Images/" + SRList[i, 2]);
                item.GetComponent<Image>().sprite = null;
                item.GetComponent<Image>().sprite = Sprite;
            }

        }
        for (int i = 0; i < RL; i++)
        {
            GameObject item = Instantiate(IconPrefab);
            item.transform.SetParent(ContentObject[2].transform, false);

            item.GetComponent<IconScript>().No = i;
            Debug.Log("No. " + item.GetComponent<IconScript>().No);
            item.GetComponent<IconScript>().Rarity = 2;
            item.GetComponent<IconScript>().SkinName = RList[i, 1];
            item.GetComponent<IconScript>().FileName = RList[i, 2];
            if (isRItemHaveList[i] == true)
            {
                Debug.Log("[ギャラリー]Images/" + RList[i, 2] + "を読み込み");
                Sprite = Resources.Load<Sprite>("Images/" + RList[i, 2]);
                item.GetComponent<Image>().sprite = null;
                item.GetComponent<Image>().sprite = Sprite;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClickSSR ()
    {
        if (ScrollView[0].activeSelf == true)
        {
            return;
        }
        if (ScrollView[1].activeSelf == true)
        {
            ScrollView[1].SetActive(false);
        }
        if(ScrollView[2].activeSelf == true)
        {
            ScrollView[2].SetActive(false);
        }
        ScrollView[0].SetActive(true);
        Debug.Log("Click : SSR");
    }

    public void onClickSR()
    {
        if(ScrollView[1].activeSelf == true)
        {
            return;
        }
        if (ScrollView[0].activeSelf == true)
        {
            ScrollView[0].SetActive(false);
        }
        if (ScrollView[2].activeSelf == true)
        {
            ScrollView[2].SetActive(false);
        }
        ScrollView[1].SetActive(true);
        Debug.Log("Click : SR");
    }

    public void onClickR()
    {
        if (ScrollView[2].activeSelf == true)
        {
            return;
        }
        if (ScrollView[1].activeSelf == true)
        {
            ScrollView[1].SetActive(false);
        }
        if (ScrollView[0].activeSelf == true)
        {
            ScrollView[0].SetActive(false);
        }
        ScrollView[2].SetActive(true);
        Debug.Log("Click : R");
    }

    public void onClickPause()
    {
        Debug.Log("Click : pause");
        if (Pause.activeSelf == false) {
            Pause.SetActive(true);
        }
        else
        {
            Pause.SetActive(false);
        }
    }

    public void goHome()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void Details(string FileName, string SkinName, int Rarity ,int num)
    {
    Manager.Skin CheckSkin = Manager.Instance.GetNowSkin();
    Manager.Skin CCheckSkin = Manager.Instance.GetCarSkin();
    Manager.Skin ThisSkin = new Manager.Skin();
    Trading.SetActive(false);
        Debug.Log(FileName);
    ThisSkin.num = num;
    ThisSkin.rank = Rarity;
    Debug.Log("人スキン番号" + CheckSkin.num + "人スキンレア度" + CheckSkin.rank);
    Debug.Log("車スキン番号" + CCheckSkin.num + "車スキンレア度" + CCheckSkin.rank);
    Debug.Log("スキン番号" + ThisSkin.num + "スキンレア度" + ThisSkin.rank);

    if(
      (CheckSkin.num==ThisSkin.num && CheckSkin.rank==ThisSkin.rank) ||
      (CCheckSkin.num == ThisSkin.num && CCheckSkin.rank == ThisSkin.rank))
    {
      SkinSet.SetActive(false);
      Debug.Log("同じ");
    }    else
    {
      SkinSet.SetActive(true);
    }
    

    if (Rarity == 0)
        {
            if (isSSRItemHaveList[num] == true)
            {
                SName.text = SkinName;
                Sprite = Resources.Load<Sprite>("Images/" + FileName);
                
            }
            else
            {
                SName.text = "?????";
                Sprite = Resources.Load<Sprite>("Images/" + "question");
                Price = 3000;
                Exchange.text = Price.ToString();
                Exchange.text += "枚で交換";
                Trading.SetActive(true);
                SkinSet.SetActive(false);
            }
        }
        else if(Rarity == 1)
        {
            if (isSRItemHaveList[num] == true)
            {
                SName.text = SkinName;
                Sprite = Resources.Load<Sprite>("Images/" + FileName);
      }
            else
            {
                SName.text = "?????";
                Sprite = Resources.Load<Sprite>("Images/" + "question");
                Price = 1000;
                Exchange.text = Price.ToString();
                Exchange.text += "枚で交換";
                Trading.SetActive(true);
                SkinSet.SetActive(false);
            }
        }
        else
        {
            if(isRItemHaveList[num] == true)
            {
                SName.text = SkinName;
                Sprite = Resources.Load<Sprite>("Images/" + FileName);
      }
            else
            {
                SName.text = "?????";
                Sprite = Resources.Load<Sprite>("Images/" + "question");
                Price = 500;
                Exchange.text = Price.ToString();
                Exchange.text += "枚で交換";
                Trading.SetActive(true);
                SkinSet.SetActive(false);
            }
        }
        Detail.SetActive(true);
        DNum = num;
    Debug.Log(DNum + "=" + num);
    Debug.Log("rarity : " + Rarity);
        Image.GetComponent<Image>().sprite = null;
        Image.GetComponent<Image>().sprite = Sprite;
        return;
    }

    public void Close()
    {
        GameObject.Find("Details").SetActive(false);
        Trading.SetActive(false);
    }

    public void Trade()
    {
        Debug.Log("購入手続き開始");
        if (ScrollView[0].activeSelf == true)
        {
            if (isSSRItemHaveList[DNum] == true)
            {
                Debug.Log("購入済");
                return;
            }
        }
        else if (ScrollView[1].activeSelf == true)
        {
            if (isSRItemHaveList[DNum] == true)
            {
                Debug.Log("購入済");
                return;
            }
        }
        else
        {
            if (isRItemHaveList[DNum] == true)
            {
                Debug.Log("購入済");
                return;
            }
        }
        if (ET < Price) 
        {
            Debug.Log("チケット不足:所持枚数..." + ET);
            return;
        }
        ET -= Price;
        Debug.Log("支払い完了:所持枚数..." + ET);
        Manager.Instance.setExcangeTicket(ET);
        if (Price == 3000)
        {
            Manager.Instance.setIsSSRItemHave(DNum);
            Debug.Log("SSRの" + DNum + "を所持済に");
            Details(SSRList[DNum,2],SSRList[DNum,1],0,DNum);
        }
        else if (Price == 1000)
        {
            Manager.Instance.setIsSRItemHave(DNum);
            Debug.Log("SRの" + DNum + "を所持済に");
            Details(SRList[DNum, 2], SRList[DNum, 1], 1, DNum);
        }
        else
        {
            Manager.Instance.setIsRItemHave(DNum);
            Debug.Log("Rの" + DNum + "を所持済に");
            Details(RList[DNum, 2], RList[DNum, 1], 2, DNum);
        }
        Manager.Instance.DataSave();
        ReLoad();
        return;
    }
    
  public void Set()
  {
    Manager.Skin HumSkin,CarSkin;
    Debug.Log("スキンセット中");
    Debug.Log("番号 :" + DNum);
    HumSkin = Manager.Instance.GetNowSkin();
    CarSkin = Manager.Instance.GetCarSkin();
    if (ScrollView[0].activeSelf == true)
    {
      if (SSRList[DNum,2][0] == 'C')
      {
        if (DNum == CarSkin.num)
        {
          Debug.Log("セット済");
          return;
        }
        else
        {
          CarSkin.num = DNum;
          CarSkin.rank = 0;
          //CarSkin.name = SSRList[DNum, 1];
         // Debug.Log("SSR : " + CarSkin.name + " : " + CarSkin.num);
          Details(SSRList[DNum, 2], SSRList[DNum, 1], 0, DNum);
        }
      }
      else
      {
        if (DNum == HumSkin.num)
        {
          Debug.Log("セット済");
          return;
        }
        else
        {
          HumSkin.num = DNum;
          HumSkin.rank = 0;
         // HumSkin.name = SSRList[DNum, 1];
          //Debug.Log("SSR : " + HumSkin.name + " : " + HumSkin.num);
          Details(SSRList[DNum, 2], SSRList[DNum, 1], 0, DNum);
        }
      }
    }
    else if (ScrollView[1].activeSelf == true)
    {
      if (SRList[DNum, 2][0] == 'C')
      {
        if (DNum == CarSkin.num)
        {
          Debug.Log("セット済");
          return;
        }
        else
        {
          CarSkin.num = DNum;
          CarSkin.rank = 1;
          //CarSkin.name = SRList[DNum, 1];
          //Debug.Log("SR : " + CarSkin.name + " : " + CarSkin.num);
          Details(SRList[DNum, 2], SRList[DNum, 1], 1, DNum);
        }
      }
      else
      {
        if (DNum == HumSkin.num)
        {
          Debug.Log("セット済");
          return;
        }
        else
        {
          HumSkin.num = DNum;
          HumSkin.rank = 1;
          //HumSkin.name = SRList[DNum, 1];
          //Debug.Log("SR : " + HumSkin.name + " : " + HumSkin.num);
          Details(SRList[DNum, 2], SRList[DNum, 1], 1, DNum);
        }
      }
    }
    else
    {
      if (RList[DNum, 2][0] == 'C')
      {
        if (DNum == CarSkin.num)
        {
          Debug.Log("セット済");
          return;
        }
        else
        {
          CarSkin.num = DNum;
          CarSkin.rank = 2;
          //CarSkin.name = RList[DNum, 1];
         // Debug.Log("R : " + CarSkin.name + " : " + CarSkin.num);
          Details(RList[DNum, 2], RList[DNum, 1], 2, DNum);
        }
      }
      else
      {
        if (DNum == HumSkin.num)
        {
          Debug.Log("セット済");
          return;
        }
        else
        {
          HumSkin.num = DNum;
          HumSkin.rank = 2;
         // HumSkin.name = RList[DNum, 1];
          //Debug.Log("R : " + HumSkin.name + " : " + HumSkin.num);
          Details(RList[DNum, 2], RList[DNum, 1], 2, DNum);
        }
      }
    }
    Manager.Instance.SetNowSkin(HumSkin);
    Manager.Instance.SetCarSkin(CarSkin);
    Manager.Instance.DataSave();
  }

    void ReLoad()//リロード
    {
        isRItemHaveList = Manager.Instance.getIsRItemHaveList();
        isSRItemHaveList = Manager.Instance.getIsSRItemHaveList();
        isSSRItemHaveList = Manager.Instance.getIsSSRItemHaveList();
        Ticket.text = Manager.Instance.getExcangeTicket().ToString() + "枚";
        ET = Manager.Instance.getExcangeTicket();
        Clear();
        Write();
        return;
    }

    void Clear()
    {
        for (int i = 0; i < ContentObject.Length; i++)
        {
            foreach(Transform childTransform in ContentObject[i].transform)
            {
                Destroy(childTransform.gameObject);
            }
        }
    }
}
