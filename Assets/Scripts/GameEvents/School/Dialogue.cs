using System;

public class Dialogue : IEvent
{
    string pathName;
    Location location;

    public Dialogue(string pathName, Location location)
    {
        this.pathName = pathName;
        this.location = location;
    }

    public bool IsConditionMet()
    {
        return true;
    }

    public void Execute()
    {
        DialogueManager.Instance.GoToPath(pathName, onFinish: OnDialogueFinished);
    }

    private void OnDialogueFinished()
    {
        EventManager.Instance.RunNextEvent(location);
    }
}
