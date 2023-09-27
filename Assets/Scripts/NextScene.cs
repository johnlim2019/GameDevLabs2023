
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NextScene : MonoBehaviour
{
  public string nextLevelName;
  public StrVariable nextLevel;
  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.tag == "Player")
    {
      Debug.Log("Change scene!");
      nextLevel.SetValue(nextLevelName);
      SceneManager.LoadSceneAsync("Loading", LoadSceneMode.Single);
    }
  }
}