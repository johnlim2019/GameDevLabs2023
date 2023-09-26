
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PauseButtonController : MonoBehaviour, IInteractiveButton
{
  private bool isPaused = false;
  public Sprite pauseIcon;
  public Sprite playIcon;
  private Image image;
  public GameManager gameManager;

  // Start is called before the first frame update
  void Start()
  {
    image = GetComponent<Image>();
    gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
  }

  // Update is called once per frame
  void Update()
  {

  }

  public void ButtonClick()
  {
    Time.timeScale = isPaused ? 1.0f : 0.0f;
    isPaused = !isPaused;
    if (isPaused)
    {
      image.sprite = playIcon;
    }
    else
    {
      image.sprite = pauseIcon;
    }
    gameManager.GamePausedEvent.Invoke(isPaused);
    gameManager.ToggleBGM();
  }
}
