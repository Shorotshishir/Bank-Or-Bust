using TMPro;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI roundCount;
    [SerializeField] private TextMeshProUGUI player1;
    [SerializeField] private TextMeshProUGUI player2;
    [SerializeField] private ScoreScriptable scoreScriptable;

    private void OnEnable()
    {
        GameSystem.Result += OnDiceResult;
        GameSystem.Round += OnUpdateRound;
    }

    private void OnUpdateRound(int round)
    {
        roundCount.SetText($"Round\n{round}");
    }

    private void OnDiceResult(GameObject player, int result)
    {
        if (player.name.Equals("Player1"))
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
            player1.SetText($"{scoreScriptable.Player1Score}");
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
            player2.SetText($"{scoreScriptable.Player2Score}");
        }
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