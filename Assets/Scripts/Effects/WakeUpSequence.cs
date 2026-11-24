using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class WakeUpSequence : Effect
{
    public Volume glitchVolume;
    public Volume mainGameVolume;
    public float glitchDuration = 1f;
    public float wakeUpDuration = 3f;

    public Material eyeBlinkMaterial;
    const string EYE_OPEN = "_EyeOpen";

    ChromaticAberration chromatic;
    LensDistortion lensDistortion;
    ColorAdjustments colorAdjustments;
    public AnimationCurve eyeOpenCurves;
    public Sprite sceneWhenOpenEye;
    public Image imageScene;

    void Start()
    {
        glitchVolume.enabled = false;
        mainGameVolume.enabled = false;
        glitchVolume.profile.TryGet(out chromatic);
        glitchVolume.profile.TryGet(out lensDistortion);
        glitchVolume.profile.TryGet(out colorAdjustments);
        eyeBlinkMaterial.SetFloat(EYE_OPEN, 1f);
    }

    public override void StartEffect(System.Action onComplete = null)
    {
        glitchVolume.enabled = true;
        mainGameVolume.enabled = true;
        StartCoroutine(StartSequence());
    }

    public override void StopEffect()
    {
        StopCoroutine(StartSequence());
        glitchVolume.enabled = false;
        mainGameVolume.enabled = false;
    }

    IEnumerator StartSequence()
    {
        InputStateManager.Instance.Disable();

        float elapsed = 0;
        chromatic.active = true;
        lensDistortion.active = true;
        colorAdjustments.active = true;

        while (elapsed < glitchDuration)
        {
            elapsed += Time.deltaTime;
            chromatic.intensity.Override(Random.Range(0, 1f));
            lensDistortion.intensity.Override(Random.Range(-0.5f, 0.5f));
            colorAdjustments.postExposure.Override(Random.Range(-1f, 1.5f));
            yield return null;
        }

        chromatic.active = false;
        lensDistortion.active = false;
        colorAdjustments.active = false;
        glitchVolume.weight = 0;

        eyeBlinkMaterial.SetFloat(EYE_OPEN, 0f);
        if (imageScene != null && sceneWhenOpenEye != null)
        {
            imageScene.sprite = sceneWhenOpenEye;
        }
        yield return new WaitForSeconds(0.5f);

        elapsed = 0;
        mainGameVolume.profile.TryGet(out DepthOfField dof);
        if (dof != null)
            dof.active = true;

        while (elapsed < wakeUpDuration)
        {
            elapsed += Time.deltaTime;
            float progress = eyeOpenCurves.Evaluate(elapsed / wakeUpDuration);
            eyeBlinkMaterial.SetFloat(EYE_OPEN, progress);

            if (dof != null)
                dof.focusDistance.Override(Mathf.Lerp(0.1f, 10f, progress));
            yield return null;
        }
        eyeBlinkMaterial.SetFloat(EYE_OPEN, 1f);

        if (dof != null)
            dof.active = false;

        InputStateManager.Instance.Enable();
    }
}
