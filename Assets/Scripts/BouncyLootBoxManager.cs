using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyLootBoxManager : MonoBehaviour
{
  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  public void ResetLootBox()
  {
    foreach (BouncyLootBox box in transform.GetComponentsInChildren<BouncyLootBox>())
    {
      box.RestartGame();
    }
  }

}
