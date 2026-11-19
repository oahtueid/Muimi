using UnityEngine;

public class LocationTrigger : MonoBehaviour
{
    [SerializeField] LocationTarget location;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            EventBus.Publish(new LocationEnteredEvent(location.id));
        }
    }
}
