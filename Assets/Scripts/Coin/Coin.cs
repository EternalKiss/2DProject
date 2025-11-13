using System;
using UnityEngine;
using UnityEngine.Pool;

public class Coin : MonoBehaviour
{
    private ObjectPool<Coin> _pool;

    public void SetPool(ObjectPool<Coin> coinPool)
    {
        _pool = coinPool;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _pool.Release(this);
        }
    }
}
