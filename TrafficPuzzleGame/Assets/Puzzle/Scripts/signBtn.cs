using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class signBtn : MonoBehaviour {
  int type;
  // Start is called before the first frame update
  void Start() {

  }

  // Update is called once per frame
  void Update() {

  }

  public void onClick() {
    GameObject.Find("manager").GetComponent<ManageMode>().SetSign(type);
  }

  public void setType(int i) {
    type = i;
  }
}
