﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
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
    SceneManager.LoadScene("Puzzle@main");
  }
}
