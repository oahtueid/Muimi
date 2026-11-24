using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MinigameUIController : MonoBehaviour
{
    [Header("Game Intro UI")]
    [SerializeField]
    TextMeshProUGUI countdownText;

    [Header("In-Game HUD")]
    [SerializeField]
    TextMeshProUGUI timeLeftText;

    [SerializeField]
    TextMeshProUGUI currentScoreText;

    [Header("Game Result UI")]
    [SerializeField]
    GameObject resultPanel;

    [SerializeField]
    CanvasGroup resultCanvasGroup;

    [SerializeField]
    TextMeshProUGUI finalScoreText;

    [SerializeField]
    TextMeshProUGUI rankText;

    [SerializeField]
    TextMeshProUGUI rewardText;

    [SerializeField]
    TextMeshProUGUI goBackGuideText;

    [SerializeField]
    float resultPanelFadeInDuration = 1.0f;

    [SerializeField]
    Button pauseBtn;

    [SerializeField]
    Button resumeBtn;
    [SerializeField]
    InstructionUI instructionUI;

    void Start()
    {
        resultPanel.SetActive(false);
        goBackGuideText.gameObject.SetActive(false);
    }

    void OnEnable()
    {
        MinigameManager.Instance.OnTimeUpdated += DisplayTime;
        MinigameManager.Instance.OnScoreUpdated += DisplayScore;
        MinigameManager.Instance.OnGameStateChanged += ShowMinigameIntro;
        MinigameManager.Instance.OnGameCompleted += DisplayResult;
        if (pauseBtn)
            pauseBtn.onClick.AddListener(MinigameManager.Instance.Pause);
        if (resumeBtn)
            resumeBtn.onClick.AddListener(MinigameManager.Instance.Resume);
    }

    void OnDisable()
    {
        MinigameManager.Instance.OnTimeUpdated -= DisplayTime;
        MinigameManager.Instance.OnScoreUpdated -= DisplayScore;
        MinigameManager.Instance.OnGameStateChanged -= ShowMinigameIntro;
        MinigameManager.Instance.OnGameCompleted -= DisplayResult;
        if (pauseBtn)
            pauseBtn.onClick.RemoveListener(MinigameManager.Instance.Pause);
        if (resumeBtn)
            resumeBtn.onClick.RemoveListener(MinigameManager.Instance.Resume);
    }

    void DisplayTime(float time)
    {
        if (timeLeftText == null)
            return;
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        string currentTimeLeft = string.Format("{0:00}:{1:00}", minutes, seconds);
        timeLeftText.text = currentTimeLeft;
    }

    void DisplayScore(int score)
    {
        if (currentScoreText == null)
            return;
        currentScoreText.text = score.ToString();
    }

    void DisplayResult(GameResult result)
    {
        if(currentScoreText != null) currentScoreText.gameObject.SetActive(false);
        if(timeLeftText != null) timeLeftText.gameObject.SetActive(false);
        if (resultPanel == null)
            return;
        resultPanel.SetActive(true);
        resultCanvasGroup.alpha = 0;
        if (finalScoreText)
            finalScoreText.text = result.score.ToString();
        if (rankText)
            rankText.text = result.rank.ToString();
        if (rewardText)
            rewardText.text = result.reward.amount.ToString();
        StartCoroutine(DisplayResultCoroutine());
    }

    IEnumerator DisplayResultCoroutine()
    {
        yield return new WaitForSeconds(1);
        float elapsed = 0;
        while (elapsed < resultPanelFadeInDuration)
        {
            elapsed += Time.deltaTime;
            resultCanvasGroup.alpha = Mathf.Lerp(0, 1, elapsed / resultPanelFadeInDuration);
            yield return null;
        }
        goBackGuideText.gameObject.SetActive(true);
        goBackGuideText.text = "Press E To Continue";
        MinigameManager.Instance.IsResultShown = true;
    }

    void ShowMinigameIntro(GameState state)
    {
        if (state == GameState.Ready)
        {
            if (
                MinigameManager.Instance.gameData.isFirstTimePlay
                && MinigameManager.Instance.gameData.instructions.Count > 0
            )
            {
                ShowInstruction();
            }
            else
                StartCoroutine(GameIntroCoroutine());
        }
    }

    IEnumerator GameIntroCoroutine()
    {
        countdownText.gameObject.SetActive(true);
        for (int i = 3; i >= 1; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSeconds(1);
        }
        countdownText.gameObject.SetActive(false);
        MinigameManager.Instance.SetState(GameState.Playing);
    }

    void ShowInstruction()
    {
        instructionUI.instructions = MinigameManager.Instance.gameData.instructions;
        instructionUI.Open();
        instructionUI.OnClosed += () => StartCoroutine(GameIntroCoroutine());
    }
    public void ShowNextInstruction()
    {
        instructionUI.NextPage();
    }
    public void ShowPreviousInstruction()
    {
        instructionUI.PreviousPage();
    }
}
