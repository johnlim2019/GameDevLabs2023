using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConstants", menuName = "ScriptableObjects/GameConstants", order = 1)]
public class GameConstants : ScriptableObject
{
  public int maxLives;
  // Mario's movement
  public int speed;
  public int maxSpeed;
  public int upSpeed;
  public int deathImpulse;
  public Vector3 marioStartingPosition;
  public MarioState StarManState;
  public float flickerInterval;
  // Goomba's movement
  public float goombaPatrolTime;
  public float goombaMaxOffset;

}
