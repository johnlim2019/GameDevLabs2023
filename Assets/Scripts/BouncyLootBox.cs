using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyLootBox : MonoBehaviour
{
  public Rigidbody2D boxBody;
  public SpringJoint2D springJoint;
  public GameObject mario;
  private Rigidbody2D marioBody;
  public bool BoxUsed;
  public Animator animator;
  public AudioSource coinAudio;
  public bool soundComplete = false;
  public Animator coinAnimator;
  public GameManager gameManager;


  // Start is called before the first frame update
  void Start()
  {
    boxBody = GetComponent<Rigidbody2D>();
    springJoint = GetComponent<SpringJoint2D>();
    mario = GameObject.FindGameObjectWithTag("Player");
    marioBody = mario.GetComponent<Rigidbody2D>();
    boxBody.bodyType = RigidbodyType2D.Static;
    animator = gameObject.GetComponent<Animator>();
    coinAudio = GetComponent<AudioSource>();
    gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
  }

  void CoinAnimationSound()
  {
    // play jump sound
    coinAnimator.SetBool("ActivateCoin", true);
    coinAudio.PlayOneShot(coinAudio.clip);
    soundComplete = true;
  }

  void OnCollisionEnter2D(Collision2D col)
  {
    // when collide from bottom trigger animation.
    float playerY = (float)(col.collider.transform.position.y - col.collider.GetComponent<SpriteRenderer>().bounds.size.y / 2);
    float otherY = (float)(this.transform.position.y + this.GetComponent<SpriteRenderer>().bounds.size.y / 2);
    if (col.gameObject.CompareTag("Player") && playerY < otherY && !soundComplete)
    {
      // activate box and score 
      activate();
    }
    else if (col.gameObject.CompareTag("PowerUp"))
    {
      boxBody.bodyType = RigidbodyType2D.Static;
    }
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

  void activate()
  {
    // update animator state
    animator.SetBool("Activated", true);
    // play sound 
    CoinAnimationSound();
    // score
    gameManager.ScoreIncrement();
  }

  // Update is called once per frame
  void Update()
  {
    // Debug.Log("Activated loot box " + activated);
  }

  public void RestartGame()
  {
    soundComplete = false;
    BoxUsed = false;
    coinAnimator.SetBool("ActivateCoin", false);
    animator.SetBool("Activated", false);
  }
}
