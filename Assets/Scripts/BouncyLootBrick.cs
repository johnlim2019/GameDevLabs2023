using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyLootBrick : MonoBehaviour
{
  public Rigidbody2D boxBody;
  public SpringJoint2D springJoint;
  public GameObject mario;
  private Rigidbody2D marioBody;
  public AudioSource coinAudio;
  public bool soundComplete = false;
  public Animator coinAnimator;


  // Start is called before the first frame update
  void Start()
  {
    boxBody = GetComponent<Rigidbody2D>();
    springJoint = GetComponent<SpringJoint2D>();
    marioBody = mario.GetComponent<Rigidbody2D>();
    boxBody.bodyType = RigidbodyType2D.Static;
    coinAudio = GetComponent<AudioSource>();
  }

  void CoinAnimationSound()
  {
    // play jump sound
    coinAnimator.SetTrigger("ActivateCoin");
    coinAudio.PlayOneShot(coinAudio.clip);
    soundComplete = true;
  }

  void OnCollisionEnter2D(Collision2D col)
  {
    // when collide from bottom trigger animation.
    if (col.gameObject.CompareTag("Player") && marioBody.position.y < boxBody.position.y && !soundComplete)
    {
      // play sound 
      try
      {
        CoinAnimationSound();
      }
      catch
      {
        return;
      }
    }
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
