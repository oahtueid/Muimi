using UnityEngine;

[CreateAssetMenu(
    fileName = "NewStoryRule",
    menuName = "Scriptable Objects/ConditionData/ObjectiveCompletedCondition"
)]
public class ObjectiveCompletedCondition : Condition
{
    [SerializeField]
    ObjectiveData objectiveData;

    public override bool IsSatisfied()
    {
        return ObjectiveManager.Instance.IsObjectiveCompleted(objectiveData.id);
    }
}
