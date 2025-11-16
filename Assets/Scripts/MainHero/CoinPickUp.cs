using System;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    public event Action<Coin> onPicked;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Coin>(out Coin coin))
        {
            onPicked?.Invoke(coin);
        }
    }
}
