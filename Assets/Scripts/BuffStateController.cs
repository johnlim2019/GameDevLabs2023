using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffStateController : StateController
{
  public PowerupType currentPowerupType = PowerupType.Default;

  public MarioState shouldBeNextState = MarioState.Default;

  private SpriteRenderer spriteRenderer;

  public GameConstants gameConstants;


  public override void Start()
  {
    base.Start();
    GameRestart(); // clear powerup in the beginning, go to start state
  }

  // this should be added to the GameRestart EventListener as callback
  public void GameRestart()
  {
    // clear powerup
    currentPowerupType = PowerupType.Default;
    // set the start state
    TransitionToState(startState);
  }

  public void SetPowerup(PowerupType i)
  {
    currentPowerupType = i;
  }

  public void SetRendererToFlicker()

  {
    spriteRenderer = GetComponent<SpriteRenderer>();
    StartCoroutine(BlinkSpriteRenderer());
  }
  public void TurnOffFlicker()
  {
    spriteRenderer = GetComponent<SpriteRenderer>();
    spriteRenderer.color = Color.white;

  }
  private IEnumerator BlinkSpriteRenderer()
  {
    spriteRenderer = GetComponent<SpriteRenderer>();
    Color originalColor = Color.white;
    Color blackColor = Color.black;
    while (string.Equals(currentState.name, "StarManMario", StringComparison.OrdinalIgnoreCase))
    {
      if (spriteRenderer.color == originalColor)
      {
        spriteRenderer.color = blackColor;
      }
      else
      {
        spriteRenderer.color = originalColor;
      }

      // Wait for the specified blink interval
      yield return new WaitForSeconds(gameConstants.flickerInterval);
    }

    spriteRenderer.enabled = true;
  }

}