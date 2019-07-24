using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner2 : MonoBehaviour {
  public GameObject Spowned;    //スポーンオブジェクトの指定
  public float interval = 3f;       //間隔
  public float angle;
  public Quaternion quaternion;
  ManageMode manageMode;

  // Start is called before the first frame update
  void Start() {
    manageMode = GameObject.Find("manager").GetComponent<ManageMode>();
    Invoke("startSpawn", 0.2f);
  }

  // Update is called once per frame
  void Update() {
    
  }

  void startSpawn() {
    StartCoroutine("Spawn");
  }

  IEnumerator Spawn() {
    while (true) {
      if (manageMode.isClear) break;
      Quaternion rote = new Quaternion(0f, 0.0f, 0f, 1.0f);
      GameObject obj = Instantiate(Spowned, transform.position, quaternion);
      obj.transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle);
      yield return new WaitForSeconds(interval);
    }
  }
}
