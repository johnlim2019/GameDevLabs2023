using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyLootBrick : MonoBehaviour
{
  public Rigidbody2D boxBody;
  public SpringJoint2D springJoint;
  public GameObject mario;
  private Rigidbody2D marioBody;
  public AudioSource coinAudio;
  public bool BrickUsed = false;
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
    coinAudio = GetComponent<AudioSource>();
    gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
  }

  void CoinAnimationSound()
  {
    // play jump sound
    BrickUsed = true;
    coinAnimator.SetBool("ActivateCoin", BrickUsed);
    coinAudio.PlayOneShot(coinAudio.clip);
    gameManager.ScoreIncrement();
  }

  void OnCollisionEnter2D(Collision2D col)
  {
    // when collide from bottom trigger animation.
    if (col.gameObject.CompareTag("Player") && marioBody.position.y < boxBody.position.y && !BrickUsed)
    {
      // play sound 
      CoinAnimationSound();
    }
  }
  // called 50 times a second
  void FixedUpdate()
  {
    if (marioBody.position.y > boxBody.position.y)
      boxBody.bodyType = RigidbodyType2D.Static;
    else
      boxBody.bodyType = RigidbodyType2D.Dynamic;
  }


  // Update is called once per frame
  void Update()
  {
    // Debug.Log("Activated loot box " + activated);
  }

  public void RestartGame()
  {
    BrickUsed = false;
    coinAnimator.SetBool("ActivateCoin", BrickUsed);
    // Debug.Log("BrickUsed " + BrickUsed);
  }
}
