
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class FireFlowerPowerup : BasePowerup
{
  // setup this object's type
  // instantiate variables
  protected override void Start()
  {
    base.Start(); // call base class Start()
    this.type = PowerupType.FireFlower;
    base.rigidBody = GetComponent<Rigidbody2D>();
    boxCollider = GetComponent<BoxCollider2D>();
    base.rigidBody.bodyType = RigidbodyType2D.Kinematic;
    base.boxCollider.enabled = false;
  }

  void OnCollisionEnter2D(Collision2D col)
  {
    Debug.Log(col.gameObject.tag);
    if (col.gameObject.CompareTag("Player") && spawned)
    {
      ApplyPowerup(col.gameObject);
      base.DestroyPowerup();
    }
  }

  // interface implementation
  public override void SpawnPowerup()
  {
    spawned = true;
  }


  // interface implementation
  public override void ApplyPowerup(GameObject i)
  {
    // TODO: do something with the object
    Debug.Log("fireflower powerup");
  }
}