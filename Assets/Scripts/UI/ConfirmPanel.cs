using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ConfirmPanel : MenuUI
{
    [SerializeField]
    TextMeshProUGUI confirmText;

    [SerializeField]
    Button confirmBtn;

    public void Display(string content, UnityAction action)
    {
        confirmText.text = content;
        confirmBtn.onClick.AddListener(action);
        EventSystem.current.SetSelectedGameObject(confirmBtn.gameObject);
    }
}
