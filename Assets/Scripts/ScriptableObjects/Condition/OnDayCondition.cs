using UnityEngine;

[CreateAssetMenu(
    fileName = "NewCondition",
    menuName = "Scriptable Objects/ConditionData/OnDayCondition"
)]
public class OnDayCondition : Condition
{
    [SerializeField]
    int day;

    public override bool IsSatisfied()
    {
        return TimeManager.Instance.CurrentDay == day;
    }
}
