using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(
    fileName = "NewCharacterData",
    menuName = "Scriptable Objects/CharacterData",
    order = 1
)]
public class CharacterData : CharacterTarget
{
    public GameObject characterPrefab;
    public Sprite image;
    public string characterName;
    public string description;
    public string vnDescription;
}
