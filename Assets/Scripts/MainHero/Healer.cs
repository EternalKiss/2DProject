using UnityEngine;

public class Healer : MonoBehaviour
{
    private ItemPickUp _itemPicked;
    private IDamageable _target;

    private void Awake()
    {

        _itemPicked = GetComponent<ItemPickUp>();
        _target = GetComponent<IDamageable>();
    }

    private void OnEnable()
    {
        _itemPicked.ItemPicked += TryHeal;
    }

    private void OnDisable()
    {
        _itemPicked.ItemPicked -= TryHeal;
    }

    private void TryHeal(Item item)
    {
        IUsable usableItem = item.GetComponent<IUsable>();

        if (usableItem != null)
        {
            usableItem.Use(_target);
        }
    }
}
