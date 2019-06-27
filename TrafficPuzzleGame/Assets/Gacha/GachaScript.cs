using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GachaScript : MonoBehaviour
{
    public GameObject PopCoin;

    public Text UserCoin;
    int Have;
    
    // Start is called before the first frame update
    void Start()
    {
        Have = 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MyCoin()//所持コイン
    {
        Debug.Log("Click-COIN");
        Debug.Log(Have.ToString());
        //UserCoin.text = Have.ToString()+"G";
        PopCoin.SetActive(true);
    }

    public void GachaList()//内容一覧
    {
        Debug.Log("Click-LIST");
    }

    public void Probability()//確立
    {
        Debug.Log("Click-PROB");
    }

    public void Once()//単発
    {
        Debug.Log("Click-ONCE");
    }

    public void TenTimes()//10連
    {
        Debug.Log("Click-TENS");
    }

    public void Pause()//ポーズ画面
    {
        Debug.Log("Click-PAUSE");
    }

    public void Pop_Close()//コイン所持数pop閉じる
    {
        Debug.Log("Click_POP");
        PopCoin.SetActive(false);
    }
}
