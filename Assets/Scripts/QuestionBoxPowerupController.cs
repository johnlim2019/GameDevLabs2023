using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuestionBoxPowerupController : MonoBehaviour
{
  public Animator powerupAnimator;
  public BasePowerup powerup;
  public Rigidbody2D boxBody;
  public SpringJoint2D springJoint;
  public GameObject mario;
  private Rigidbody2D marioBody;
  public bool BoxUsed;
  public Animator animator;
  public AudioSource powerUpAudio;
  public bool soundComplete = false;
  public GameManager gameManager;
  void Start()
  {
    boxBody = GetComponent<Rigidbody2D>();
    springJoint = GetComponent<SpringJoint2D>();
    mario = GameObject.FindGameObjectWithTag("Player");
    marioBody = mario.GetComponent<Rigidbody2D>();
    boxBody.bodyType = RigidbodyType2D.Static;
    animator = gameObject.GetComponent<Animator>();
    powerUpAudio = GetComponent<AudioSource>();
    gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
  }

  // Update is called once per frame
  void Update()
  {

  }


  // called 50 times a second
  void FixedUpdate()
  {
    if (animator.GetCurrentAnimatorStateInfo(0).IsName("Done"))
    {
      BoxUsed = true;
    }
    else
    {
      BoxUsed = false;
    }
    if (marioBody.position.y > boxBody.position.y || BoxUsed)
      boxBody.bodyType = RigidbodyType2D.Static;
    else
      boxBody.bodyType = RigidbodyType2D.Dynamic;
  }


  private void OnCollisionEnter2D(Collision2D col)
  {
    if (col.gameObject.tag == "Player")
    {
      float playerY = (float)(col.collider.transform.position.y - col.collider.GetComponent<SpriteRenderer>().bounds.size.y / 2);
      float otherY = this.transform.position.y + 0.22f;
      Debug.Log(playerY + " " + otherY);
      if (!powerup.spawned && otherY > playerY)
      {
        powerup.SpawnPowerup();
        powerupAnimator.SetTrigger("spawned");
        activate();

      }
    }
  }

  void PowerUpSound()
  {
    // play jump sound
    powerUpAudio.PlayOneShot(powerUpAudio.clip);
    soundComplete = true;
  }

  void activate()
  {
    // update animator state
    animator.SetBool("Activated", true);
    // play sound 
    PowerUpSound();
  }

  public void RestartGame()
  {
    powerup.DestroyPowerup();
    powerup.spawned = false;
    soundComplete = false;
    BoxUsed = false;
    animator.SetBool("Activated", false);
  }

}