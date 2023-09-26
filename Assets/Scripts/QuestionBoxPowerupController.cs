using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuestionBoxPowerupController : BasePowerupBoxController
{
  void Start()
  {
    base.BaseStart();
  }

  // called 50 times a second
  void FixedUpdate()
  {
    if (base.animator.GetCurrentAnimatorStateInfo(0).IsName("Done"))
    {
      base.BoxUsed = true;
    }
    else
    {
      base.BoxUsed = false;
    }
    if (base.marioBody.position.y > boxBody.position.y || BoxUsed)
      base.boxBody.bodyType = RigidbodyType2D.Static;
    else
      base.boxBody.bodyType = RigidbodyType2D.Dynamic;
  }


  private void OnCollisionEnter2D(Collision2D col)
  {
    if (col.gameObject.tag == "Player")
    {
      float playerY = (float)(col.collider.transform.position.y - col.collider.GetComponent<SpriteRenderer>().bounds.size.y / 2);
      float otherY = this.transform.position.y + 0.22f;
      // Debug.Log(playerY + " " + otherY);
      if (!powerup.spawned && otherY > playerY)
      {
        base.powerup.SpawnPowerup();
        base.powerupAnimator.SetTrigger("spawned");
        Activate();
      }
    }
  }

  public override void PowerUpSound()
  {
    base.BasePowerUpSound();
  }

  public override void Activate()
  {
    PowerUpSound();
    base.BaseActivate();
  }

  public override void RestartGame()
  {
    powerup.DestroyPowerup();
    powerup.spawned = false;
    base.BaseRestartGame();

  }
}