using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    [SerializeField] private Transform player1;
    [SerializeField] private Transform player2;
    [SerializeField] private Dice dice;

    private Transform currentPlayer;
    private int roundCount;

    public static event Action<GameObject, int> Result;

    public static event Action<int> Round;

    private bool rollLock = false;

    private void OnEnable()
    {
        player1.GetComponent<PlayerHandler>().RollDice += OnPlayerRoll;
        player1.GetComponent<PlayerHandler>().BankScore += OnBankScore;
        player2.GetComponent<PlayerHandler>().RollDice += OnPlayerRoll;
        player2.GetComponent<PlayerHandler>().BankScore += OnBankScore;
    }

    private void Start()
    {
        player2.gameObject.SetActive(false);
        currentPlayer = player1;
    }

    private void OnBankScore(GameObject player)
    {
        OnBankScoreAsync(player).Forget();
    }

    private async UniTaskVoid OnBankScoreAsync(GameObject player)
    {
        if (!rollLock)
        {
            SwitchPlayer(player);
        }
        await UniTask.Yield();
    }

    private void OnPlayerRoll(GameObject player)
    {
        OnPlayerRollAsync(player).Forget();
    }

    private async UniTaskVoid OnPlayerRollAsync(GameObject player)
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
                SwitchPlayer(player);
            }
            Result?.Invoke(player, result);
            roundCount++;
            Round?.Invoke(roundCount);
            rollLock = false;
        }
        await UniTask.Yield();
    }

    private void SwitchPlayer(GameObject player)
    {
        if (player.name.Equals("Player1"))
        {
            player1.gameObject.SetActive(false);
            currentPlayer = player2;
        }
        else
        {
            player2.gameObject.SetActive(false);
            currentPlayer = player1;
        }
        currentPlayer.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        player1.GetComponent<PlayerHandler>().RollDice -= OnPlayerRoll;
        player1.GetComponent<PlayerHandler>().BankScore -= OnBankScore;
        player2.GetComponent<PlayerHandler>().RollDice -= OnPlayerRoll;
        player2.GetComponent<PlayerHandler>().BankScore -= OnBankScore;
    }
}