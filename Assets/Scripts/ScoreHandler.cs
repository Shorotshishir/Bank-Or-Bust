using TMPro;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    [SerializeField] private ScoreScriptable scoreScriptable;
    [SerializeField] private MainUi mainUi;

    private void OnEnable()
    {
        GameSystem.Result += OnDiceResult;
        GameSystem.Round += OnUpdateRound;
    }

    private void OnUpdateRound(int round)
    {
       //  roundCount.SetText($"Round\n{round}");
        scoreScriptable.RoundCount = round;
        scoreScriptable.RoundUpdate();
        if (round == 10)
        {
            // stop game
            scoreScriptable.GameOver();
            // show results
        }
    }

    private void OnDiceResult(string playerName, int result)
    {
        if (playerName.Equals("Player1"))
        {
            if (result == 1)
            {
                scoreScriptable.Player1Score = 0;
                //Debug.Log($"Bust player 1 !!! {scoreScriptable.Player1Score}");
            }
            else
            {
                scoreScriptable.Player1Score += result;
            }
        }
        else
        {
            if (result == 1)
            {
                scoreScriptable.Player2Score = 0;
                //Debug.Log($"Bust Player 2!!!{scoreScriptable.Player2Score}");
            }
            else
            {
                scoreScriptable.Player2Score += result;
            }
        }
        scoreScriptable.ScoreUpdate();
    }

    private void OnDisable()
    {
        GameSystem.Result -= OnDiceResult;
    }

    private void OnDestroy()
    {
        scoreScriptable.Reset();
    }
}