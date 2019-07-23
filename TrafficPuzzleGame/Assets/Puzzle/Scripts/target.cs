using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class target : MonoBehaviour {
  ManageMode manageMode;


  // Start is called before the first frame update
  void Start() {
    manageMode = GameObject.Find("manager").GetComponent<ManageMode>();
  }

  // Update is called once per frame
  void Update() {

  }

  

  public void onClick() {
    if (!manageMode.choosing) return;

    Debug.Log("target Clicked");
    manageMode.setSignImage(GetComponent<SpriteRenderer>());
    manageMode.praceSign();

  }
}
