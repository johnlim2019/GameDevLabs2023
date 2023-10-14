using UnityEngine;

public interface IPowerup
{

  public enum PowerupType
  {
    Coin = 0,
    MagicMushroom = 1,
    OneUpMushroom = 2,
    StarMan = 3,
    FireFlower = 4
  }

  void DestroyPowerup();
  void SpawnPowerup();
  void ApplyPowerup(GameObject i);

  PowerupType powerupType
  {
    get;
  }

  bool hasSpawned
  {
    get;
  }
}
