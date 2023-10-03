using System;
using UnityEngine;

public class UiHandler : MonoBehaviour
{
    public Transform GameOverUi;
    public Transform MainUi;

    [SerializeField] private ScoreScriptable score;

    
    public event Action OnGameOver;
    public event Action OnGameRestart;
    
    private void OnEnable()
    {
        score.OnGameOver += ScoreOnOnGameOver;
    }

    private void OnDisable()
    {
        score.OnGameOver -= ScoreOnOnGameOver;
    }
    
    private void ScoreOnOnGameOver()
    {
        ShowGameoverUi();
        OnGameOver?.Invoke();
    }

    private void ShowGameoverUi()
    {
        GameOverUi.gameObject.SetActive(true);
        MainUi.gameObject.SetActive(false);
    }

    public void ShowMainUi()
    {
        MainUi.gameObject.SetActive(true);
        GameOverUi.gameObject.SetActive(false);
        OnGameRestart?.Invoke();
        score.ResetData();
    }
}