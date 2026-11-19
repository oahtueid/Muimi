using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "NewObjective", menuName = "Scriptable Objects/Objective/Data")]
public class ObjectiveData : ScriptableObject
{
    public string id;
    public string description;
    public GameTarget target;
    public ObjectiveType objectiveType;
    public bool hideObjectAfterCompleted = false;
    public List<GameAction> OnCompleted;

    public void Complete()
    {
        foreach (var action in OnCompleted)
        {
            if (action != null)
                action.Execute();
        }
    }
}

public enum ObjectiveType
{
    LocationEntered,
    ItemInteracted,
}
