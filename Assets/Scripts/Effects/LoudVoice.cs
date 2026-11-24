using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LoudVoice : Effect
{
    public TextMeshProUGUI text;
    public Image sceneImage;

    public override void StartEffect(Action onComplete = null)
    {
        text.TryGetComponent(out Shake textShake);
        if (textShake != null)
            textShake.StartEffect();
        Camera.main.TryGetComponent(out Shake cameraShake);
        if (cameraShake != null)
            cameraShake.StartEffect();
        sceneImage.TryGetComponent(out Shake imageShake);
        if (imageShake != null)
            imageShake.StartEffect();
    }

    public override void StopEffect() { }
}
