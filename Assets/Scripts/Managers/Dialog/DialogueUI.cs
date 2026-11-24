using System.Collections.Generic;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    [Header("Dialogue UI")]
    public CanvasGroup root;
    public CanvasGroup speakerCG;
    public TextMeshProUGUI speaker;
    public Image speakerImg;
    public Button choiceBtnPrefab;
    public Transform choiceBtnContainer;
    public Image sceneImage;

    [SerializeField]
    SlidingText contentText;

    [SerializeField]
    GameObject continueHint;

    [Header("Narrator UI")]
    [SerializeField]
    Image narratorBackground;

    [SerializeField]
    SlidingText narratorText;
    SlidingText currentText;

    const string NONE = "none";

    public void OnEnable()
    {
        DialogueManager.Instance.OnEnterDialogue += Show;
        DialogueManager.Instance.OnExitDialogue += Hide;
        DialogueManager.Instance.OnAdvanceInput += HandleAdvanceInput;
        DialogueManager.Instance.OnProceedDialogue += ProceedDialogue;
        DialogueManager.Instance.OnChangePortrait += UpdatePortrait;
        DialogueManager.Instance.OnChangeSceneImage += UpdateSceneImage;
        contentText.OnFinishTyping += DisplayContinueHint;
        narratorText.OnFinishTyping += DisplayContinueHint;
    }

    public void OnDisable()
    {
        DialogueManager.Instance.OnEnterDialogue -= Show;
        DialogueManager.Instance.OnExitDialogue -= Hide;
        DialogueManager.Instance.OnAdvanceInput -= HandleAdvanceInput;
        DialogueManager.Instance.OnProceedDialogue -= ProceedDialogue;
        DialogueManager.Instance.OnChangePortrait -= UpdatePortrait;
        DialogueManager.Instance.OnChangeSceneImage -= UpdateSceneImage;
        contentText.OnFinishTyping -= DisplayContinueHint;
        narratorText.OnFinishTyping -= DisplayContinueHint;
    }

    void Start()
    {
        currentText = contentText;
    }

    private void Show()
    {
        root.gameObject.SetActive(true);
        sceneImage.gameObject.SetActive(true);
    }

    private void Hide()
    {
        root.gameObject.SetActive(false);
        sceneImage.gameObject.SetActive(false);
    }

    void HandleAdvanceInput()
    {
        if (currentText.isTyping)
        {
            currentText.SkipTypeText();
        }
        else
        {
            DialogueManager.Instance.Continue();
        }
    }

    void ProceedDialogue(DialogueData data)
    {
        continueHint.SetActive(false);

        if (data.isNarrator)
        {
            narratorBackground.gameObject.SetActive(true);
            speakerCG.gameObject.SetActive(false);
            Hide();
            currentText = narratorText;
        }
        else
        {
            narratorBackground.gameObject.SetActive(false);
            speakerCG.gameObject.SetActive(true);
            currentText = contentText;
            speaker.text = data.speakerName;
        }
        if (data.content != "")
        {
            root.alpha = 1;
            currentText.TypeText(data.content);
        }
        else
            root.alpha = 0;
        foreach (Transform choiceBtn in choiceBtnContainer.GetComponentInChildren<Transform>())
        {
            Destroy(choiceBtn.gameObject);
        }

        if (data.choices.Count > 0)
        {
            choiceBtnContainer.gameObject.SetActive(true);
            for (int i = 0; i < data.choices.Count; i++)
            {
                Choice choice = data.choices[i];
                Button button = CreateChoiceView(choice.text.Trim());
                button.onClick.AddListener(() =>
                {
                    DialogueManager.Instance.currentStory.ChooseChoiceIndex(choice.index);
                    DialogueManager.Instance.Continue();
                    InputStateManager.Instance.PopState();
                });
                if (i == 0)
                {
                    EventSystem.current.SetSelectedGameObject(button.gameObject);
                }
            }
        }
    }

    Button CreateChoiceView(string text)
    {
        Button choice = Instantiate(choiceBtnPrefab);
        choice.transform.SetParent(choiceBtnContainer, false);

        TextMeshProUGUI choiceText = choice.GetComponentInChildren<TextMeshProUGUI>();
        choiceText.text = text;

        // Make the button expand to fit the text
        HorizontalLayoutGroup layoutGroup = choice.GetComponent<HorizontalLayoutGroup>();
        layoutGroup.childForceExpandHeight = false;

        return choice;
    }

    void DisplayContinueHint()
    {
        continueHint.SetActive(true);
    }
    void UpdateSceneImage(string imageName)
    {
        Color sceneColor = sceneImage.color;
        if (imageName != NONE)
        {
            sceneColor.a = 1;
            sceneImage.sprite = Resources.Load<Sprite>($"Scenes/{imageName}");
        }
        else
        {
            sceneColor.a = 0;
        }
        sceneImage.color = sceneColor;
    }
    void UpdatePortrait(string portraitName)
    {
        if (portraitName == "none")
        {
            speakerImg.gameObject.SetActive(false);
            return;
        }
        speakerImg.gameObject.SetActive(true);
        speakerImg.sprite = Resources.Load<Sprite>($"Potraits/{portraitName}");
    }
}
