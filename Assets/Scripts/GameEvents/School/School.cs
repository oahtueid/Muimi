using UnityEngine;

public abstract class School : IEvent
{

    public virtual bool IsConditionMet()
    {
        return TimeManager.Instance.IsSchoolDay();
    }

    public abstract void Execute();
}
