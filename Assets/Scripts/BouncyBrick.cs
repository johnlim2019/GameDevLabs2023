using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyBrick : MonoBehaviour
{
  public Rigidbody2D boxBody;
  public SpringJoint2D springJoint;
  public GameObject mario;
  private Rigidbody2D marioBody;


  // Start is called before the first frame update
  void Start()
  {
    boxBody = GetComponent<Rigidbody2D>();
    springJoint = GetComponent<SpringJoint2D>();
    mario = GameObject.FindGameObjectWithTag("Player");
    marioBody = mario.GetComponent<Rigidbody2D>();
    boxBody.bodyType = RigidbodyType2D.Static;
  }

  // called 50 times a second
  void FixedUpdate()
  {
    if (marioBody.position.y > boxBody.position.y)
      boxBody.bodyType = RigidbodyType2D.Static;
    else
      boxBody.bodyType = RigidbodyType2D.Dynamic;
  }


  // Update is called once per frame
  void Update()
  {
    // Debug.Log("Activated loot box " + activated);
  }
}
