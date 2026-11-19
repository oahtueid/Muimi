using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Glitch : Effect
{
    public Volume glitchVolume;
    ChromaticAberration chromaticAberration;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() { }

    // Update is called once per frame
    void Update() { }

    public override void StartEffect(Action onComplete = null)
    {
        glitchVolume.profile.TryGet(out chromaticAberration);
        chromaticAberration.intensity.value = 1f;
    }

    public override void StopEffect()
    {
        chromaticAberration.intensity.value = 0;
    }
}
