using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
  public string leftKey = "left";
  public string rightKey = "right";
  public string jumpKey = "space";
  public float speed = 25;
  public float upSpeed = 25;
  public float maxSpeed = 30;
  public float deathImpulse = 15;
  private bool onGroundState = true;
  private bool faceRightState = true;
  private bool moving = false;
  private bool jumpedState = false;
  private JumpOverGoomba scoreScript;
  private Rigidbody2D marioBody;
  private SpriteRenderer marioSprite;
  public TextMeshProUGUI scoreText;
  public TextMeshProUGUI endScoreText;
  public CanvasGroup HUD;
  public Button restartTopRight;
  public GameObject enemies;
  public CanvasGroup endgame;
  public Animator marioAnimator;
  public Transform gameCamera;
  public AudioClip marioDeath;

  // state
  [System.NonSerialized]
  public bool alive = true;


  public AudioSource marioAudio;


  void PlayDeathImpulse()
  {
    marioBody.AddForce(Vector2.up * deathImpulse, ForceMode2D.Impulse);
  }

  void GameOverScene()
  {
    PlayDeathImpulse();
    HUD.alpha = 0.0f;
    restartTopRight.interactable = false;
    endgame.alpha = 1.0f;
    endgame.interactable = true; // enable interaction
    endgame.blocksRaycasts = true; // do not block raycasts
    marioAnimator.Play("Mario Death");
    marioAudio.PlayOneShot(marioDeath);
    alive = false;
  }

  // Start is called before the first frame update
  void Start()
  {
    // Set to be 30 FPS
    Application.targetFrameRate = 30;
    scoreScript = GetComponent<JumpOverGoomba>();
    marioBody = GetComponent<Rigidbody2D>();
    marioSprite = GetComponent<SpriteRenderer>();
    marioBody.constraints = RigidbodyConstraints2D.FreezeRotation;
    endgame.alpha = 0f;
    marioAnimator.SetBool("onGround", onGroundState);
  }

  // Update is called once per frame
  void Update()
  {
    if (alive)
    {
      marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));
    }
  }


  void OnCollisionEnter2D(Collision2D col)
  {
    ContactPoint2D contact = col.contacts[0];
    float otherY = (float)(contact.collider.transform.position.y + contact.collider.GetComponent<SpriteRenderer>().bounds.size.y / 2 - 0.1);
    float playerY = (float)(contact.otherCollider.transform.position.y - contact.otherCollider.GetComponent<SpriteRenderer>().bounds.size.y / 2 + 0.1);
    // Debug.Log("otherY " + otherY);
    // Debug.Log("playerY " + playerY);
    if ((col.gameObject.CompareTag("Ground") || col.gameObject.CompareTag("Enemies") || (col.gameObject.CompareTag("Obstacles") && playerY > otherY)) && !onGroundState)
    {
      onGroundState = true;
      // update animator state
      marioAnimator.SetBool("onGround", onGroundState);
    }
  }

  // FixedUpdate is called 50 times a second
  void FixedUpdate()
  {
    if (alive && moving)
    {
      Move(faceRightState == true ? 1 : -1);
    }
  }

  void PlayJumpSound()
  {
    // play jump sound
    // Debug.Log("play jump audio");
    marioAudio.PlayOneShot(marioAudio.clip);
  }

  public void Jump()
  {
    if (alive && onGroundState)
    {
      // jump
      PlayJumpSound();
      marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
      onGroundState = false;
      jumpedState = true;
      // update animator state
      marioAnimator.SetBool("onGround", onGroundState);

    }
  }

  public void JumpHold()
  {
    if (alive && jumpedState)
    {
      // jump higher
      marioBody.AddForce(Vector2.up * upSpeed * 30, ForceMode2D.Force);
      jumpedState = false;

    }
  }

  void Move(int value)
  {

    Vector2 movement = new Vector2(value, 0);
    // check if it doesn't go beyond maxSpeed
    if (marioBody.velocity.magnitude < maxSpeed)
      marioBody.AddForce(movement * speed);
  }

  public void MoveCheck(int value)
  {
    if (value == 0)
    {
      moving = false;
    }
    else
    {
      FlipMarioSprite(value);
      moving = true;
      Move(value);
    }
  }

  void FlipMarioSprite(int value)
  {
    if (value == -1 && faceRightState)
    {
      faceRightState = false;
      marioSprite.flipX = true;
      if (marioBody.velocity.x > 0.10f)
        marioAnimator.SetTrigger("onSkid");
    }

    if (value == 1 && !faceRightState)
    {
      faceRightState = true;
      marioSprite.flipX = false;
      if (marioBody.velocity.x < -0.01f)
        marioAnimator.SetTrigger("onSkid");
    }
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.CompareTag("Enemies") && alive)
    {
      // Debug.Log("Collided with goomba!");
      marioAnimator.SetTrigger("onDeath");
      GameOverScene();
    }
  }

  public void RestartButtonCallback(int input)
  {
    // Debug.Log("Restart!");
    // reset everything
    ResetGame();
    // resume time
    Time.timeScale = 1.0f;
  }

  private void ResetGame()
  {
    // reset position
    marioBody.transform.position = new Vector3(0.0f, -2.703f, 0.0f);
    // reset sprite direction
    faceRightState = true;
    marioSprite.flipX = false;
    marioAnimator.SetTrigger("gameRestart");
    gameCamera.position = new Vector3(0, 1.05f, -10);
    alive = true;
    // reset score
    scoreText.text = "Score: 0";
    endScoreText.text = "Score: 0";
    scoreScript.Restart();
    // close the endgame canvas
    endgame.alpha = 0f;
    endgame.interactable = false; // disable interaction
    endgame.blocksRaycasts = false; // do not block raycasts
    // Set up HUD
    HUD.alpha = 1.0f;
    restartTopRight.interactable = true;

    // reset Goomba
    foreach (Transform eachChild in enemies.transform)
    {
      eachChild.transform.localPosition = eachChild.GetComponent<EnemyMovement>().startPosition;
    }

  }
}