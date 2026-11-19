using System;
using System.Collections.Generic;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public struct DialogueData
{
    public string speakerName;
    public string content;
    public string speakerPortraitPath;
    public bool isNarrator;
    public List<Choice> choices;
}

public class DialogueManager : PersistentManager<DialogueManager>
{
    public TextAsset inkJson;
    public Story currentStory { get; private set; }
    public string CurrentKnotName { get; private set; }
    public Action OnEnterDialogue;
    public Action OnExitDialogue;
    public Action OnFinishDialogue;
    public Action OnAdvanceInput;
    public Action<DialogueData> OnProceedDialogue;
    public Action<string> OnChangeSceneImage;
    public Action<string> OnChangePortrait;
    const string NONE = "none";
    const string NARRATOR = "narrator";
    const string POTRAIT_TAG = "potrait";
    const string SPEAKER_TAG = "speaker";
    const string EFFECT_TAG = "effect";
    const string IMAGE_TAG = "image";
    const string SOUND_TAG = "sound";
    AudioSource audioSource;

    [SerializeField]
    Image currentScene;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void LoadFile(string inkFileName, string section = null)
    {
        inkJson = Resources.Load<TextAsset>($"Dialogue/{inkFileName}");
        currentStory = new Story(inkJson.text);

        if (section != null && section != "")
            currentStory.ChoosePathString(section);
        BindFunctions();
    }

    public void StartDialogue()
    {
        UIManager.Instance.Show("ScriptUI");
        InputStateManager.Instance.PushState(new DialogueState(this));
        OnEnterDialogue?.Invoke();
        Continue();
    }
    public void RequestAdvance()
    {
        OnAdvanceInput?.Invoke();
    }

    public void Continue()
    {
        List<Choice> choices = currentStory.currentChoices;
        if (currentStory.canContinue)
        {
            string text = currentStory.Continue();
            choices = currentStory.currentChoices;

            Debug.Log("Current: " + text);
            DialogueData data = BuildDialogueData(text, choices, currentStory.currentTags);
            OnProceedDialogue?.Invoke(data);
            if (choices.Count > 0)
            {
                InputStateManager.Instance.PushState(new StoryChoiceState());
            }
        }
        else if (choices.Count == 0)
        {
            OnExitDialogue?.Invoke();
            // OnFinishDialogue?.Invoke();
            EventBus.Publish(new DialogueFinishedEvent(CurrentKnotName));
            InputStateManager.Instance.PopState();
        }
    }

    DialogueData BuildDialogueData(string content, List<Choice> choices, List<string> currentTags)
    {
        DialogueData data = new DialogueData
        {
            speakerName = "",
            speakerPortraitPath = "",
            content = content,
            isNarrator = false,
            choices = choices,
        };
        OnEnterDialogue?.Invoke();

        foreach (string tag in currentTags)
        {
            Debug.Log("Tag: " + tag);
            string[] splitTag = tag.Split(":");
            if (splitTag.Length != 2)
            {
                Debug.LogError("Tag could not be appropriate parsed: " + tag);
            }
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();
            switch (tagKey)
            {
                case POTRAIT_TAG:
                    OnChangePortrait?.Invoke(tagValue);
                    break;
                case SPEAKER_TAG:
                    if (tagValue == NARRATOR)
                    {
                        data.isNarrator = true;
                    }
                    else
                    {
                        data.speakerName = tagValue;
                        data.isNarrator = false;
                    }
                    break;
                case EFFECT_TAG:
                    if (tagValue != NONE)
                    {
                        EffectManager.Instance.Play(tagValue);
                    }
                    else
                        EffectManager.Instance.StopAll();
                    break;
                case IMAGE_TAG:
                    OnChangeSceneImage?.Invoke(tagValue);
                    break;
                case SOUND_TAG:
                    AudioClip clip = Resources.Load<AudioClip>($"Sounds/{tagValue}");
                    audioSource.PlayOneShot(clip);
                    break;
            }
        }
        return data;
    }

    public void GoToPath(string pathName, string fileName = null, Action onFinish = null)
    {
        if (fileName != null)
        {
            LoadFile(fileName);
        }
        if (currentStory == null)
        {
            LoadFile("main");
        }
        currentStory.ChoosePathString(pathName);
        StartDialogue();
        OnFinishDialogue = onFinish;
    }

    void BindFunctions()
    {
        currentStory.BindExternalFunction(
            "AddObjective",
            (string id) =>
            {
                ObjectiveManager.Instance.AddObjective(id);
            }
        );
        currentStory.BindExternalFunction(
            "CompleteObjective",
            (string id) =>
            {
                ObjectiveManager.Instance.CompleteObjective(id);
            }
        );
        currentStory.BindExternalFunction(
            "StartMinigame",
            (string name) =>
            {
                if (name == "Rhythm")
                {
                    SceneManager.LoadScene("Rhythm");
                }
            }
        );
    }

    public string Save()
    {
        if (!currentStory)
        {
            Debug.LogError("No Story to save!");
            return "";
        }
        return currentStory.state.ToJson();
    }

    public void Load(string json)
    {
        if (json != "")
            currentStory.state.LoadJson(json);
    }
}
