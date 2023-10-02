using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

  public bool alive = true;
  private float originalX;
  private float maxOffset;
  private float enemyPatroltime;
  private int moveRight = -1;
  private Vector2 velocity;
  private Animator animator;
  private Rigidbody2D enemyBody;
  public Vector3 startPosition;
  public BoxCollider2D boxCollider;
  public AudioSource audioSource;
  public GameConstants gameConstants;

  void Start()
  {
    alive = true;
    maxOffset = gameConstants.goombaMaxOffset;
    enemyPatroltime = gameConstants.goombaPatrolTime;
    audioSource = GetComponent<AudioSource>();
    enemyBody = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();
    boxCollider = GetComponent<BoxCollider2D>();
    enemyBody.constraints = RigidbodyConstraints2D.FreezeRotation;
    // get the starting position
    originalX = transform.position.x;
    ComputeVelocity();
    animator.SetBool("Alive", alive);
  }
  void ComputeVelocity()
  {
    velocity = new Vector2((moveRight) * maxOffset / enemyPatroltime, 0);
  }
  void Movegoomba()
  {
    enemyBody.MovePosition(enemyBody.position + velocity * Time.fixedDeltaTime);
  }

  void Update()
  {
    if (alive)
    {
      if (Mathf.Abs(enemyBody.position.x - originalX) < maxOffset)
      {// move goomba
        Movegoomba();
      }
      else
      {
        // change direction
        moveRight *= -1;
        ComputeVelocity();
        Movegoomba();
      }
    }
  }
  public void RestartGame()
  {
    enemyBody.transform.localPosition = startPosition;
    enemyBody.bodyType = RigidbodyType2D.Dynamic;
    boxCollider.enabled = true;
    enemyBody.simulated = true;
    alive = true;
    moveRight = 1;
    ComputeVelocity();
    animator.SetBool("Alive", alive);
  }

  public void Die()
  {
    enemyBody.bodyType = RigidbodyType2D.Static;
    enemyBody.simulated = false;
    boxCollider.enabled = false;
    enemyBody.transform.localPosition -= new Vector3(0, 0.25f);
    audioSource.PlayOneShot(audioSource.clip);
    alive = false;
    animator.SetBool("Alive", alive);
  }
}