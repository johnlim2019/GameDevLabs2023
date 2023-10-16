using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RestartButton : MonoBehaviour, IInteractiveButton
{
  // GameManager gameManager;

  // public UnityEvent gameRestart;
  public SimpleGameEvent gameRestart;

  public void ButtonClick()
  {
    gameRestart.Raise(null);
  }

  void Awake()
  {
    // gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

  }

  // Update is called once per frame
  void Update()
  {

  }
}
