
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class MoveStart1_2 : MonoBehaviour
{
  GameManager gameManager;
  CameraController cameraController;
  public SimpleGameEvent goUnderGround;

  void Start()
  {
    gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    cameraController = GameObject.Find("Main Camera").GetComponent<CameraController>();
  }
  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.tag == "Player")
    {
      other.transform.position = new Vector3(-1f, -13.5f, 0);
      other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
      goUnderGround.Raise(null);
    }
  }
}