using System.Collections;
using TMPro;
using UnityEngine;

public class Objective : MonoBehaviour
{
    public ObjectiveData Data { get; private set; }

    [SerializeField]
    TextMeshProUGUI description;

    public void Display(ObjectiveData data)
    {
        Data = data;
        description.text = data.description;
    }

    public void Complete()
    {
        Debug.Log("complete obj");
        description.text = "";
        description.color = Color.yellow;
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        float duration = 2f;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            description.alpha = Mathf.Lerp(description.alpha, 0, elapsed);
            yield return null;
        }
        gameObject.SetActive(false);
    }
}
