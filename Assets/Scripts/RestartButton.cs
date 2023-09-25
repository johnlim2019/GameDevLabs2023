using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartButton : MonoBehaviour, IInteractiveButton
{
  GameManager gameManager;

  public void ButtonClick()
  {
    gameManager.ResetGame();
  }

  void Awake()
  {
    gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
  }

  // Update is called once per frame
  void Update()
  {

  }
}
