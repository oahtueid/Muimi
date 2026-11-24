using UnityEngine;

public class Conductor : SceneManager<Conductor>
{
    [SerializeField]
    AudioSource currentSong;
    public SongMapData MapData;

    double dspSongStartTime;

    [SerializeField]
    float delayOffset;
    float songBpm;
    public float SongPosition { get; private set; }

    void Start()
    {
        InputStateManager.Instance.PushState(new RythmMinigameState());
        dspSongStartTime = AudioSettings.dspTime;
        currentSong.clip = MapData.song;
    }

    void OnEnable()
    {
        MinigameManager.Instance.OnGameStateChanged += PlaySong;
    }

    void OnDisable()
    {
        MinigameManager.Instance.OnGameStateChanged -= PlaySong;
    }

    void PlaySong(GameState state)
    {
        if (state == GameState.Playing)
        {
            currentSong.Play();
            dspSongStartTime = AudioSettings.dspTime;
        }
    }

    void Update()
    {
        SongPosition = (float)(AudioSettings.dspTime - dspSongStartTime - delayOffset);
    }
}
