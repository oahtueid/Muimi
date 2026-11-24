using TMPro;
using UnityEngine;

public class TimeTextUI : MonoBehaviour
{
    TextMeshProUGUI timeText;

    void Awake()
    {
        timeText = GetComponent<TextMeshProUGUI>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeText.text = $"Day {TimeManager.Instance.CurrentDay} - {TimeManager.Instance.CurrentSession} - {TimeManager.Instance.today.DayOfWeek}";
        TimeManager.Instance.OnUpdateTime += UpdateTimeText;
    }

    void OnDestroy()
    {
        TimeManager.Instance.OnUpdateTime -= UpdateTimeText;
    }


    void UpdateTimeText(int currentDay, Session currentSession)
    {
        timeText.text = $"Day {currentDay} - {currentSession}"; 
    }
}
