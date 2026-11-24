using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MemoryManager : PersistentManager<MemoryManager>
{
    public List<MemoryData> fakeMemories;
    public List<MemoryData> realMemories;
    public int currentRealMemoryIndex = 0;
    public Dictionary<MemoryData, bool> unlockedRealMemories;
    public int currentFakeMemoryIndex = 0;
    public Dictionary<MemoryData, bool> unlockedFakeMemories;

    void Awake()
    {
        unlockedRealMemories ??= new Dictionary<MemoryData, bool>();
        unlockedFakeMemories ??= new Dictionary<MemoryData, bool>();
    }

    public void UnlockNewMemory(MemoryType memoryType)
    {
        MemoryData unlockedMemory = null;
        Dictionary<MemoryData, bool> unlockedMemories = unlockedFakeMemories;

        if (memoryType == MemoryType.Fake)
        {
            unlockedMemory = fakeMemories[currentFakeMemoryIndex++];
        }
        else if (memoryType == MemoryType.Real)
        {
            unlockedMemory = realMemories[currentRealMemoryIndex++];
            unlockedMemories = unlockedRealMemories;
        }
        if (!unlockedMemory)
            return;

        if (unlockedMemories.ContainsKey(unlockedMemory))
        {
            unlockedMemories[unlockedMemory] = true;
        }
        else
        {
            unlockedMemories.Add(unlockedMemory, true);
        }

        EventManager.Instance.TriggerEvent(new MemoryUnlocked(unlockedMemory));
    }

    public bool IsMemoryUnlocked(MemoryData memoryData)
    {
        if (unlockedFakeMemories.ContainsKey(memoryData) && unlockedFakeMemories[memoryData])
        {
            return true;
        }
        if (unlockedRealMemories.ContainsKey(memoryData) && unlockedRealMemories[memoryData])
        {
            return true;
        }
        else
            return false;
    }
}
