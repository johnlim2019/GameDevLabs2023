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
  public SimpleGameEvent gameOverEvent;
  public SimpleGameEvent playerStompEvent;
  public SimpleGameEvent scoreIncrementEvent;

  void Start()
  {
    // gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
  }



  // void OnCollisionEnter2D(Collision2D col)
  // {
  //   if (col.gameObject.CompareTag("Ground"))
  //   {
  //     onGroundState = true;
  //   }
  // }

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.CompareTag("Enemies"))
    {
      if
        (other.transform.position.y + other.GetComponent<SpriteRenderer>().bounds.size.y / 2 > this.transform.position.y - this.GetComponent<SpriteRenderer>().bounds.size.y / 3 + 0.01)
      {
        // Debug.Log((other.transform.position.y + other.GetComponent<SpriteRenderer>().bounds.size.y / 2) + " " + (this.transform.position.y - this.GetComponent<SpriteRenderer>().bounds.size.y / 3 + 0.01));
        gameOverEvent.Raise(null);
      }
      else
      {
        // Debug.Log("SQUAHS");
        // Debug.Log((other.transform.position.y + other.GetComponent<SpriteRenderer>().bounds.size.y / 2) + " " + (this.transform.position.y - this.GetComponent<SpriteRenderer>().bounds.size.y / 3 + 0.01));
        playerStompEvent.Raise(null);
        scoreIncrementEvent.Raise(null);
        other.gameObject.GetComponent<EnemyMovement>().Die();
      }
    }

  }

  // private bool OnGroundCheck()
  // {
  //   if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, maxDistance, groundMask))
  //   {
  //     // Debug.Log("on ground");
  //     return true;
  //   }
  //   else
  //   {
  //     // Debug.Log("not on ground");
  //     return false;
  //   }
  // }

  // helper
  void OnDrawGizmos()
  {
    Gizmos.color = Color.yellow;
    Gizmos.DrawCube(transform.position - transform.up * maxDistance, boxSize);
  }
}
