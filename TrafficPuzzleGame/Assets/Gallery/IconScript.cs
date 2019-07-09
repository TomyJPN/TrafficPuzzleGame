using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconScript : MonoBehaviour
{
    public int No;
    public string SkinName;
    public string FileName;

    public int Rarity;//0-SSR,1-SR,2-R


    public GameObject Detail;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClick()
    {
        Debug.Log("details_click");
        GameObject.Find("GalleryManager").GetComponent<GalleryScript>().Details(FileName,SkinName,Rarity,No);
    }

    
}
