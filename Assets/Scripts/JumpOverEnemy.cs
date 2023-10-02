using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class JumpOverGoomba : MonoBehaviour
{
  public Transform enemyLocation;
  private bool onGroundState;
  private bool countScoreState = false;
  public Vector3 boxSize = new Vector3();
  public float maxDistance;
  public LayerMask groundMask;
  public LayerMask enemyMask;
  public GameManager gameManager;
  public BoxCollider2D MarioCollider;

  void Start()
  {
    gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
  }

  void FixedUpdate()
  {
    // mario jumps
    if (OnGroundCheck())
    {
      onGroundState = false;
      countScoreState = true;
    }

    // when jumping, and Goomba is near Mario and we haven't registered our score
    if (!onGroundState && countScoreState)
    {
      if (Mathf.Abs(transform.position.x - enemyLocation.position.x) > 1f)
      {
        countScoreState = false;
        // gameManager.ScoreIncrement();
        // Debug.Log(score);
      }
    }
  }

  void OnCollisionEnter2D(Collision2D col)
  {
    if (col.gameObject.CompareTag("Ground"))
    {
      onGroundState = true;
    }
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.CompareTag("Enemies") && gameManager.alive && countScoreState)
    {
      gameManager.GameOver();
    }
    if (other.gameObject.CompareTag("Enemies") && gameManager.alive && !countScoreState)
    {
      gameManager.marioKillGoomba();
      other.gameObject.GetComponent<EnemyMovement>().Die();
    }
  }

  private bool OnGroundCheck()
  {
    if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, maxDistance, groundMask))
    {
      // Debug.Log("on ground");
      return true;
    }
    else
    {
      // Debug.Log("not on ground");
      return false;
    }
  }

  // helper
  void OnDrawGizmos()
  {
    Gizmos.color = Color.yellow;
    Gizmos.DrawCube(transform.position - transform.up * maxDistance, boxSize);
  }
}
