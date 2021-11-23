using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Score", menuName = "ScriptableObjects/Score", order = 1)]
public class ScoreScriptable : ScriptableObject
{
    public int Player1Score;
    public int Player2Score;

    public void Reset()
    {
        Player1Score = 0;
        Player2Score = 0;
    }
}