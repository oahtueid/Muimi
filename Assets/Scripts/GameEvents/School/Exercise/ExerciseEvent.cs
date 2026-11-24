using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExerciseEvent : MonoBehaviour
{
    [SerializeField]
    ExamData examData;
    float time;

    [SerializeField]
    Slider timeSlider;

    [SerializeField]
    TextMeshProUGUI timeLeftText;

    [SerializeField]
    TextMeshProUGUI questionText;

    [SerializeField]
    Transform answersContainer;
    int totalScore = 0;
    int currentQuestionIdx = 0;
    bool isFinish = false;

    void Awake()
    {
        examData = GameManager.Instance.GetExamOnDay(TimeManager.Instance.CurrentDay);
        time = examData.timeLimit;
        timeSlider.maxValue = examData.timeLimit;
        timeSlider.minValue = 0;
    }

    void Start()
    {
        DisplayQuestion(examData.questions[currentQuestionIdx]);
    }

    void Update()
    {
        if (!isFinish)
        {
            if (time > 0)
            {
                time -= Time.deltaTime;
                DisplayTime(time);
            }
            else
                DisplayTime(0);
        }
    }

    void DisplayQuestion(Question ques)
    {
        ResetAnswer();
        questionText.text = ques.content;
        DisplayAnswer(ques);
    }

    void DisplayAnswer(Question ques)
    {
        var rand = new System.Random();
        var shuffled = ques.answers.OrderBy(x => rand.Next()).ToList();
        int idx = 0;
        foreach (Transform child in answersContainer)
        {
            if (idx >= shuffled.Count)
                break;
            child.gameObject.SetActive(true);
            Button answerBtn = child.GetComponent<Button>();
            TextMeshProUGUI answerContent = answerBtn.GetComponentInChildren<TextMeshProUGUI>();
            answerContent.text = shuffled[idx].content;
            int score = shuffled[idx].score;
            if (shuffled[idx].isCorrect == true)
            {
                answerBtn.onClick.AddListener(() =>
                {
                    PlusScore(score);
                });
            }
            int nextQuestionIdx = currentQuestionIdx + 1;
            answerBtn.onClick.AddListener(() =>
            {
                if (nextQuestionIdx < examData.questions.Count)
                    DisplayQuestion(examData.questions[nextQuestionIdx]);
                else
                    Finish();
            });
            idx++;
        }
        currentQuestionIdx++;
    }

    void ResetAnswer()
    {
        foreach (Transform child in answersContainer)
        {
            child.GetComponent<Button>().onClick.RemoveAllListeners();
            child.gameObject.SetActive(false);
        }
    }

    void PlusScore(int score)
    {
        totalScore += score;
    }

    void DisplayTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        string currentTimeLeft = string.Format("{0:00}:{1:00}", minutes, seconds);
        timeLeftText.text = currentTimeLeft;
        timeSlider.value = time;
    }

    void Finish()
    {
        isFinish = true;
        questionText.text = "Score: " + totalScore;
        ResetAnswer();
        timeSlider.gameObject.SetActive(false);
        timeLeftText.text = "";
        PlayerStat.Instance.UpdateStatus(StatType.Intelligence, totalScore);
        EventManager.Instance.RunNextEvent(Location.School);
    }
}
