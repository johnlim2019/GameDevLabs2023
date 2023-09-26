using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionBoxPowerupManager : MonoBehaviour, IPowerUpBoxManager
{
  public void ResetPowerupBoxes()
  {
    foreach (QuestionBoxPowerupController box in transform.GetComponentsInChildren<QuestionBoxPowerupController>())
    {
      box.RestartGame();
    }
  }

}
