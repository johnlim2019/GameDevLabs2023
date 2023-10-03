using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector2;

public class PlayerMovement : MonoBehaviour
{
  public GameConstants gameConstants;
  public Vector3 marioStartPosition = new Vector3(0.0f, -2.703f, 0.0f);
  private float speed;
  private float upSpeed;
  private float maxSpeed;
  private float deathImpulse;
  private bool onGroundState = true;
  private bool faceRightState = true;
  private bool moving = false;
  private bool jumpedState = false;
  private bool isPaused = false;
  private Rigidbody2D marioBody;
  private SpriteRenderer marioSprite;
  public Animator marioAnimator;
  public AudioSource marioAudio;
  public GameManager gameManager;
  public BoxCollider2D MarioCollider;
  public void PlayDeathImpulse()
  {
    marioBody.AddForce(Vector2.up * deathImpulse, ForceMode2D.Impulse);
  }

  public void GameOverScene(int value)
  {
    marioAnimator.Play("Mario Death");
    PlayDeathImpulse();
    MarioCollider.enabled = false;
  }

  // Start is called before the first frame update
  void Start()
  {
    speed = gameConstants.speed;
    maxSpeed = gameConstants.maxSpeed;
    deathImpulse = gameConstants.deathImpulse;
    upSpeed = gameConstants.upSpeed;
    // Set to be 30 FPS
    Application.targetFrameRate = 30;
    marioBody = GetComponent<Rigidbody2D>();
    marioSprite = GetComponent<SpriteRenderer>();
    marioBody.constraints = RigidbodyConstraints2D.FreezeRotation;
    marioAnimator = GetComponent<Animator>();
    marioAudio = GetComponent<AudioSource>();
    marioAnimator.SetBool("onGround", onGroundState);
    gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    MarioCollider = GetComponent<BoxCollider2D>();
    SetStartingPosition(SceneManager.GetActiveScene());
  }
  public void SetStartingPosition(Scene current)
  {
    if (current.name == "1-2")
    {
      // change the position accordingly in your World-1-2 case
      transform.position = new Vector3(4f, -1.06f, 0.0f);
    }
  }

  // Update is called once per frame
  void Update()
  {
    // Debug.Log(onGroundState);
    if (gameManager.alive)
    {
      marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));
    }
  }


  void OnCollisionEnter2D(Collision2D col)
  {
    if (col.gameObject.CompareTag("PowerUp"))
    {
      return;
    }
    ContactPoint2D contact = col.contacts[0];
    float otherY = (float)(contact.collider.transform.position.y + contact.collider.GetComponent<SpriteRenderer>().bounds.size.y / 2 - 0.1);
    float playerY = (float)(contact.otherCollider.transform.position.y - contact.otherCollider.GetComponent<SpriteRenderer>().bounds.size.y / 2 + 0.1);
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
    if (gameManager.alive && moving)
    {
      Move(faceRightState == true ? 1 : -1);
    }
  }

  void PlayJumpSound()
  {
    // play jump sound
    // Debug.Log("play jump audio");
    if (onGroundState)
      marioAudio.PlayOneShot(marioAudio.clip);
  }

  public void Jump()
  {
    if (gameManager.alive && onGroundState && !isPaused)
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
    if (gameManager.alive && jumpedState && !isPaused)
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

  public void PauseGame(bool value)
  {
    isPaused = value;
  }

  public void MoveCheck(int value)
  {
    if (value == 0 || isPaused)
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

  public void ResetMario()
  {
    SpriteReset();
    MarioCollider.enabled = true;
    marioBody.transform.position = marioStartPosition;
    marioAnimator.ResetTrigger("onDeath");
  }

  public void SpriteReset()
  {
    if (marioAnimator.GetCurrentAnimatorStateInfo(0).IsName("Mario Death"))
    {
      marioAnimator.SetTrigger("gameRestart");
    }
    faceRightState = true;
    marioSprite.flipX = false;
  }

}