using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Character : MonoBehaviour, IInteractable
{
    [SerializeField]
    CharacterData character;

    SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = character.image;
    }


    public void Interact()
    {
        EventBus.Publish(new CharacterInteractedEvent(character.id));
    }
}
