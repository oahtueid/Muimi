using Unity.VisualScripting;
using UnityEngine;

public class Entrance : MonoBehaviour
{
    public Transform destination;
    public Collider2D confiner;

    void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        if (player)
        {
                    MoveToDestination(player);

            // EffectManager.Instance.Play(
            //     "fade",
            //     () =>
            //     {
            //         MoveToDestination(player);
            //     }
            // );
        }
    }

    void MoveToDestination(Player player)
    {
        player.transform.position = destination.position;
        CameraManager.Instance.ChangeScene(destination.position, confiner);
    }
}

public enum PlaceType
{
    Scene,
    Area,
}
