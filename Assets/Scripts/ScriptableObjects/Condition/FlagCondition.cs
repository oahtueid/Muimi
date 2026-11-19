using UnityEngine;

[CreateAssetMenu(
    fileName = "FlagCondition",
    menuName = "Scriptable Objects/ConditionData/FlagCondition"
)]
public class FlagCondition : Condition
{
    public GameFlag flag;
    public bool invert = false;
    public override bool IsSatisfied()
    {
        bool result = FlagManager.Instance.HasFlag(flag);
        return invert ? !result : result;
    }
}
