using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyLootBrick : BasePowerupBoxController
{
  //   public Rigidbody2D boxBody;
  //   public SpringJoint2D springJoint;
  //   private Rigidbody2D marioBody;
  //   public AudioSource powerUpAudio;
  //   public bool BoxUsed = false;
  //   public Animator powerupAnimator;
  //   public GameManager gameManager;


  // Start is called before the first frame update
  void Start()
  {
    base.BaseStart();
  }

  void OnCollisionEnter2D(Collision2D col)
  {
    // when collide from bottom trigger animation.
    if (col.gameObject.CompareTag("Player"))
    {
      float playerY = (float)(col.collider.transform.position.y - col.collider.GetComponent<SpriteRenderer>().bounds.size.y / 2);
      float otherY = (float)(this.transform.position.y + this.GetComponent<SpriteRenderer>().bounds.size.y / 2);
      if (playerY < otherY && !BoxUsed)
      {
        // play sound 
        Activate();
      }
    }
    else if (col.gameObject.CompareTag("PowerUp"))
    {
      boxBody.bodyType = RigidbodyType2D.Static;
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

  public override void PowerUpSound()
  {
    base.BasePowerUpSound();
  }

  public override void Activate()
  {
    PowerUpSound();
    base.BoxUsed = true;
    powerupAnimator.SetBool("ActivateCoin", BoxUsed);
    gameManager.ScoreIncrement();

  }

  public override void RestartGame()
  {
    BoxUsed = false;
    powerupAnimator.SetBool("ActivateCoin", BoxUsed);
  }
}
