using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class JumpOverGoomba : MonoBehaviour
{
  // private bool onGroundState;
  public Vector3 boxSize = new Vector3();
  public float maxDistance;
  public LayerMask groundMask;
  public SimpleGameEvent playerStompEvent;
  public SimpleGameEvent scoreIncrementEvent;
  public GameConstants gameConstants;

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.CompareTag("Enemies"))
    {
      if
        (other.transform.position.y + other.GetComponent<SpriteRenderer>().bounds.size.y / 2 > this.transform.position.y - this.GetComponent<SpriteRenderer>().bounds.size.y / 3 + 0.035
        && gameConstants.StarManState == MarioState.Default)
      {
        // Debug.Log((other.transform.position.y + other.GetComponent<SpriteRenderer>().bounds.size.y / 2) + " " + (this.transform.position.y - this.GetComponent<SpriteRenderer>().bounds.size.y / 3 + 0.035));
        // Debug.Log("take damage");
        GetComponent<MarioStateController>().SetPowerup(PowerupType.Damage);
      }
      else
      {
        // Debug.Log("STOMP");
        // Debug.Log((other.transform.position.y + other.GetComponent<SpriteRenderer>().bounds.size.y / 2) + " " + (this.transform.position.y - this.GetComponent<SpriteRenderer>().bounds.size.y / 3 + 0.035));
        playerStompEvent.Raise(null);
        scoreIncrementEvent.Raise(null);
        other.gameObject.GetComponent<EnemyMovement>().Die();
      }
    }

  }

  // helper
  void OnDrawGizmos()
  {
    Gizmos.color = Color.yellow;
    Gizmos.DrawCube(transform.position - transform.up * maxDistance, boxSize);
  }
}
