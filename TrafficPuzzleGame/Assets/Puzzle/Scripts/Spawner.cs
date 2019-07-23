//https://gist.github.com/hiroyukihonda/8571691

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//指定したゲームオブジェクトを無限スポーンさせるスクリプト
public class Spawner : MonoBehaviour {
  public GameObject Spowned;    //スポーンオブジェクトの指定
  public float interval = 3f;       //間隔
  public float angle;
  ManageMode manageMode;

  [SerializeField]
  GameObject before;
  [SerializeField]
  GameObject after;

  void Start() {
    manageMode = GameObject.Find("manager").GetComponent<ManageMode>();
    StartCoroutine("Spawn");
  }

  void Update() {
    if (manageMode.isClear) {
      place();
    }
  }

  public void place() {
    before.SetActive(false);
    after.SetActive(true);

  }

  IEnumerator Spawn() {
    while (true) {
      if (manageMode.isClear) break;
      Quaternion rote = new Quaternion(0f, 0.0f, 0f, 1.0f);
      GameObject obj=Instantiate(Spowned, transform.position , rote);
      obj.transform.rotation = Quaternion.Euler(0.0f, 0.0f,angle);
      yield return new WaitForSeconds(interval);
    }
  }

}