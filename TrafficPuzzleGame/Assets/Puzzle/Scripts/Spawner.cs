//https://gist.github.com/hiroyukihonda/8571691

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//指定したゲームオブジェクトを無限スポーンさせるスクリプト
public class Spawner : MonoBehaviour {
  public GameObject Spowned;    //スポーンオブジェクトの指定
  public float interval = 3f;       //間隔
  public float angle;

  void Start() {
    StartCoroutine("Spawn");
  }

  IEnumerator Spawn() {
    while (true) {
      Quaternion rote = new Quaternion(0f, 0.0f, 0f, 1.0f);
      GameObject obj=Instantiate(Spowned, transform.position , rote);
      obj.transform.rotation = Quaternion.Euler(0.0f, 0.0f,angle);
      yield return new WaitForSeconds(interval);
    }
  }

}