using UnityEngine;
using System.Collections;

public class background : MonoBehaviour {
  void Update() {
    transform.Translate(2f, 0, 0);
    if (transform.localPosition.x > 970f) {
      transform.localPosition = new Vector3(-975f, 49.68f, 0);
    }
  }
}