
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuButtons : MonoBehaviour
{
  public string nextSceneName;
  public IntVariable topScore;

  public TextMeshProUGUI highScoreText;

  void Start()
  {
    string str = "TOP-" + topScore.previousHighestValue.ToString("D4");
    highScoreText.GetComponent<TextMeshProUGUI>().text = str;
  }

  public void StartGame()
  {
    SceneManager.LoadSceneAsync(nextSceneName, LoadSceneMode.Single);
  }

  public void ResetScore()
  {
    topScore.ResetHighestValue();
    string str = "TOP-" + topScore.previousHighestValue.ToString("D4");
    highScoreText.GetComponent<TextMeshProUGUI>().text = str;
  }

  public void MainMenu()
  {
    SceneManager.LoadSceneAsync("Menu", LoadSceneMode.Single);
  }
}