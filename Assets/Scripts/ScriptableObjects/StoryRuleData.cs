using UnityEngine;

[CreateAssetMenu(fileName = "NewStoryRule", menuName = "Scriptable Objects/StoryRuleData")]
public class StoryRuleData : ScriptableObject
{
    public GameTarget triggerTarget;
    public Condition[] conditions;
    public GameAction[] actions;

    public bool IsSatisfied()
    {
        foreach (Condition condition in conditions)
        {
            if (!condition.IsSatisfied())
                return false;
        }
        return true;
    }

    public void Execute()
    {
        foreach (GameAction action in actions)
        {
            if (action != null)
                action.Execute();
        }
    }
}
