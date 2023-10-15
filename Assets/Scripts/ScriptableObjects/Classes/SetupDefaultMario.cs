
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableSM/Actions/SetupDefaultMario")]

public class SetupDefaultMario : Action
{
  public GameConstants gameConstants;

  public override void Act(StateController controller)
  {
    BuffStateController m = (BuffStateController)controller;

    m.gameObject.GetComponent<AudioSource>().Stop();

    gameConstants.StarManState = MarioState.Default;

    m.TurnOffFlicker();

    m.SetPowerup(PowerupType.Default);
  }
}
