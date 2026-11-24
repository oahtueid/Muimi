using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NoteData
{
    public float targetTime;

    [Range(0, 3)]
    public int laneIndex;
    public float length;
}

[CreateAssetMenu(fileName = "NewSongMap", menuName = "Scriptable Objects/SongMapData")]
public class SongMapData : ScriptableObject
{
    public string songName;
    public AudioClip song;
    public float offset;
    public int bpm;
    public List<NoteData> notes;
}
