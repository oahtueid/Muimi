using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "NewMemory", menuName = "Scriptable Objects/MemoryData", order = 1)]
public class MemoryData : ScriptableObject
{
    public string memoryName;
    public string description;
    public Sprite memoryImage;
    public MemoryType memoryType;
}

public enum MemoryType
{
    Fake,
    Real,
}
