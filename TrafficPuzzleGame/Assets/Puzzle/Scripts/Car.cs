using UnityEngine;

public class Car : MonoBehaviour {
  ManageMode ManageMode;
  /// <summary>
  /// 1=player ,2=enemy
  /// </summary>
  public int type;

  int Mode = 0;
  float CarSpeed = 5;
  float CarDir;
  public float CarX, CarY;

  void Start() {
    ManageMode = GameObject.Find("manager").GetComponent<ManageMode>();
  }

  void Update() {
    if (type == 1) Mode = ManageMode.Mode;
    else Mode = 1;
    switch (Mode) {
      case (0): break;
      case (1): Run(); break;
    }
  }


  void Run() {
    //		Quaternion CarXY = transform.rotation;

    //		print(CarXY);
    //		float CarX
    Vector2 CarDir = new Vector2(CarX, CarY).normalized;            //車の進行方向
    //Vector2 CarDir = new Vector2(0, 1).normalized;      //テスト用
    GetComponent<Rigidbody2D>().velocity = CarDir * CarSpeed; //方向*速度を代入
  }

  private void OnTriggerEnter2D(Collider2D collision) {
    Debug.Log("衝突:" + collision.gameObject.name);
    if (type == 1 && collision.gameObject.name == "goal") {
      ManageMode.clear();
    }
  }

  public void onClick() {
    Debug.Log("click");
  }
}
