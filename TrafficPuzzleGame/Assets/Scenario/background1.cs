using UnityEngine;
using System.Collections;

public class background1 : MonoBehaviour {
  void Update() {
    transform.Translate(2f, 0, 0);
    if (transform.localPosition.x > 682f) {
      transform.localPosition = new Vector3(-1679f,0, 0);
    }
  }
}