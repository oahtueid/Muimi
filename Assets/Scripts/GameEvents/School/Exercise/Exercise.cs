using UnityEngine;
using UnityEngine.SceneManagement;

public class Exercise : School
{
    [SerializeField]
    ExamData examData;

    public Exercise(ExamData examData)
    {
        this.examData = examData;
    }

    public override bool IsConditionMet()
    {
        return base.IsConditionMet() && (examData != null);
    }

    public override void Execute()
    {
        SceneManager.LoadScene("Exercise");
    }
}
