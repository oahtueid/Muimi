using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActionPanel : MonoBehaviour
{
    public GameObject buttonPrefab;
    public Transform buttonContainer;

    public void Show(List<(string, Action)> actions)
    {
        foreach (Transform child in buttonContainer)
            Destroy(child.gameObject);

        for (int i = 0; i < actions.Count; i++)
        {
            var (name, action) = actions[i];

            var btnObj = Instantiate(buttonPrefab, buttonContainer);
            var btn = btnObj.GetComponent<Button>();
            var txt = btnObj.GetComponentInChildren<TextMeshProUGUI>();
            txt.text = name;

            btn.onClick.AddListener(() =>
            {
                action();
                UIManager.Instance.HideAction();
                InputStateManager.Instance.PopState();
            });
            if (i == 0)
            {
                EventSystem.current.SetSelectedGameObject(btn.gameObject);
            }
        }
    }
}
