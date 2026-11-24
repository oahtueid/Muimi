using System.Collections.Generic;
using UnityEngine;

public class FlagManager : PersistentManager<FlagManager>
{
    HashSet<GameFlag> flagSet = new();
    public bool HasFlag(GameFlag flag)
    {
        return flagSet.Contains(flag);
    }
    public void AddFlag(GameFlag flag)
    {
        flagSet.Add(flag);
    }
}
