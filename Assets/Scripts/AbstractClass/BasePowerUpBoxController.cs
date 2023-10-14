using UnityEngine;

public abstract class BasePowerupBoxController : MonoBehaviour, IPowerUpBoxController
{
  public Animator powerupAnimator;
  public BasePowerup powerup;
  public Rigidbody2D boxBody;
  public SpringJoint2D springJoint;
  public Rigidbody2D marioBody;
  public bool BoxUsed;
  public Animator animator;
  public AudioSource powerUpAudio;
  public bool soundComplete = false;
  public SimpleGameEvent ScoreIncrementEvent;
  public BoxCollider2D boxCollider;
  public EdgeCollider2D edgeColliderTop;
  public EdgeCollider2D edgeColliderBottom;

  public void BaseStart()
  {
    boxBody = GetComponent<Rigidbody2D>();
    springJoint = GetComponent<SpringJoint2D>();
    boxBody.bodyType = RigidbodyType2D.Static;
    animator = gameObject.GetComponent<Animator>();
    powerUpAudio = GetComponent<AudioSource>();
    marioBody = GameObject.Find("Mario").GetComponent<Rigidbody2D>();
  }

  public abstract void PowerUpSound();
  public void BasePowerUpSound()
  {
    powerUpAudio.PlayOneShot(powerUpAudio.clip);
    soundComplete = true;
  }

  public abstract void Activate();
  public void BaseActivate()
  {
    // update animator state
    animator.SetBool("Activated", true);
  }

  public abstract void RestartGame();
  public void BaseRestartGame()
  {
    soundComplete = false;
    BoxUsed = false;
    animator.SetBool("Activated", false);
  }
}