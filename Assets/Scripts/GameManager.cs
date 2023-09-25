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
  public HUDManager hudManager;
  public AudioSource BGM;
  public AudioSource ugBGM;
  public PlayerMovement playerMovement;
  public BouncyLootBoxManager bouncyLootBoxManager;
  public BouncyLootBrickManager bouncyLootBrickManager;
  public EnemyManager enemyManager;
  public CameraController gameCamera;
  public QuestionBoxPowerupManager questionBoxPowerupManager;


  // unity events 
  public UnityEvent<int> GameOverEvent;
  public UnityEvent GameResetEvent;
  public UnityEvent<int> GameStartEvent;
  public UnityEvent<int> ScoreIncrementEvent;
  public UnityEvent PlayerStompEvent;
  public UnityEvent<bool> GamePausedEvent;

  [System.NonSerialized]
  public int score = 0; // we don't want this to show up in the inspector
  public bool alive = true;

  override public void Awake()
  {
    base.Awake();
    StartSceneHelper();
  }

  // Start is called before the first frame update
  void Start()
  {
    SceneManager.activeSceneChanged += StartScene;
  }

  private void StartSceneHelper()
  {
    marioDeath = GameObject.Find("MarioDeathSfx").GetComponent<AudioSource>();
    BGM = GameObject.Find("BGM").GetComponent<AudioSource>();
    ugBGM = GameObject.Find("UG-BGM").GetComponent<AudioSource>();
    playerMovement = GameObject.Find("Mario").GetComponent<PlayerMovement>();
    hudManager = GameObject.Find("Canvas").GetComponent<HUDManager>();
    bouncyLootBoxManager = GameObject.Find("Bouncy-Loot-Boxes").GetComponent<BouncyLootBoxManager>();
    bouncyLootBrickManager = GameObject.Find("Bouncy-Loot-Bricks").GetComponent<BouncyLootBrickManager>();
    enemyManager = GameObject.Find("Enemies").GetComponent<EnemyManager>();
    gameCamera = GameObject.Find("Main Camera").GetComponent<CameraController>();
    questionBoxPowerupManager = GameObject.Find("Bouncy-Powerup-Box").GetComponent<QuestionBoxPowerupManager>();

    GameOverEvent.AddListener(hudManager.GameOver);
    GameOverEvent.AddListener(playerMovement.GameOverScene);

    GameResetEvent.AddListener(hudManager.RestartGame);
    GameResetEvent.AddListener(bouncyLootBoxManager.ResetLootBox);
    GameResetEvent.AddListener(bouncyLootBrickManager.ResetLootBrick);
    GameResetEvent.AddListener(enemyManager.RestartGame);
    GameResetEvent.AddListener(playerMovement.ResetMario);
    GameResetEvent.AddListener(gameCamera.ResetCamera);
    GameResetEvent.AddListener(questionBoxPowerupManager.ResetPowerupBoxes);

    GameStartEvent.AddListener(hudManager.StartGame);

    ScoreIncrementEvent.AddListener(hudManager.ScoreIncrement);

    PlayerStompEvent.AddListener(playerMovement.PlayDeathImpulse);

    GamePausedEvent.AddListener(playerMovement.PauseGame);

    GameStartEvent.Invoke(score);
    BGM.Play();

  }
  public void StartScene(Scene current, Scene next)
  {
    Debug.Log("scene change " + next.name);
    StartSceneHelper();
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
    // score reset
    score = 0;
  }

  public void PlayerStomp()
  {
    PlayerStompEvent.Invoke();
  }

  public void OverGround2UnderGroundBGM()
  {
    BGM.Stop();
    ugBGM.Play();
  }
}
