using UnityEngine;

public interface IPowerup
{

  public enum PowerupType
  {
    Coin = 0,
    MagicMushroom = 1,
    OneUpMushroom = 2,
    StarMan = 3
  }

  void DestroyPowerup();
  void SpawnPowerup();
  void ApplyPowerup(MonoBehaviour i);

  PowerupType powerupType
  {
    get;
  }

  bool hasSpawned
  {
    get;
  }
}
