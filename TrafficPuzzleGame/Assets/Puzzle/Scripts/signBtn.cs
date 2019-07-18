using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class signBtn : MonoBehaviour {
  public enum EaseType {
    tyusya,
    tomare,
    rakuseki,
    sika,
    sokudo,
    NoSinnyu
  }
  [SerializeField] EaseType easeType;
  // Start is called before the first frame update
  void Start() {

  }

  // Update is called once per frame
  void Update() {

  }

  public void onClick() {
    GameObject.Find("manager").GetComponent<ManageMode>().SetSign((int)easeType);
  }
}
