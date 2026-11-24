using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Ready,
    Playing,
    Pause,
    Completed,
}

public enum ResultRank
{
    S,
    A,
    B,
    C,
}

[Serializable]
public class GameResult
{
    public GameResult(int score, ResultRank rank, GameReward reward)
    {
        this.score = score;
        this.rank = rank;
        this.reward = reward;
    }

    public int score;
    public ResultRank rank;
    public GameReward reward;
}

public class MinigameManager : SceneManager<MinigameManager>
{
    public MinigameData gameData;

    public Action<int> OnScoreUpdated;
    public Action<float> OnTimeUpdated;
    public Action<GameState> OnGameStateChanged;
    public Action<GameResult> OnGameCompleted;

    int currentScore;
    float currentTimeLeft;
    int currentCombo;

    public GameState CurrentState { get; private set; }
    public bool IsResultShown = false;
    GameResult result;

    [Header("Reward Config")]
    [SerializeField]
    float rankSMultiplier = 2.0f;

    [SerializeField]
    float rankAMultiplier = 1.5f;

    [SerializeField]
    float rankBMultiplier = 1.2f;

    public void InitializeGame()
    {
        currentScore = 0;
        currentTimeLeft = gameData.timeLimit;
        currentCombo = 0;
        SetState(GameState.Ready);
    }

    void Start()
    {
        InitializeGame();
    }

    void Update()
    {
        if (CurrentState == GameState.Playing)
        {
            currentTimeLeft = Mathf.Max(currentTimeLeft - Time.deltaTime, 0);
            OnTimeUpdated?.Invoke(currentTimeLeft);
        }
    }

    public void CompleteGame()
    {
        CurrentState = GameState.Completed;
        int rewardAmount = gameData.reward.amount;
        ResultRank rank;
        if (currentScore >= gameData.rankSThreshold)
        {
            rank = ResultRank.S;
            rewardAmount = (int)(gameData.reward.amount * rankSMultiplier);
        }
        else if (currentScore >= gameData.rankAThreshold)
        {
            rank = ResultRank.A;
            rewardAmount = (int)(gameData.reward.amount * rankAMultiplier);
        }
        else if (currentScore >= gameData.rankBThreshold)
        {
            rank = ResultRank.B;
            rewardAmount = (int)(gameData.reward.amount * rankBMultiplier);
        }
        else
            rank = ResultRank.C;
        GameReward reward = new(rewardAmount, gameData.reward.rewardType);
        result = new(currentScore, rank, reward);
        OnGameCompleted?.Invoke(result);
    }

    public void AddScore(int amount, bool isCombo)
    {
        if (isCombo)
        {
            currentCombo++;
            float multiplier = 1 + currentCombo / 100;
            currentScore += Mathf.RoundToInt(amount * multiplier);
        }
        else
        {
            currentScore += amount;
        }
        OnScoreUpdated?.Invoke(currentScore);
    }

    public void ResetCombo()
    {
        currentCombo = 0;
    }

    public void Pause()
    {
        CurrentState = GameState.Pause;
        Time.timeScale = 0;
    }

    public void Resume()
    {
        CurrentState = GameState.Playing;
        Time.timeScale = 1;
    }

    public void OnSubmit()
    {
        if (CurrentState != GameState.Completed || !IsResultShown)
            return;
        SceneManager.LoadScene("Home");
        InputStateManager.Instance.PopState();
        EventBus.Publish(new MinigameFinishedEvent(gameData.gameName));
    }

    public void SetState(GameState state)
    {
        CurrentState = state;
        OnGameStateChanged?.Invoke(CurrentState);
    }
}
