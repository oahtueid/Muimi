using UnityEngine;

public class Sleep : MonoBehaviour, IEvent
{
    public bool IsConditionMet()
    {
        return TimeManager.Instance.CurrentSession == Session.Night;
    }

    public void Execute()
    {
        if (IsConditionMet())
        {
            MemoryManager.Instance.UnlockNewMemory(
                PlayerStat.Instance.takeDailyPill ? MemoryType.Fake : MemoryType.Real
            );
            TimeManager.Instance.AdvanceTime();
            PlayerStat.Instance.takeDailyPill = false;
        }
        else
        {
            Debug.Log("now is not the time");
        }
    }
}
