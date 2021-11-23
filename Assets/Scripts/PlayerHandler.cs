using Cysharp.Threading.Tasks;
using System;

using UnityEngine;

public enum PlayerState
{
    Active,
    Inactive
}

public class PlayerHandler : MonoBehaviour
{
    public event Action<GameObject> RollDice;

    public event Action<GameObject> BankScore;

    public int Score { get; set; }

    public void Roll()
    {
        RollAsync().Forget();
    }

    private async UniTaskVoid RollAsync()
    {
        RollDice?.Invoke(this.gameObject);
        await UniTask.Yield();
    }

    public void Bank()
    {
        BankAsync().Forget();
    }

    private async UniTaskVoid BankAsync()
    {
        BankScore?.Invoke(this.gameObject);
        await UniTask.Yield();
    }
}