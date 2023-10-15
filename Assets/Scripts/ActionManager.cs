using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;


public class ActionManager : MonoBehaviour
{
  public MarioActions marioActions;
  public UnityEvent jump;
  public UnityEvent jumpHold;
  public UnityEvent<int> moveCheck;
  public UnityEvent click;
  public UnityEvent ZAction;

  void Start()
  {
    marioActions = new MarioActions();
    marioActions.gameplay.Enable();
    marioActions.gameplay.jump.performed += OnJumpAction;
    marioActions.gameplay.jumphold.performed += OnJumpHoldAction;
    marioActions.gameplay.move.started += OnMoveAction;
    marioActions.gameplay.move.canceled += OnMoveAction;
    marioActions.gameplay.ZAction.performed += OnZAction;
    // marioActions.gameplay.click.started += OnClickAction;
    // marioActions.gameplay.click.canceled += OnClickAction;
    // marioActions.gameplay.click.performed += OnClickAction;
    // marioActions.gameplay.point.performed += OnPointAction;
  }

  public void OnJumpHoldAction(InputAction.CallbackContext context)
  {
    if (context.started)
    // Debug.Log("JumpHold was started");
    { }
    else if (context.performed)
    {
      // Debug.Log("JumpHold was performed");
      // Debug.Log(context.duration);
      jumpHold.Invoke();
    }
    else if (context.canceled)
    { }
    // Debug.Log("JumpHold was cancelled");
  }

  // called twice, when pressed and unpressed
  public void OnJumpAction(InputAction.CallbackContext context)
  {
    if (context.started)
    {
      // Debug.Log("Jump was started");
    }
    else if (context.performed)
    {
      jump.Invoke();
      // Debug.Log("Jump was performed");
    }
    else if (context.canceled)
    { }
    // Debug.Log("Jump was cancelled");

  }

  // called twice, when pressed and unpressed
  public void OnMoveAction(InputAction.CallbackContext context)
  {
    // Debug.Log("OnMoveAction callback invoked");
    if (context.started)
    {
      // Debug.Log("move started");
      int faceRight = context.ReadValue<float>() > 0 ? 1 : -1;
      moveCheck.Invoke(faceRight);
    }
    if (context.canceled)
    {
      // Debug.Log("move stopped");
      moveCheck.Invoke(0);
    }
  }

  public void OnZAction(InputAction.CallbackContext context)
  {
    // Debug.Log("Z pressed");
    if (context.performed)
    {
      ZAction.Invoke();
    }
  }



}
