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
    public GameObject PopResult; //ガチャ結果ポップアップ
    

    public Text UserCoin; //常在コイン数表示用テキスト
    public Text UserCoin2; //コイン数詳細表示用テキスト
    public Text GachaResult; //ガチャ結果表示(仮)

    int Have; //所持コイン
    int Rand; //乱数
    
    // Start is called before the first frame update
    void Start()
    {
        Have = 90;
        UserCoin.text = Have.ToString() + "G";
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
        UserCoin2.text = "無償コイン : " + UserCoin.text + "\n" + "無償コイン : 0G";
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

    public void Probability()//確立
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
            GachaResult.text = "1 : SSR";
        }
        else if (Rand > 5 && Rand <= 55)
        {
            GachaResult.text = "1 : SR";
        }
        else
        {
            GachaResult.text = "1 : R";
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
                GachaResult.text += "SR";
            }
            else
            {
                GachaResult.text += "R";
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
            GachaResult.text += "SR";
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
        Have += 100;
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
