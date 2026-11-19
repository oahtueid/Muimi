using UnityEngine;

public class Pill : Item
{
    public override void Interact()
    {
        PlayerStat.Instance.takeDailyPill = true;
    }
}
