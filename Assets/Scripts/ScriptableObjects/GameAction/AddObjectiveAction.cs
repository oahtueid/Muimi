using UnityEngine;

[CreateAssetMenu(
    fileName = "AddObjectiveAction",
    menuName = "Scriptable Objects/Actions/AddObjective"
)]
public class AddObjectiveAction : GameAction
{
    [SerializeField]
    ObjectiveData objectiveData;

    public override void Execute()
    {
        ObjectiveManager.Instance.AddObjective(objectiveData);
    }
}
