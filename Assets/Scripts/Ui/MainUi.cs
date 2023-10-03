using System;
using UnityEngine;
using UnityEngine.UIElements;

public class MainUi : MonoBehaviour
{
    [SerializeField] private ScoreScriptable scoreScriptable;
    [SerializeField] private UiHandler uiHandler;
    private Button bank1;
    private Button bank2;

    private Button roll1;
    private Button roll2;

    private Label roundCountL;
    private Label roundCountR;

    private Label score1;
    private Label score2;
    
    public event Action<string, string> PlayerClickAction;

    private void OnEnable()
    {
        uiHandler.OnGameRestart += UiHandlerOnOnGameRestart;
        var uiDocument = GetComponent<UIDocument>();
        bank1 = uiDocument.rootVisualElement.Q("Bank1") as Button;
        bank2 = uiDocument.rootVisualElement.Q("Bank2") as Button;
        roll1 = uiDocument.rootVisualElement.Q("Roll1") as Button;
        roll2 = uiDocument.rootVisualElement.Q("Roll2") as Button;

        score1 = uiDocument.rootVisualElement.Q<Label>("Score1");
        score2 = uiDocument.rootVisualElement.Q<Label>("Score2");
        roundCountL = uiDocument.rootVisualElement.Q<Label>("RoundCountL");
        roundCountR = uiDocument.rootVisualElement.Q<Label>("RoundCountR");

        bank1.RegisterCallback<ClickEvent>(OnBank1Click);
        bank2.RegisterCallback<ClickEvent>(OnBank2Click);
        roll1.RegisterCallback<ClickEvent>(OnRoll1Click);
        roll2.RegisterCallback<ClickEvent>(OnRoll2Click);

        InitialSetup();

        scoreScriptable.OnScoreUpdate += ScoreScriptable_ScoreUpdated;
        scoreScriptable.OnGameOver += ScoreScriptable_OnGameOver;
        scoreScriptable.OnRoundUpdate += ScoreScriptable_OnRoundUpdate;
    }

    private void OnDisable()
    {
        bank1.UnregisterCallback<ClickEvent>(OnBank1Click);
        bank2.UnregisterCallback<ClickEvent>(OnBank2Click);
        roll1.UnregisterCallback<ClickEvent>(OnRoll1Click);
        roll2.UnregisterCallback<ClickEvent>(OnRoll2Click);

        scoreScriptable.OnGameOver -= ScoreScriptable_OnGameOver;
        scoreScriptable.OnScoreUpdate -= ScoreScriptable_ScoreUpdated;
        scoreScriptable.OnRoundUpdate -= ScoreScriptable_OnRoundUpdate;
    }


    private void UiHandlerOnOnGameRestart()
    {
        InitialSetup();
    }

    private void InitialSetup()
    {
        ChangePlayer1Status(true);
        ChangePlayer2Status(false);
    }


    private void ScoreScriptable_OnRoundUpdate()
    {
        roundCountL.text = $"Round : {scoreScriptable.RoundCount}";
        roundCountR.text = $"Round : {scoreScriptable.RoundCount}";
    }

    private void ScoreScriptable_OnGameOver()
    {
        ChangePlayer1Status(false);
        ChangePlayer2Status(false);
    }

    private void ScoreScriptable_ScoreUpdated()
    {
        ChangePlayer1Score(scoreScriptable.Player1Score);
        ChangePlayer2Score(scoreScriptable.Player2Score);
    }

    public void ChangePlayer1Score(int score)
    {
        score1.text = $"Score : {score}";
    }

    public void ChangePlayer2Score(int score)
    {
        score2.text = $"Score : {score}";
    }

    public void ChangePlayer1Status(bool status)
    {
        bank1.SetEnabled(status);
        roll1.SetEnabled(status);
    }

    public void ChangePlayer2Status(bool status)
    {
        bank2.SetEnabled(status);
        roll2.SetEnabled(status);
    }

    private void OnBank1Click(ClickEvent evt)
    {
        PlayerClickAction?.Invoke("Player1", "Bank");
    }

    private void OnRoll1Click(ClickEvent evt)
    {
        PlayerClickAction?.Invoke("Player1", "Roll");
    }

    private void OnBank2Click(ClickEvent evt)
    {
        PlayerClickAction?.Invoke("Player2", "Bank");
    }

    private void OnRoll2Click(ClickEvent evt)
    {
        PlayerClickAction?.Invoke("Player2", "Roll");
    }
}