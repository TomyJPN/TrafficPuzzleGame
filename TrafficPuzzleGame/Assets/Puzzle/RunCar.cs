using UnityEngine;

public class RunCar : MonoBehaviour{
	public ManageMode ManageMode;
	int Mode = 0;
	float CarSpeed = 5;
	float CarDir;
	float CarX, CarY;

	void Start(){

	}

	void Update(){
		Mode = ManageMode.Mode;
		switch (Mode){
			case (0): break;
			case (1): Run(); break;
		}
	}


	void Run(){
//		Quaternion CarXY = transform.rotation;
		
//		print(CarXY);
//		float CarX
//		Vector2 CarDir = new Vector2(CarX, CarY).normalized;            //車の進行方向
		Vector2 CarDir = new Vector2(0, 1).normalized;		//テスト用
		GetComponent<Rigidbody2D>().velocity = CarDir * CarSpeed; //方向*速度を代入
	}
}
