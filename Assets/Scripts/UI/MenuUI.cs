using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CanvasGroup))]
public class MenuUI : MonoBehaviour
{
    [SerializeField]
    GameObject defaultSelected;
    public GameObject LastSelected { get; set; }

    public CanvasGroup canvasGroup;
    public UIType type = UIType.Screen;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Open()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        GameObject target = LastSelected ? LastSelected : defaultSelected;
        EventSystem.current.SetSelectedGameObject(target);
    }

    public void Close()
    {
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0;
    }
}

public enum UIType
{
    Screen,
    Popup,
}
