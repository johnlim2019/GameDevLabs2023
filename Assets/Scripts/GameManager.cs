using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
  public Transform gameCamera;
  public TextMeshProUGUI scoreText;
  public TextMeshProUGUI endScoreText;
  private Rigidbody2D marioBody;
  public PlayerMovement playerMovement;
  public GameObject BouncyLootBoxes;
  public GameObject BouncyLootBricks;
  public AudioSource marioDeath;

  public UnityEvent<int> GameOverEvent;
  public UnityEvent GameResetEvent;
  public UnityEvent GameStartEvent;
  public UnityEvent<int> ScoreIncrementEvent;
  public UnityEvent PlayerStompEvent;

  public delegate void MarioKillGoomba();
  public MarioKillGoomba marioKillGoomba;

  [System.NonSerialized]
  public int score = 0; // we don't want this to show up in the inspector
  public bool alive = true;

  // Start is called before the first frame update
  void Start()
  {
    GameStartEvent.Invoke();
    marioDeath = GameObject.Find("MarioDeathSfx").GetComponent<AudioSource>();
    marioKillGoomba += PlayerStompEvent.Invoke;
    marioKillGoomba += ScoreIncrement;
  }

  // Update is called once per frame
  void Update()
  {

  }

  public void ScoreIncrement()
  {
    score++;
    ScoreIncrementEvent.Invoke(score);
  }

  public void GameOver()
  {
    GameOverEvent.Invoke(0);
    marioDeath.PlayOneShot(marioDeath.clip);
    alive = false;
  }
  public void ResetGame()
  {
    GameResetEvent.Invoke();
    alive = true;
    score = 0;
  }
}
