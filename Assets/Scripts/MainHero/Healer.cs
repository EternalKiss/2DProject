using UnityEngine;

public class Healer : MonoBehaviour
{
    private float _heal = 40f;

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

    public void TryHeal(Item item)
    {
        if (IsHealthPackPicked(item))
        {
            ApplyHeal();
        }
    }

    private bool IsHealthPackPicked(Item item)
    {
        return (item.TryGetComponent<FirstAidKit>(out _));
    }

    private void ApplyHeal()
    {
        Health healthComponent = _target.GetHealthComponent();

        if (healthComponent != null)
        {
            float newHealth = healthComponent.CurrentHealth + _heal;
            healthComponent.SetHealth(newHealth);
        }
    }
}
