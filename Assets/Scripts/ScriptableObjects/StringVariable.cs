using UnityEngine;

[CreateAssetMenu(fileName = "StrVariable", menuName = "ScriptableObjects/StrVariable", order = 2)]
public class StrVariable : Variable<string>
{

  public string nextMap;
  public override void SetValue(string value)
  {
    _value = value;
    nextMap = value;
  }

  // overload
  public void SetValue(StrVariable value)
  {
    SetValue(value.Value);
    nextMap = value.Value;
  }

}