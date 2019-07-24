//https://gist.github.com/hiroyukihonda/8571691

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//指定したゲームオブジェクトを無限スポーンさせるスクリプト
public class Spawner : MonoBehaviour {
  public GameObject Spowned;    //スポーンオブジェクトの指定
  public float interval = 3f;       //間隔
  public float angle;
  public Quaternion quaternion;
  ManageMode manageMode;

  [SerializeField]
  GameObject before;
  [SerializeField]
  GameObject after;

  [SerializeField]
  GameObject speedChanger;

  [SerializeField]
  GameObject walk;

  void Start() {
    manageMode = GameObject.Find("manager").GetComponent<ManageMode>();
    Invoke("startSpawn", 0.2f);
  }

  void startSpawn() {
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
    if (manageMode.stage == 1) Invoke("deleteWalk", 2f);
  }

  IEnumerator Spawn() {
    while (true) {
      if (manageMode.isClear) break;
      Quaternion rote = new Quaternion(0f, 0.0f, 0f, 1.0f);
      GameObject obj=Instantiate(Spowned, transform.position , quaternion);
      obj.transform.rotation = Quaternion.Euler(0.0f, 0.0f,angle);
      yield return new WaitForSeconds(interval);
    }
  }
  void deleteWalk() {
    walk.SetActive(false);
  }
}