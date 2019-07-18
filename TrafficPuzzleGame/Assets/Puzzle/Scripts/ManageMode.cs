using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageMode : MonoBehaviour {
  public int Mode;
  public GameObject eventCol;
  public GameObject syogaiCol;
  public GameObject targetView;
  public GameObject spawner;
  public GameObject sika;
  int sign;

  [SerializeField]
  GameObject clearUI;

  void Start() {
    Mode = 0;
  }

  void Update() {
  }

  public void Trying() {
    Mode = 1;
  }

  public void OnTrueBtn() {
    if (sign == 1 || sign == 3 || sign == 5) {
      eventCol.SetActive(true);
      syogaiCol.SetActive(false);
      targetView.SetActive(false);
      Invoke("deleteSpawner", 1.5f);
      if (sign == 3) {
        sika.SetActive(true);
      }
    }
    else {
      OnFalseBtn();
    }
  }
  public void OnFalseBtn() {
    targetView.SetActive(false);
    Debug.Log("間違い");
  }

  void deleteSpawner() {
    spawner.SetActive(false);
  }

  public void SetSign(int n) {
    targetView.SetActive(true);
    sign = n;
    Debug.Log(n);
  }

  public void clear() {
    Debug.Log("クリア！");
    clearUI.SetActive(true);
    Invoke("goTitle", 3f);
  }

  void goTitle() {
    SceneManager.LoadScene("TitleScene");
  }
}