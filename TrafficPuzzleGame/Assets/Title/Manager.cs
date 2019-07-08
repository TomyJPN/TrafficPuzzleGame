using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// アプリ起動中常に動くシングルトンマネージャークラス
/// </summary>
public class Manager : SingletonMonoBehaviour<Manager> {
  int coinNum;
  List<bool> isItemGetList;

  public void Awake() {
    if (this != Instance) {
      Destroy(this);
      return;
    }

    DontDestroyOnLoad(this.gameObject);
  }

  void Start() {
    coinNum = 10000;
    isItemGetList = new List<bool>();

  }
}
