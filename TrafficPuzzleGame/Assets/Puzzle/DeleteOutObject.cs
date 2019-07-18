using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteOutObject : MonoBehaviour{
	void Start(){
		
	}

	void Update() {
		if (!GetComponent<Renderer>().isVisible) {
			Destroy(this.gameObject);
	}
}
}
