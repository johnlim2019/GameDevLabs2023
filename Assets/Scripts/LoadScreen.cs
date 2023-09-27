using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScreen : MonoBehaviour
{

  public Image panel;
  public string menuName = "Menu";
  public StrVariable nextLevel;
  public TextMeshProUGUI mapName;
  public TextMeshProUGUI topMapName;
  public void MainMenu()
  {
    SceneManager.LoadSceneAsync(menuName, LoadSceneMode.Single);
  }

  IEnumerator Fade()
  {
    for (float alpha = 1f; alpha >= -0.05f; alpha -= 0.05f)
    {
      var tempColor = panel.color;
      tempColor.a = alpha;
      panel.color = tempColor;
      yield return new WaitForSecondsRealtime(0.1f);
    }
    SceneManager.LoadSceneAsync(nextLevel.Value, LoadSceneMode.Single);
  }

  void Start()
  {
    mapName.SetText("WORLD " + nextLevel.Value);
    topMapName.SetText("WORLD " + nextLevel.Value);
    StartCoroutine(Fade());
  }
}
