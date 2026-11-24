using UnityEngine;

public class PlayerStat : PersistentManager<PlayerStat>
{
    public CharacterData characterData;
    public int strength = 10;
    public int intelligence = 10;
    public int sanity = 100;
    public int courage = 100;
    public int harmony = 100;
    public bool takeDailyPill = false;

    public void Start()
    {
        if (characterData != null)
        {
            Debug.Log("Player Character: " + characterData.characterName);
        }
        else
        {
            Debug.LogWarning("No character data assigned to the player.");
        }
    }

    public void UpdateStatus(StatType status, int value)
    {
        switch (status)
        {
            case StatType.Strength:
                strength += value;
                Debug.Log("Player Strength: " + strength);
                break;
            case StatType.Intelligence:
                intelligence += value;
                Debug.Log("Player Intelligence: " + intelligence);
                break;
            case StatType.Sanity:
                sanity += value;
                Debug.Log("Player Sanity: " + sanity);
                break;
            case StatType.Courage:
                courage += value;
                Debug.Log("Player Courage: " + courage);
                break;
            case StatType.Harmony:
                harmony += value;
                Debug.Log("Player Harmony: " + harmony);
                break;
            default:
                Debug.LogWarning("Unknown status: " + status);
                break;
        }
    }
}

public enum StatType
{
    Strength,
    Intelligence,
    Sanity,
    Courage,
    Harmony,
}
