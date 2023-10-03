using Cysharp.Threading.Tasks;
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


    
    private void OnUpdateRound()
    {
       //  roundCount.SetText($"Round\n{round}");
        scoreScriptable.RoundCount++;
        scoreScriptable.RoundUpdate();
        if (scoreScriptable.RoundCount == 10)
        {
            GameOver().Forget();
        }
    }

    private async UniTaskVoid GameOver()
    {
        // wait to show the last hand
        await UniTask.Delay(1000);
        // Trigger Gameover scene
        scoreScriptable.GameOver();
    }

    private void OnDiceResult(string playerName, int result)
    {
        if (playerName.Equals("Player1"))
        {
            if (result == 1)
            {
                scoreScriptable.Player1Score = 0;
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
        scoreScriptable.ResetData();
    }
}