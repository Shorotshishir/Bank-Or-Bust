using System;
using UnityEngine;

public class UiHandler : MonoBehaviour
{
    public Transform GameOverUi;
    public Transform MainUi;
    public Transform StartUi;
    public Transform AboutUi;

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
        HideAllUi();
        GameOverUi.gameObject.SetActive(true);
    }

    public void ShowMainUi()
    {
        HideAllUi();
        MainUi.gameObject.SetActive(true);
        OnGameRestart?.Invoke();
        score.ResetData();
    }

    public void ShowStartUi()
    {
        HideAllUi();
        StartUi.gameObject.SetActive(true);
    }

    public void ShowAboutUi()
    {
        HideAllUi();
        AboutUi.gameObject.SetActive(true);
    }

    private void HideAllUi()
    {
        StartUi.gameObject.SetActive(false);
        MainUi.gameObject.SetActive(false);
        GameOverUi.gameObject.SetActive(false);
        AboutUi.gameObject.SetActive(false);
    }
}