using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class GameManager : MonoBehaviour
{
  public Transform gameCamera;
  public TextMeshProUGUI scoreText;
  public TextMeshProUGUI endScoreText;
  private Rigidbody2D marioBody;
  public PlayerMovement playerMovement;
  public Vector3 cameraStartPosition = new Vector3(0, 1.05f, -10);
  public GameObject BouncyLootBoxes;
  public GameObject BouncyLootBricks;
  public AudioSource marioDeath;

  public UnityEvent<int> GameOverEvent;
  public UnityEvent GameResetEvent;
  public UnityEvent GameStartEvent;
  public UnityEvent<int> ScoreIncrementEvent;
  public UnityEvent PlayerStompEvent;

  [System.NonSerialized]
  public int score = 0; // we don't want this to show up in the inspector
  public bool alive = true;

  // Start is called before the first frame update
  void Start()
  {
    GameStartEvent.Invoke();
    marioDeath = GameObject.Find("MarioDeathSfx").GetComponent<AudioSource>();
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
    gameCamera.position = cameraStartPosition;
    // score reset
    score = 0;
  }

  public void PlayerStomp()
  {
    PlayerStompEvent.Invoke();
  }
}
