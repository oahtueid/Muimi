using System;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]

public class NoteController : MonoBehaviour
{
    public float assignedTime;
    float spawnY = 8.0f;
    public float endY = -3.2f;

    public float noteSpeed = 2.0f;
    public int laneIndex;
    public int noteIndex;

    void Start()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, spawnY, 0f);
    }

    void Update()
    {
        float songPos = Conductor.Instance.SongPosition;
        float progress = 1.0f - ((assignedTime - songPos) / noteSpeed);
        float currentY = Mathf.LerpUnclamped(spawnY, endY, progress);
        transform.localPosition = new Vector3(transform.localPosition.x, currentY, 0f);

        if (songPos - assignedTime > 0.15f)
        {
            JudgementManager.Instance.RemoveMissedNote(laneIndex, this);
            Despawn();
        }
    }

    public void Despawn()
    {
        ObjectPool.Instance.Despawn(gameObject, "Note");
        if(noteIndex == Conductor.Instance.MapData.notes.Count - 1)
        {
            MinigameManager.Instance.CompleteGame();
        }
    }
}
