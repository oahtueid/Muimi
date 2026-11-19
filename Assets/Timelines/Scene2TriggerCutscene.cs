using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(BoxCollider2D))]
public class Scene2TriggerCutscene : MonoBehaviour
{
    [SerializeField]
    PlayableDirector playableDirector;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playableDirector.Play();
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    public void TriggerScene2()
    {
        DialogueManager.Instance.GoToPath("Day1.Scene2");
    }
}
