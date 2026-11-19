using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "NewExam", menuName = "Scriptable Objects/ExamData", order = 1)]
public class ExamData : ScriptableObject
{
    public List<Question> questions;
    public float timeLimit = 50f;
}

[Serializable]
public class Question
{
    public string content;
    public List<Answer> answers;
}

[Serializable]
public class Answer
{
    public string content;
    public bool isCorrect;
    public int score;
}
