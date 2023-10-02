using UnityEngine;

[CreateAssetMenu(fileName = "BoolVariable", menuName = "ScriptableObjects/BoolVariable", order = 2)]
public class BoolVariable : Variable<bool>
{

  public bool alive;
  public override void SetValue(bool value)
  {
    _value = value;
    alive = value;
  }

  // overload
  public void SetValue(BoolVariable value)
  {
    SetValue(value.Value);
    alive = value.Value;
  }

}