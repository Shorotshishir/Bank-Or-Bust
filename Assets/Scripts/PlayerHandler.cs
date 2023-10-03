using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public enum PlayerState
{
    Active,
    Inactive
}

public class PlayerHandler : MonoBehaviour
{
    public int Score { get; set; }
    public event Action<GameObject> RollDice;

    public event Action<GameObject> BankScore;

    public void Roll()
    {
        RollAsync().Forget();
    }

    private async UniTaskVoid RollAsync()
    {
        RollDice?.Invoke(gameObject);
        await UniTask.Yield();
    }

    public void Bank()
    {
        BankAsync().Forget();
    }

    private async UniTaskVoid BankAsync()
    {
        BankScore?.Invoke(gameObject);
        await UniTask.Yield();
    }
}