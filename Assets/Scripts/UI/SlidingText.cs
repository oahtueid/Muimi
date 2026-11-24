using System;
using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class SlidingText : MonoBehaviour
{
    TextMeshProUGUI textUI;
    string currentText;
    public bool isTyping { get; private set; } = false;
    public Action OnFinishTyping;

    [SerializeField]
    float delay = 0.02f;
    Coroutine typeTextCoroutine;

    void Awake()
    {
        textUI = GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        textUI.text = "";
    }

    private IEnumerator TypeTextCoroutine(string text)
    {
        currentText = text;
        textUI.text = "";
        isTyping = true;
        Debug.Log("Sliding text typing: " + text);
        foreach (char c in text)
        {
            yield return new WaitForSeconds(delay);
            textUI.text += c;
        }
        isTyping = false;
        OnFinishTyping?.Invoke();
    }

    public void TypeText(string text)
    {
       typeTextCoroutine = StartCoroutine(TypeTextCoroutine(text));
    }

    public void SkipTypeText()
    {
        StopCoroutine(typeTextCoroutine);
        textUI.text = currentText;
        currentText = "";
        isTyping = false;
        OnFinishTyping?.Invoke();
    }
}
