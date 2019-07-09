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

    public RectTransform IconPrefab = null;

    private Sprite Sprite;//画像読み込み用sprite
    private GameObject IconImage;//アイコン表示用

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

        RL = RList.GetLength(0);
        Debug.Log("  RL : " + RL);
        SRL = SRList.GetLength(0);
        Debug.Log(" SRL : " + SRL);
        SSRL = SSRList.GetLength(0);
        Debug.Log("SSRL : " + SSRL);

        for (int i = 0; i < SSRL; i++)
        {
            var item = GameObject.Instantiate(IconPrefab) as RectTransform;
            item.SetParent(transform, false);

            if (isSSRItemHaveList[i] == true)
            {
                Debug.Log("Images/" + SSRList[i, 2] + "を読み込み");
                Sprite = Resources.Load<Sprite>("Images/" + SSRList[i, 2]);
                IconImage.GetComponent<Image>().sprite = null;
                IconImage.GetComponent<Image>().sprite = Sprite;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClickSSR ()
    {
        Debug.Log("Click : SSR");
    }

    public void onClickSR()
    {
        Debug.Log("Click : SR");
    }

    public void onClickR()
    {
        Debug.Log("Click : R");
    }

    public void onClickPause()
    {
        Debug.Log("Click : pause");
    }
}
