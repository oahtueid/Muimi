using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewMinigame", menuName = "Scriptable Objects/MinigameData")]
public class MinigameData : ScriptableObject
{
    public string gameName;
    public bool isFirstTimePlay = true;
    public List<Instruction> instructions;
    public float timeLimit;
    public GameReward reward;
    public int rankSThreshold = 1000;
    public int rankAThreshold = 800;
    public int rankBThreshold = 500;
}

[Serializable]
public class Instruction
{
    public string description;
    public Image guideImg;
}

[Serializable]
public class GameReward
{
    public GameReward(int amount, StatType rewardType)
    {
        this.amount = amount;
        this.rewardType = rewardType;
    }

    public int amount;
    public StatType rewardType;
    public bool isFirstTime = true;
}
