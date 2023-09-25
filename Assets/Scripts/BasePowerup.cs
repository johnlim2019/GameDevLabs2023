using UnityEngine;

public abstract class BasePowerup : MonoBehaviour, IPowerup
{
  public IPowerup.PowerupType type;
  public bool spawned = false;
  protected bool consumed = false;
  protected bool goRight = true;
  protected Rigidbody2D rigidBody;
  protected BoxCollider2D boxCollider;
  public Animator animator;

  // base methods
  protected virtual void Start()
  {
    rigidBody = GetComponent<Rigidbody2D>();
    boxCollider = GetComponent<BoxCollider2D>();
  }

  // interface methods
  // 1. concrete methods
  public IPowerup.PowerupType powerupType
  {
    get // getter
    {
      return type;
    }
  }

  public bool hasSpawned
  {
    get // getter
    {
      return spawned;
    }
  }

  IPowerup.PowerupType IPowerup.powerupType => throw new System.NotImplementedException();

  public void DestroyPowerup()
  {
    if (animator.GetCurrentAnimatorStateInfo(0).IsName("Spawn"))
    {
      animator.SetTrigger("spawned");
    }
    spawned = false;
    rigidBody.velocity = new Vector3(0, 0, 0);
    rigidBody.bodyType = RigidbodyType2D.Static;
    boxCollider.enabled = false;
    goRight = true;
    rigidBody.transform.position = transform.parent.position + new Vector3(0, 0.5f, 0);
  }

  // 2. abstract methods, must be implemented by derived classes
  public abstract void SpawnPowerup();
  public abstract void ApplyPowerup(MonoBehaviour i);
}
