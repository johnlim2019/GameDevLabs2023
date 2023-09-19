using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyLootBrickManager : MonoBehaviour
{
  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }
  public void ResetLootBrick()
  {
    foreach (BouncyLootBrick brick in transform.GetComponentsInChildren<BouncyLootBrick>())
    {
      brick.RestartGame();
    }
  }
}
