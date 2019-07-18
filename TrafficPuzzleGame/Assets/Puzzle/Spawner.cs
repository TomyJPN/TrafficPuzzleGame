//https://gist.github.com/hiroyukihonda/8571691

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//指定したゲームオブジェクトを無限スポーンさせるスクリプト
public class Spawner: MonoBehaviour{
	public GameObject Spowned;	//スポーンオブジェクトの指定
	public float interval = 3f;		//間隔

	void Start(){
		StartCoroutine("Spawn");
	}

	IEnumerator Spawn(){
		while (true){
			Instantiate(Spowned, transform.position, Quaternion.identity);
			yield return new WaitForSeconds(interval);
		}
	}

}