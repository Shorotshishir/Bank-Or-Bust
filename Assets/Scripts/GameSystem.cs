using Cysharp.Threading.Tasks;
using System;
using System.Threading.Tasks;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    [SerializeField] private Transform player1;
    [SerializeField] private Transform player2;
    [SerializeField] private Dice dice;

    [SerializeField] private MainUi mainUi;

    private Transform currentPlayer;
    private int roundCount;

    // public static event Action<GameObject, int> Result;
    public static event Action<string, int> Result;

    public static event Action<int> Round;

    private bool rollLock = false;

    private void OnEnable()
    {
        mainUi.PlayerClickAction += MainUi_PlayerClickAction;
    }

    private void Start()
    {
        player2.gameObject.SetActive(false);
        currentPlayer = player1;
    }

    private async void MainUi_PlayerClickAction(string playerName, string buttonType)
    {
        if (playerName.Equals("Player1"))
        {
            if (buttonType.Equals("Bank"))
            {
                ActivatePlayer2();
            }
            else if (buttonType.Equals("Roll"))
            {
                OnPlayerRollAsync(playerName).Forget();
            }
        }
        else
        {
            if (buttonType.Equals("Bank"))
            {
                ActivatePlayer1();
            }
            else if (buttonType.Equals("Roll"))
            {
                OnPlayerRollAsync(playerName).Forget();
            }
        }
        await Task.CompletedTask;
    }

    private void ActivatePlayer2()
    {
        mainUi.ChangePlayer1Status(false);
        mainUi.ChangePlayer2Status(true);
    }
    private void ActivatePlayer1()
    {
        mainUi.ChangePlayer1Status(true);
        mainUi.ChangePlayer2Status(false);
    }


    private async UniTaskVoid OnPlayerRollAsync(string playerName)
    {
        if (rollLock)
        {
            Debug.Log("Roll Locked");
            return;
        }

        if (dice != null)
        {
            rollLock = true;
            var result = await dice.RollAsync();
            if (result == 1)
            {
                SwitchPlayer(playerName);
            }
            Result?.Invoke(playerName, result);
            roundCount++;
            Round?.Invoke(roundCount);
            rollLock = false;
        }
        await UniTask.Yield();
    }



    private void SwitchPlayer(string playerName)
    {
        if (playerName.Equals("Player1"))
        {
            ActivatePlayer2();
        }
        else if(playerName.Equals("Player2"))
        {
            ActivatePlayer1();
        }
    }


    private void OnDisable()
    {
        mainUi.PlayerClickAction += MainUi_PlayerClickAction;
    }
}