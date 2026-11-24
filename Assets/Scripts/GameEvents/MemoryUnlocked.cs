using UnityEngine;

public class MemoryUnlocked : IEvent
{
    private MemoryData memoryData;

    public MemoryUnlocked(MemoryData memoryData)
    {
        this.memoryData = memoryData;
    }

    public bool IsConditionMet()
    {
        return true;
    }

    public void Execute()
    {
        Debug.Log($"Memory Unlocked: {memoryData}: {memoryData.description}");
        // Additional logic for when the memory is unlocked can be added here
    }
}
