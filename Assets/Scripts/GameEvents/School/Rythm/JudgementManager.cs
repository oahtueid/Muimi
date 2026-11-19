using System;
using System.Collections.Generic;
using UnityEngine;

public enum HitResult
{
    Miss,
    Perfect,
    Great,
    Good,
    Bad,
}

public class JudgementManager : SceneManager<JudgementManager>
{
    public List<Queue<NoteController>> laneQueues = new();

    const float PERFECT_WINDOW = 0.035f;
    const float GREAT_WINDOW = 0.08f;
    const float GOOD_WINDOW = 0.14f;
    const float BAD_WINDOW = 0.2f;
    public Action<HitResult> OnNoteHit;
    public Action<int> OnLanePressed;

    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            laneQueues.Add(new());
        }
    }

    public void CheckHit(int laneIndex)
    {
        OnLanePressed?.Invoke(laneIndex);
        if (laneQueues[laneIndex].Count == 0)
            return;
        NoteController note = laneQueues[laneIndex].Peek();
        float timeDiff = Mathf.Abs(Conductor.Instance.SongPosition - note.assignedTime);
        if (timeDiff <= BAD_WINDOW)
        {
            laneQueues[laneIndex].Dequeue();
            if (timeDiff <= PERFECT_WINDOW)
                RegisterHit(HitResult.Perfect);
            else if (timeDiff <= GREAT_WINDOW)
                RegisterHit(HitResult.Great);
            else if (timeDiff <= GOOD_WINDOW)
                RegisterHit(HitResult.Good);
            else
                RegisterHit(HitResult.Bad);
            note.Despawn();
        }
    }

    public void RegisterHit(HitResult result)
    {
        OnNoteHit?.Invoke(result);
        int score = 0;
        if (result == HitResult.Perfect)
            score = 500;
        else if (result == HitResult.Great)
            score = 300;
        else if (result == HitResult.Good)
            score = 200;
        else if (result == HitResult.Bad)
            score = 100;
        else
            MinigameManager.Instance.ResetCombo();
        MinigameManager.Instance.AddScore(score, score != 0);
    }

    public void RemoveMissedNote(int laneIndex, NoteController note)
    {
        var queue = laneQueues[laneIndex];
        if (queue.Count > 0 && queue.Peek() == note)
        {
            queue.Dequeue();
            RegisterHit(HitResult.Miss);
        }
    }
}
