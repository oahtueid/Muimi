using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Character : MonoBehaviour, IInteractable
{
    [SerializeField]
    CharacterData data;
    SpriteRenderer spriteRenderer;
    int currentConversationIndex = 1;
    const string CONVERSATION_FILENAME = "Conversation";

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = data.image;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() { }

    // Update is called once per frame
    void Update() { }

    public void Interact()
    {
        StartCoroutine(LoadConversation());
    }

    public void UnlockNewConversation()
    {
        if (CanUnlock())
            currentConversationIndex++;
    }

    bool CanUnlock()
    {
        return true;
    }

    private IEnumerator LoadConversation()
    {
        yield return null;
        DialogueManager.Instance.Load(CONVERSATION_FILENAME);
        DialogueManager.Instance.GoToPath($"{data.characterName}_{currentConversationIndex}");
    }
}
