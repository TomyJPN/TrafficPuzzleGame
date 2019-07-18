using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageMode : MonoBehaviour{
	public int Mode;

	void Start(){
		Mode = 0;
	}

	void Update(){
	}

	public void Trying(){
		Mode = 1;
	}
}