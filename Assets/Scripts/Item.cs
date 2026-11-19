using System.Collections;
using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    [SerializeField] ItemTarget item;
    public virtual void Interact()
    {
        StartCoroutine(SwitchToChoiceStateNextFrame());
        EventBus.Publish(new ItemInteractedEvent(item.id));
        Debug.Log("Interact " + gameObject.name);
    }

    private IEnumerator SwitchToChoiceStateNextFrame()
    {
        yield return null;
        InputStateManager.Instance.PushState(new ActionChoiceState());
    }
}
