using UnityEngine;

[CreateAssetMenu(fileName = "PlayStoryAction", menuName = "Scriptable Objects/Actions/PlayStory")]
public class PlayStoryAction : GameAction
{
    [SerializeField]
    string knotName;

    [SerializeField]
    string fileName;

    public override void Execute()
    {
        DialogueManager.Instance.GoToPath(knotName, fileName);
    }
}
