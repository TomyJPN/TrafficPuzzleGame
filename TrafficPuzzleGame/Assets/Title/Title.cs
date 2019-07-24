using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
  [SerializeField]
  GameObject MapMenu;

  [SerializeField]
  GameObject Credit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

  public void loadGachaScene() {
    SceneManager.LoadScene("Gacha");
  }

  public void loadGallaryScene() {
    SceneManager.LoadScene("Gallery");
  }

  public void LoadGameScene() {
    Manager.Instance.templyGameStage = 0;
    SceneManager.LoadScene("Puzzle@main");
  }

  public void onStage1() {
    Manager.Instance.templyGameStage = 1;
    SceneManager.LoadScene("Puzzle@main");
  }
  public void onStage2() {
    Manager.Instance.templyGameStage = 2;
    SceneManager.LoadScene("Puzzle@main");
  }

  public void OnMapMenuBtn() {
    MapMenu.SetActive(true);
  }

  public void OnMapMenuCloseBtn() {
    MapMenu.SetActive(false);
  }

  public void OnCreditBtn() {
    Credit.SetActive(true);
  }
  public void OnCloseCredit() {
    Credit.SetActive(false);
  }
}
