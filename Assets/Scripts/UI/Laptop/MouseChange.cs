using UnityEngine;
using UnityEngine.EventSystems;

public class MouseChange : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] MouseMode modeChangeTo;

    public void OnPointerEnter(PointerEventData eventData)
    {
        MouseController.Instance.SetToMode(modeChangeTo);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        MouseController.Instance.SetToMode(MouseMode.Default);
    }
}
