
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class MoveStart1_2 : MonoBehaviour
{
  public UnityEvent goUnderGround;
  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.tag == "Player")
    {
      other.transform.position = new Vector3(-1f, -14f, 0);
      goUnderGround.Invoke();
    }
  }
}