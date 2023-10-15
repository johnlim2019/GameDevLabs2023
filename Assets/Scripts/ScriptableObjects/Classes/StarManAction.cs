
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableSM/Actions/StarManAction")]
public class StarManAction : Action
{
  public GameConstants gameConstants;
  public AudioClip StarMan;
  public override void Act(StateController controller)
  {
    BuffStateController m = (BuffStateController)controller;

    m.gameObject.GetComponent<AudioSource>().PlayOneShot(StarMan);

    gameConstants.StarManState = MarioState.StarManMario;

    m.SetRendererToFlicker();
  }
}
