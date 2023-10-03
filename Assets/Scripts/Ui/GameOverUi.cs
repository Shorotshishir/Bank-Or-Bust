using System.Text;
using UnityEngine;
using UnityEngine.UIElements;

public class GameOverUi : MonoBehaviour
{
    [SerializeField] private ScoreScriptable scoreScriptable;
    [SerializeField] private UIDocument uiDocument;
    [SerializeField] private UiHandler uiHandler;

    private Button restartGame;

    private StringBuilder sb;
    private Label status;

    private Label winner;


    private void OnEnable()
    {
        sb = new StringBuilder();
        restartGame = uiDocument.rootVisualElement.Q("restart") as Button;

        winner = uiDocument.rootVisualElement.Q<Label>("final_winner");
        status = uiDocument.rootVisualElement.Q<Label>("final_status");

        restartGame.RegisterCallback<ClickEvent>(OnRestartClick);
        uiHandler.OnGameOver += ScoreScriptableOnOnGameOver;
    }

    private void OnDisable()
    {
        restartGame.UnregisterCallback<ClickEvent>(OnRestartClick);
        scoreScriptable.OnGameOver -= ScoreScriptableOnOnGameOver;
    }

    private void ScoreScriptableOnOnGameOver()
    {
        UpdateWiningResult();
    }

    private void UpdateWiningResult()
    {
        winner.text = scoreScriptable.GetStatus();
        ShowStatus();
    }

    private void ShowStatus()
    {
        sb.Append($"player1 : {scoreScriptable.Player1Score}");
        sb.Append("\n");
        sb.Append($"player2 : {scoreScriptable.Player2Score}");
        status.text = sb.ToString();
        sb.Clear();
    }

    private void OnRestartClick(ClickEvent evt)
    {
        print("Restart Clicked");
        uiHandler.ShowMainUi();
    }
}