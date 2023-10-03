using UnityEngine;

[CreateAssetMenu(fileName = "ScoreVariable", menuName = "ScriptableObjects/ScoreVariable", order = 2)]
public class ScoreVariable : Variable<int>
{

  public override void SetValue(int value)
  {
    _value = value;
  }

  // overload
  public void SetValue(IntVariable value)
  {
    SetValue(value.Value);
  }

  public void ApplyChange(int amount)
  {
    this.Value += amount;
  }

  public void ApplyChange(IntVariable amount)
  {
    ApplyChange(amount.Value);
  }

  public void ResetValue()
  {
    _value = 0;
  }

}