using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor;

public class GameManager : Singleton<GameManager>
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


  [System.NonSerialized]
  public int score; // we don't want this to show up in the inspector
  public IntVariable topScore;
  public bool alive = true;

  override public void Awake()
  {
    base.Awake();
    StartSceneHelper();
  }

  void Start()
  {
    SceneManager.activeSceneChanged += StartScene;
  }

  private void StartSceneHelper()
  {
    marioDeath = GameObject.Find("MarioDeathSfx").GetComponent<AudioSource>();
    BGM = GameObject.Find("BGM").GetComponent<AudioSource>();
    ugBGM = GameObject.Find("UG-BGM").GetComponent<AudioSource>();
    menuBGM = GameObject.Find("MenuBGM").GetComponent<AudioSource>();

    Debug.Log(SceneManager.GetActiveScene().name);

    if (!SceneManager.GetActiveScene().name.Equals("Loading") && !SceneManager.GetActiveScene().name.Equals("Menu"))
    {
      menuBGM.Stop();
      GameStartEvent.Raise(score);
      BGM.Play();
      currentTrack = EnumBGM.BGM;

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
    score++;
    ScoreUpdateEvent.Raise(score);
  }

  public void GameOver()
  {
    GameOverEvent.Raise(score);
    marioDeath.PlayOneShot(marioDeath.clip);
    alive = false;
    topScore.SetValue(score);
    StopBGM();
  }
  public void ResetGame()
  {
    alive = true;
    // score reset
    score = 0;
    RestartBGM();
  }


  public void OverGround2UnderGroundBGM()
  {
    BGM.Stop();
    ugBGM.Play();
    currentTrack = EnumBGM.UG;
  }
}
