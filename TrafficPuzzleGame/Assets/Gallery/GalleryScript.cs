using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public GameObject Trading;

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

        RL = RList.GetLength(0);
        Debug.Log("[ギャラリー]  RL : " + RL);
        SRL = SRList.GetLength(0);
        Debug.Log("[ギャラリー] SRL : " + SRL);
        SSRL = SSRList.GetLength(0);
        Debug.Log("[ギャラリー]SSRL : " + SSRL);

        isSSRItemHaveList[0] = true;

        for (int i = 0; i < SSRL; i++)
        {
            GameObject item = Instantiate(IconPrefab);
            item.transform.SetParent(ContentObject[0].transform, false);

            item.GetComponent<IconScript>().No = i;
            Debug.Log("No. " + item.GetComponent<IconScript>().No);
            item.GetComponent<IconScript>().Rarity = 0;
            if (isSSRItemHaveList[i] == true)
            {
                Debug.Log("[ギャラリー]Images/" + SSRList[i, 2] + "を読み込み");
                Sprite = Resources.Load<Sprite>("Images/" + SSRList[i, 2]);
                item.GetComponent<Image>().sprite = null;
                item.GetComponent<Image>().sprite = Sprite;
                item.GetComponent<IconScript>().SkinName = SSRList[i, 1];
                item.GetComponent<IconScript>().FileName = SSRList[i, 2];
            }

        }
        for (int i = 0; i < SRL; i++)
        {
            GameObject item = Instantiate(IconPrefab);
            item.transform.SetParent(ContentObject[1].transform, false);

            item.GetComponent<IconScript>().No = i;
            Debug.Log("No. " + item.GetComponent<IconScript>().No);
            item.GetComponent<IconScript>().Rarity = 1;
            if (isSRItemHaveList[i] == true)
            {
                Debug.Log("[ギャラリー]Images/" + SRList[i, 2] + "を読み込み");
                Sprite = Resources.Load<Sprite>("Images/" + SRList[i, 2]);
                item.GetComponent<Image>().sprite = null;
                item.GetComponent<Image>().sprite = Sprite;
                item.GetComponent<IconScript>().SkinName = SRList[i, 1];
                item.GetComponent<IconScript>().FileName = SRList[i, 2];
            }

        }
        for (int i = 0; i < RL; i++)
        {
            GameObject item = Instantiate(IconPrefab);
            item.transform.SetParent(ContentObject[2].transform, false);

            item.GetComponent<IconScript>().No = i;
            Debug.Log("No. " + item.GetComponent<IconScript>().No);
            item.GetComponent<IconScript>().Rarity = 2;
            if (isRItemHaveList[i] == true)
            {
                Debug.Log("[ギャラリー]Images/" + RList[i, 2] + "を読み込み");
                Sprite = Resources.Load<Sprite>("Images/" + RList[i, 2]);
                item.GetComponent<Image>().sprite = null;
                item.GetComponent<Image>().sprite = Sprite;
                item.GetComponent<IconScript>().SkinName = RList[i, 1];
                item.GetComponent<IconScript>().FileName = RList[i, 2];
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
    }

    public void Details(string FileName, string SkinName, int Rarity ,int num)
    {
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
            }
        }
        Detail.SetActive(true);
        DNum = num;
        
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
        //チケット消費など
    }
}
