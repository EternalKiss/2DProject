using System;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public event Action<Item> ItemPicked;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Item>(out Item item))
        {
            ItemPicked?.Invoke(item);
        }
    }
}
