
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
public class MenuButtons : MonoBehaviour
{
  public string nextSceneName;
  public IntVariable topScore;
  public StrVariable nextMap;
  public ScoreVariable scoreVariable;

  public TextMeshProUGUI highScoreText;

  void Start()
  {
    string str = "TOP-" + topScore.previousHighestValue.ToString("D4");
    highScoreText.GetComponent<TextMeshProUGUI>().text = str;
    nextMap.SetValue("1-1");
    scoreVariable.ResetValue();
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

}