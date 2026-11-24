using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : PersistentManager<GameManager>
{
    [SerializeField]
    List<StoryRuleData> storyRules;
    Dictionary<string, List<StoryRuleData>> storyRuleDict;

    [SerializeField]
    string currentStory;

    [SerializeField]
    string currentSection;

    [SerializeField]
    List<ExamOnDay> examOnDaysList;
    Dictionary<int, ExamData> examOnDays;

    void Awake()
    {
        examOnDays = new();
        foreach (ExamOnDay examOnDay in examOnDaysList)
        {
            examOnDays[examOnDay.day] = examOnDay.examData;
        }
    }

    void OnEnable()
    {
        EventBus.Subcribe<LocationEnteredEvent>(OnLocationEntered);
        EventBus.Subcribe<ItemInteractedEvent>(OnItemInteracted);
        EventBus.Subcribe<MinigameFinishedEvent>(OnMinigameFinished);
        EventBus.Subcribe<CharacterInteractedEvent>(OnCharacterInteracted);
    }

    void OnDisable()
    {
        EventBus.UnSubcribe<LocationEnteredEvent>(OnLocationEntered);
        EventBus.UnSubcribe<ItemInteractedEvent>(OnItemInteracted);
        EventBus.UnSubcribe<MinigameFinishedEvent>(OnMinigameFinished);
        EventBus.UnSubcribe<CharacterInteractedEvent>(OnCharacterInteracted);
    }

    void Start()
    {
        storyRuleDict ??= new Dictionary<string, List<StoryRuleData>>();

        foreach (var rule in storyRules)
        {
            if (storyRuleDict.ContainsKey(rule.triggerTarget.id))
            {
                storyRuleDict[rule.triggerTarget.id].Add(rule);
            }
            else
            {
                storyRuleDict.Add(rule.triggerTarget.id, new List<StoryRuleData> { rule });
            }
        }
        if (InputStateManager.Instance.CurrentState == null)
            InputStateManager.Instance.SetState(new GameplayState(Player.Instance));
        // Comment to test rhythm minigame
        if (currentStory != "")
        {
            DialogueManager.Instance.LoadFile(currentStory, currentSection);
            DialogueManager.Instance.StartDialogue();
        }
    }

    void Update() { }

    public ExamData GetExamOnDay(int day)
    {
        Debug.Log("Exam on day: " + day);
        if (examOnDays.ContainsKey(day))
            return examOnDays[day];
        return null;
    }

    public void GoBack()
    {
        MenuManager.Instance.HandleOpenCloseMenu();
    }

    public void Save(int slot)
    {
        SaveData data = new SaveData();
        string storyJson = DialogueManager.Instance.Save();
        data.currentStoryJson = storyJson;
        data.saveTime = DateTime.Now;
        BinarySaveSystem.Save(data, slot);
    }

    public void Load(int slot)
    {
        Debug.Log("Load slot: " + slot);
        SaveData data = BinarySaveSystem.Load(slot);
        DialogueManager.Instance.Load(data.currentStoryJson);
    }

    void OnLocationEntered(LocationEnteredEvent e)
    {
        if (!storyRuleDict.ContainsKey(e.locationId))
            return;
        foreach (var rule in storyRuleDict[e.locationId])
        {
            if (rule.IsSatisfied())
            {
                rule.Execute();
            }
        }
    }

    void OnItemInteracted(ItemInteractedEvent e)
    {
        if (!storyRuleDict.ContainsKey(e.itemId))
            return;
        foreach (var rule in storyRuleDict[e.itemId])
        {
            if (rule.IsSatisfied())
            {
                rule.Execute();
            }
        }
    }

    void OnMinigameFinished(MinigameFinishedEvent e)
    {
        if (!storyRuleDict.ContainsKey(e.minigameId))
            return;
        foreach (var rule in storyRuleDict[e.minigameId])
        {
            if (rule.IsSatisfied())
            {
                rule.Execute();
            }
        }
    }
        void OnCharacterInteracted(CharacterInteractedEvent e)
    {
        if (!storyRuleDict.ContainsKey(e.characterId))
            return;
        foreach (var rule in storyRuleDict[e.characterId])
        {
            if (rule.IsSatisfied())
            {
                rule.Execute();
            }
        }
    }
}

[Serializable]
public class ExamOnDay
{
    public int day;
    public ExamData examData;
}
