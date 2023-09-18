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
  public float speed = 20;
  public float upSpeed = 15;
  public float maxSpeed = 30;
  public float deathImpulse = 15;
  private bool onGroundState = true;
  private bool faceRightState = true;
  private bool moving = false;
  private bool jumpedState = false;
  private Rigidbody2D marioBody;
  private SpriteRenderer marioSprite;
  public Animator marioAnimator;
  public AudioSource marioAudio;
  public GameManager gameManager;


  void PlayDeathImpulse()
  {
    marioBody.AddForce(Vector2.up * deathImpulse, ForceMode2D.Impulse);
  }

  void GameOverScene()
  {
    PlayDeathImpulse();
    gameManager.GameOver();
    marioAnimator.Play("Mario Death");
  }

  // Start is called before the first frame update
  void Start()
  {
    // Set to be 30 FPS
    Application.targetFrameRate = 30;
    marioBody = GetComponent<Rigidbody2D>();
    marioSprite = GetComponent<SpriteRenderer>();
    marioBody.constraints = RigidbodyConstraints2D.FreezeRotation;
    marioAnimator = GetComponent<Animator>();
    marioAudio = GetComponent<AudioSource>();
    marioAnimator.SetBool("onGround", onGroundState);
    gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
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
    if (gameManager.alive && onGroundState)
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
    if (gameManager.alive && jumpedState)
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
    Debug.Log(other.gameObject.tag + " " + gameManager.alive);
    if (other.gameObject.CompareTag("Enemies") && gameManager.alive)
    {
      Debug.Log("Collided with goomba!");
      GameOverScene();
    }
  }

  public void RestartButtonCallback(int input)
  {
    // Debug.Log("Restart!");
    // reset everything
    gameManager.ResetGame();
    // resume time
    Time.timeScale = 1.0f;
  }

  public void SpriteReset()
  {
    faceRightState = true;
    marioSprite.flipX = false;
  }

}