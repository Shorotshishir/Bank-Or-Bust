using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class Dice : MonoBehaviour
{
    public Sprite[] diceSides;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    public async UniTask<int> RollAsync()
    {
        var result = 0;
        var random = new System.Random();
        result = random.Next(0, 6);

        for (var i = 0; i < 20; i++)
        {
            result = UnityEngine.Random.Range(0, 5);
            spriteRenderer.sprite = diceSides[result];
            await UniTask.Delay(50);
        }

        return result + 1;
    }
}