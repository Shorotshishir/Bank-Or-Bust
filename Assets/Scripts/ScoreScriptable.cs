using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Score", menuName = "ScriptableObjects/Score", order = 1)]
public class ScoreScriptable : ScriptableObject
{
    public int Player1Score;
    public int Player2Score;
    public int RoundCount;

    public event Action OnRoundUpdate;
    public event Action OnScoreUpdate;
    public event Action OnGameOver;

    public void RoundUpdate()
    {
        OnRoundUpdate?.Invoke();
    }

    public void ScoreUpdate()
    {
        OnScoreUpdate?.Invoke();
    }

    public void GameOver()
    {
        OnGameOver?.Invoke();
    }

    public string GetStatus()
    {
        if (Player1Score == Player2Score) return "Draw!";
        return Player1Score > Player2Score ? "Winner : Player 1" : "Winner : Player 2";
    }

    public void ResetData()
    {
        Player1Score = 0;
        Player2Score = 0;
        RoundCount = 0;
    }


}