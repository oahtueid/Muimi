using System.Collections;
using UnityEngine;

public class LaneUI : MonoBehaviour
{
    [SerializeField]
    Color pressedColor;

    [SerializeField]
    Color originColor;
    SpriteRenderer spriteRenderer;
    float duration = 0.2f;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start(){
        spriteRenderer.color = originColor;
    }

    void OnEnable()
    {
        JudgementManager.Instance.OnLanePressed += PressLane;
    }

    void OnDisable()
    {
        JudgementManager.Instance.OnLanePressed -= PressLane;
    }

    void PressLane(int laneIndex)
    {
        if (laneIndex == transform.GetSiblingIndex())
        {
            StartCoroutine(PressLaneCoroutine());
        }
    }

    IEnumerator PressLaneCoroutine()
    {
        float elapsed = 0f;
        while (elapsed <= duration)
        {
            spriteRenderer.color = Color.Lerp(pressedColor, originColor, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}
