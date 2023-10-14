using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyLootBox : BasePowerupBoxController
{
  // Start is called before the first frame update
  void Start()
  {
    base.BaseStart();
  }

  void OnCollisionEnter2D(Collision2D col)
  {

    // when collide from bottom trigger animation.
    // float playerY = (float)(col.collider.transform.position.y - col.collider.GetComponent<SpriteRenderer>().bounds.size.y / 2);
    // float otherY = (float)(this.transform.position.y + this.GetComponent<SpriteRenderer>().bounds.size.y / 2);
    // if (col.gameObject.CompareTag("Player") && playerY < otherY && !soundComplete)
    if (col.otherCollider == base.edgeColliderBottom && col.gameObject.CompareTag("Player") && !soundComplete)
    {
      // activate box and score 
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

  public override void PowerUpSound()
  {
    base.BasePowerUpSound();
  }

  public override void Activate()
  {
    base.BaseActivate();
    // trigger powerup animation.
    powerupAnimator.SetBool("ActivateCoin", true);
    // play sound 
    PowerUpSound();
    // score
    base.ScoreIncrementEvent.Raise(null);
  }



  public override void RestartGame()
  {
    base.BaseRestartGame();
    powerupAnimator.SetBool("ActivateCoin", false);
  }
}
