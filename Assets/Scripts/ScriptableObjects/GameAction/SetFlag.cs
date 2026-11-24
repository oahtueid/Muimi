using UnityEngine;

[CreateAssetMenu(fileName = "SetFlag", menuName = "Scriptable Objects/Actions/SetFlag")]
public class SetFlag : GameAction
{
    public GameFlag flag;

    public override void Execute()
    {
        FlagManager.Instance.AddFlag(flag);
    }
}
