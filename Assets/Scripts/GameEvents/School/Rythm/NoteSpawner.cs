using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    [SerializeField]
    SongMapData songMapData;

    [SerializeField]
    GameObject notePrefab;

    [SerializeField]
    Transform[] laneSpawnPoints;

    [SerializeField]
    float approachTime = 2.0f; // Note speed
    int currentNoteIndex = 0;

    void Start()
    {
        songMapData = Conductor.Instance.MapData;
    }

    void Update()
    {
        if (MinigameManager.Instance.CurrentState != GameState.Playing || currentNoteIndex >= songMapData.notes.Count)
            return;
        float currentSongPosition = Conductor.Instance.SongPosition;

        while (currentNoteIndex < songMapData.notes.Count &&
               songMapData.notes[currentNoteIndex].targetTime - approachTime <= currentSongPosition)
        {
            SpawnNote(songMapData.notes[currentNoteIndex]);
            currentNoteIndex++;
        }
    }

    void SpawnNote(NoteData noteData)
    {
        GameObject note = ObjectPool.Instance.Spawn(
            notePrefab,
            laneSpawnPoints[noteData.laneIndex].position,
            Quaternion.identity
        );
        NoteController controller = note.GetComponent<NoteController>();
        controller.assignedTime = noteData.targetTime;
        controller.noteSpeed = approachTime;
        controller.endY = laneSpawnPoints[noteData.laneIndex].position.y;
        controller.laneIndex = noteData.laneIndex;
        controller.noteIndex = currentNoteIndex;

        JudgementManager.Instance.laneQueues[noteData.laneIndex].Enqueue(controller);
    }
}
