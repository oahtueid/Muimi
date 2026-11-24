using System;
using System.Collections;
using UnityEngine;

public class Shake : Effect
{
    public float magnitude = 2f;

    public override void StartEffect(Action onComplete = null)
    {
        StartCoroutine(ShakeCoroutine());
    }

    public override void StopEffect() { }

    IEnumerator ShakeCoroutine()
    {
        Vector3 originalPos = transform.localPosition;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float offsetX = UnityEngine.Random.Range(-1f, 1f) * magnitude;
            float offsetY = UnityEngine.Random.Range(-1f, 1f) * magnitude;
            transform.localPosition = originalPos + new Vector3(offsetX, offsetY, 0);

            yield return null;
        }
        transform.localPosition = originalPos;
    }
}
