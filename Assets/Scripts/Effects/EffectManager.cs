using System;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;

public class EffectManager : PersistentManager<EffectManager>
{
    public List<EffectEntry> effects;
    public Dictionary<string, Effect> effectDict;
    HashSet<Effect> activeEffects = new();

    void Awake()
    {
        effectDict = new();
        foreach (EffectEntry entry in effects)
        {
            effectDict[entry.name] = entry.effect;
        }
    }

    public void Play(string effectName)
    {
        Play(effectName, null);
    }

    public void Play(string effectName, Action onComplete)
    {
        if (!effectDict.TryGetValue(effectName, out Effect effect))
        {
            Debug.LogWarning("Effect not found: " + effectName);
            return;
        }
        effect.StartEffect(onComplete);
        activeEffects.Add(effect);
    }

    public void Stop(string effectName)
    {
        if (!effectDict.TryGetValue(effectName, out Effect effect))
        {
            Debug.LogWarning("Effect not found: " + effectName);
            return;
        }
        effect.StopEffect();
        activeEffects.Remove(effect);
    }

    public void StopAll()
    {
        foreach (Effect effect in activeEffects)
        {
            effect.StopEffect();
        }
        activeEffects.Clear();
    }
}

[Serializable]
public class EffectEntry
{
    public string name;
    public Effect effect;
}
