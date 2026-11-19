using System;
using UnityEngine;
using UnityEngine.Rendering;

public class Flashback : Effect
{
    public Volume flashbackVolume;
    private float targetWeight = 0f;

    void Start()
    {
        flashbackVolume.weight = targetWeight;
    }

    void Update()
    {
        if (flashbackVolume != null)
        {
            flashbackVolume.weight = Mathf.MoveTowards(
                flashbackVolume.weight,
                targetWeight,
                duration * Time.deltaTime
            );
        }
    }

    public override void StartEffect(Action onComplete = null)
    {
        targetWeight = 1f;
        flashbackVolume.enabled = true;
    }

    public override void StopEffect()
    {
        targetWeight = 0f;
        flashbackVolume.enabled = false;
    }
}
