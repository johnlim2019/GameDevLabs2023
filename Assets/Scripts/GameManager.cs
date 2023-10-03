using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor;

public class GameManager : MonoBehaviour
{
  // elements to be scraped from the scene and then attached to events 
  public AudioSource marioDeath;
  public AudioSource BGM;
  public AudioSource ugBGM;
  public AudioSource menuBGM;

  // events 
  public IntSimpleGameEvent GameOverEvent;
  public IntSimpleGameEvent ScoreUpdateEvent;
  public IntSimpleGameEvent GameStartEvent;
  private EnumBGM currentTrack;
  public enum EnumBGM
  {
    BGM = 0,
    UG = 1
  }

  public ScoreVariable score;
  public IntVariable topScore;
  public bool alive = true;

  void Start()
  {
    StartSceneHelper();
    SceneManager.activeSceneChanged += StartScene;
  }

  private void StartSceneHelper()
  {
    Debug.Log(SceneManager.GetActiveScene().name);

    if (SceneManager.GetActiveScene().name.Equals("1-1"))
    {
      menuBGM.Stop();
      GameStartEvent.Raise(score.Value);
      BGM.Play();
      currentTrack = EnumBGM.BGM;
      ugBGM.Stop();
    }
    else if (SceneManager.GetActiveScene().name.Equals("1-2"))
    {
      menuBGM.Stop();
      GameStartEvent.Raise(score.Value);
      BGM.Stop();
      currentTrack = EnumBGM.UG;
      ugBGM.Play();
    }
    else if (SceneManager.GetActiveScene().name.Equals("Menu"))
    {
      BGM.Stop();
      ugBGM.Stop();
      menuBGM.Play();
    }
  }

  void makePlayerAlive()
  {
    alive = true;
  }
  public void StartScene(Scene current, Scene next)
  {
    Debug.Log("scene change " + next.name);
    makePlayerAlive();
  }

  // Update is called once per frame
  void Update()
  {

  }

  public void ToggleBGM()
  {
    if (BGM.isPlaying)
    {
      BGM.Pause();
      currentTrack = EnumBGM.BGM;
    }
    else if (ugBGM.isPlaying)
    {
      ugBGM.Pause();
      currentTrack = EnumBGM.UG;
    }
    else if (currentTrack == EnumBGM.BGM)
    {
      BGM.UnPause();
    }
    else if (currentTrack == EnumBGM.UG)
    {
      ugBGM.UnPause();
    }
  }

  public void StopBGM()
  {
    if (BGM.isPlaying)
    {
      BGM.Stop();
      currentTrack = EnumBGM.BGM;
    }
    else if (ugBGM.isPlaying)
    {
      ugBGM.Stop();
      currentTrack = EnumBGM.UG;
    }
  }

  public void RestartBGM()
  {
    if (currentTrack == EnumBGM.BGM)
    {
      BGM.Play();
    }
    else if (currentTrack == EnumBGM.UG)
    {
      ugBGM.Play();
    }
  }

  public void ScoreIncrement()
  {
    score.ApplyChange(1);
    ScoreUpdateEvent.Raise(score.Value);
  }

  public void GameOver()
  {
    GameOverEvent.Raise(score.Value);
    marioDeath.PlayOneShot(marioDeath.clip);
    alive = false;
    topScore.SetValue(score.Value);
    StopBGM();
  }
  public void ResetGame()
  {
    alive = true;
    // score reset
    score.ResetValue();
    RestartBGM();
  }
}
