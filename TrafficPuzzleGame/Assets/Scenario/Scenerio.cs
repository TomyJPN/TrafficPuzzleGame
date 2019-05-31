using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scenerio : MonoBehaviour
{
    public GameObject  score_object = null;
    public string[] vs; //シナリオ格納
    public Text uiText;
    public GameObject wakaba;

    int CL = 0; //行番号

    // Start is called before the first frame update
    void Start()
    {

        Update();
    }

    // Update is called once per frame
    void Update()
    {
        if (wakaba.activeSelf == false && Input.GetMouseButtonDown(0))
        {
            wakaba.SetActive(true);
        }
        if (CL < vs.Length && Input.GetMouseButtonDown(0))
        {
            Textupdate();
        }
    }

    void Textupdate()
    {
        uiText.text = vs[CL];
        CL++;
        if(CL >= vs.Length)
        {
            End();
        }
    }

    void End()
    {
        uiText.text = "The End...";
    }
}
