using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyLootBrick : BasePowerupBoxController
{


  // Start is called before the first frame update
  void Start()
  {
    base.BaseStart();
  }

  void OnCollisionEnter2D(Collision2D col)
  {
    // when collide from bottom trigger animation.
    if (col.gameObject.CompareTag("Player") && !BoxUsed && col.otherCollider == base.edgeColliderBottom)
    {
      // play sound 
      Activate();

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
    base.ScoreIncrementEvent.Raise(null);

  }

  public override void RestartGame()
  {
    BoxUsed = false;
    powerupAnimator.SetBool("ActivateCoin", BoxUsed);
  }
}
