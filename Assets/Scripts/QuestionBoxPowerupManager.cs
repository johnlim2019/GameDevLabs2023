using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionBoxPowerupManager : MonoBehaviour
{
  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  public void ResetPowerupBoxes()
  {
    foreach (QuestionBoxPowerupController box in transform.GetComponentsInChildren<QuestionBoxPowerupController>())
    {
      box.RestartGame();
    }
  }

}
