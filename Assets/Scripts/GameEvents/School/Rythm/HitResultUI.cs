using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class HitResultUI : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI hitResultText;

    [Header("Result Color")]
    [SerializeField]
    Color pefect;

    [SerializeField]
    Color great;

    [SerializeField]
    Color good;

    [SerializeField]
    Color bad;

    [SerializeField]
    Color miss;
    float duration = 0.7f;

    void Awake()
    {
        hitResultText = GetComponent<TextMeshProUGUI>();
    }

    void OnEnable()
    {
        JudgementManager.Instance.OnNoteHit += DisplayResult;
    }

    void OnDisable()
    {
        JudgementManager.Instance.OnNoteHit -= DisplayResult;
    }

    IEnumerator currentCouroutine;

    public void DisplayResult(HitResult result)
    {
        hitResultText.text = result.ToString().ToUpper();
        switch (result)
        {
            case HitResult.Perfect:
                hitResultText.color = pefect;
                break;
            case HitResult.Great:
                hitResultText.color = great;
                break;
            case HitResult.Good:
                hitResultText.color = good;
                break;
            case HitResult.Bad:
                hitResultText.color = bad;
                break;
            case HitResult.Miss:
                hitResultText.color = miss;
                break;
        }
        if (currentCouroutine != null)
        {
            StopCoroutine(currentCouroutine);
        }
        currentCouroutine = FadeResultCoroutine();
        StartCoroutine(FadeResultCoroutine());
    }

    IEnumerator FadeResultCoroutine()
    {
        hitResultText.alpha = 1;
        float elapsed = 0;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            hitResultText.alpha = Mathf.Lerp(1, 0, elapsed / duration);
            yield return null;
        }
        currentCouroutine = null;
    }
}
