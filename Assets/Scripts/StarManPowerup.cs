
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class StarManPowerup : BasePowerup
{
  // setup this object's type
  // instantiate variables
  private float force = 3f;
  protected override void Start()
  {
    base.Start(); // call base class Start()
    this.type = PowerupType.StarMan;
    base.rigidBody = GetComponent<Rigidbody2D>();
    boxCollider = GetComponent<BoxCollider2D>();
    base.rigidBody.bodyType = RigidbodyType2D.Static;
    base.boxCollider.enabled = false;
  }

  void OnCollisionEnter2D(Collision2D col)
  {
    // Debug.Log(col.gameObject.tag);
    if (col.gameObject.CompareTag("Player") && spawned)
    {
      ApplyPowerup(col.gameObject);
      DestroyPowerup();
    }
    else if (col.gameObject.CompareTag("Pipes"))
    {
      if (spawned)
      {
        goRight = !goRight;
        base.rigidBody.AddForce(Vector2.right * force * (goRight ? 1 : -1), ForceMode2D.Impulse);

      }
    }
  }

  private IEnumerator EnablePhysics()
  {
    base.rigidBody.bodyType = RigidbodyType2D.Dynamic;
    base.boxCollider.enabled = true;
    yield return null;
  }
  // interface implementation
  public override void SpawnPowerup()
  {
    StartCoroutine(EnablePhysics());
    spawned = true;
    base.rigidBody.velocity = new Vector3(0, 0);
    base.rigidBody.AddForce(Vector2.right * force, ForceMode2D.Impulse); // move to the right
  }


  // interface implementation
  public override void ApplyPowerup(GameObject i)
  {
    // TODO: do something with the object
    Debug.Log("Starman powerup");
  }
}