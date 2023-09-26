using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyLootBoxManager : MonoBehaviour, IPowerUpBoxManager
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
    foreach (BouncyLootBox box in transform.GetComponentsInChildren<BouncyLootBox>())
    {
      box.RestartGame();
    }
  }

}
