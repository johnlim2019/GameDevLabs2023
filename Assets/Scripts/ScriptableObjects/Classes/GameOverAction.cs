using UnityEngine;

[CreateAssetMenu(menuName = "PluggableSM/Actions/GameOver")]
public class GameOverAction : Action
{
  public SimpleGameEvent gameOverEvent;

  public override void Act(StateController controller)
  {
    gameOverEvent.Raise(null);
  }

}
