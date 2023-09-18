using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
  public Transform gameCamera;
  public Vector2 playerStartposition = new Vector3(7f, -3.862f, 0.0f);
  public CanvasGroup HUD;
  public TextMeshProUGUI scoreText;
  public TextMeshProUGUI endScoreText;
  public Button restartTopRight;
  public GameObject enemies;
  public CanvasGroup endgame;
  private Rigidbody2D marioBody;
  public PlayerMovement playerMovement;
  public Vector3 marioStartPosition = new Vector3(0.0f, -2.703f, 0.0f);
  public Vector3 cameraStartPosition = new Vector3(0, 1.05f, -10);
  public GameObject BouncyLootBoxes;

  public GameObject BouncyLootBricks;

  public AudioSource marioDeath;

  [System.NonSerialized]
  public int score = 0; // we don't want this to show up in the inspector
  public bool alive = true;

  // Start is called before the first frame update
  void Start()
  {
    endgame.alpha = 0f;
    marioBody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    BouncyLootBoxes = GameObject.Find("Bouncy-Loot-Boxes");
    BouncyLootBricks = GameObject.Find("Bouncy-Loot-Bricks");
    marioDeath = GameObject.Find("MarioDeathSfx").GetComponent<AudioSource>();
  }

  // Update is called once per frame
  void Update()
  {

  }

  public void ScoreIncrement()
  {
    score++;
    scoreText.text = "Score: " + score.ToString();
    endScoreText.text = "Score: " + score.ToString();
  }

  public void ResetLootBox()
  {
    foreach (BouncyLootBox box in BouncyLootBoxes.GetComponentsInChildren<BouncyLootBox>())
    {
      box.Reset();
    }
  }

  public void ResetLootBrick()
  {
    foreach (BouncyLootBrick brick in BouncyLootBricks.GetComponentsInChildren<BouncyLootBrick>())
    {
      brick.Reset();
    }
  }

  public void GameOver()
  {
    Debug.Log("GAmeOVER");
    marioDeath.PlayOneShot(marioDeath.clip);
    alive = false;
    HUD.alpha = 0.0f;
    restartTopRight.interactable = false;
    Color colorInvisible = new Color(255, 255, 255)
    {
      a = 0f
    };
    restartTopRight.GetComponent<Image>().color = colorInvisible;
    endgame.alpha = 1.0f;
    endgame.interactable = true; // enable interaction
    endgame.blocksRaycasts = true; // do not block raycasts
  }
  private void ResetMario()
  {
    // reset Mario 
    playerMovement.SpriteReset();
    marioBody.transform.position = marioStartPosition;
    playerMovement.marioAnimator.SetTrigger("gameRestart");
    alive = true;
  }
  public void ResetGame()
  {
    ResetMario();
    gameCamera.position = cameraStartPosition;
    // reset position
    marioBody.transform.position = marioStartPosition;
    // score reset
    score = 0;
    scoreText.text = "Score: 0";
    endScoreText.text = "Score: 0";
    // close the endgame canvas
    endgame.alpha = 0f;
    endgame.interactable = false; // disable interaction
    endgame.blocksRaycasts = false; // do not block raycasts
    // Set up HUD
    HUD.alpha = 1.0f;
    restartTopRight.interactable = true;
    Color colorVisible = new Color(255, 255, 255)
    {
      a = 1f
    };
    restartTopRight.GetComponent<Image>().color = colorVisible;
    // reset Goomba
    foreach (Transform eachChild in enemies.transform)
    {
      eachChild.transform.localPosition = eachChild.GetComponent<EnemyMovement>().startPosition;
    }
    // reset loot boxes
    ResetLootBox();
    ResetLootBrick();
  }
}
