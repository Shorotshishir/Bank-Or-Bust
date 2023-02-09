using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainUi : MonoBehaviour
{
    private Button Bank1;
    private Button Bank2;

    private Button Roll1;
    private Button Roll2;

    private Label Score1;
    private Label Score2;

    private Label RoundCountL;
    private Label RoundCountR;

    public event Action<string, string> PlayerClickAction;
    [SerializeField] private ScoreScriptable scoreScriptable;


    private void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();
        Bank1 = uiDocument.rootVisualElement.Q("Bank1")as Button;
        Bank2 = uiDocument.rootVisualElement.Q("Bank2")as Button;
        Roll1 = uiDocument.rootVisualElement.Q("Roll1")as Button;
        Roll2 = uiDocument.rootVisualElement.Q("Roll2")as Button;

        Score1 = uiDocument.rootVisualElement.Q<Label>("Score1");
        Score2 = uiDocument.rootVisualElement.Q<Label>("Score2");
        RoundCountL = uiDocument.rootVisualElement.Q<Label>("RoundCountL");
        RoundCountR = uiDocument.rootVisualElement.Q<Label>("RoundCountR");

        Bank1.RegisterCallback<ClickEvent>(OnBank1Click);
        Bank2.RegisterCallback<ClickEvent>(OnBank2Click);
        Roll1.RegisterCallback<ClickEvent>(OnRoll1Click);
        Roll2.RegisterCallback<ClickEvent>(OnRoll2Click);

        ChangePlayer2Status(false);

        scoreScriptable.OnScoreUpdate += ScoreScriptable_ScoreUpdated;
        scoreScriptable.OnGameOver += ScoreScriptable_OnGameOver;
        scoreScriptable.OnRoundUpdate += ScoreScriptable_OnRoundUpdate;
    }

    private void ScoreScriptable_OnRoundUpdate()
    {
        RoundCountL.text = $"Round : {scoreScriptable.RoundCount}";
        RoundCountR.text = $"Round : {scoreScriptable.RoundCount}";
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
        Score1.text = $"Score : {score}";
    }
    public void ChangePlayer2Score(int score)
    {
        Score2.text = $"Score : {score}";
    }
    public void ChangePlayer1Status(bool status)
    {
        Bank1.SetEnabled(status);
        Roll1.SetEnabled(status);
    }
    public void ChangePlayer2Status(bool status)
    {
        Bank2.SetEnabled(status);
        Roll2.SetEnabled(status);
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

    void OnDisable()
    {
        Bank1.UnregisterCallback<ClickEvent>(OnBank1Click);
        Bank2.UnregisterCallback<ClickEvent>(OnBank2Click);
        Roll1.UnregisterCallback<ClickEvent>(OnRoll1Click);
        Roll2.UnregisterCallback<ClickEvent>(OnRoll2Click);

        scoreScriptable.OnGameOver -= ScoreScriptable_OnGameOver;
        scoreScriptable.OnScoreUpdate -= ScoreScriptable_ScoreUpdated;
    }
}
