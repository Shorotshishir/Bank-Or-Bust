using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiHandler : MonoBehaviour
{
    public Transform GameOverUi;
    public Transform MainUi;
    public event Action OnGameOver;
    public event Action OnGameRestart;
    
    [SerializeField] private ScoreScriptable score;

    private void OnEnable()
    {
        score.OnGameOver += ScoreOnOnGameOver;
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
    }
}
