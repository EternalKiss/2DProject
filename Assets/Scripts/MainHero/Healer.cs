using UnityEngine;

public class Healer : MonoBehaviour
{
    private float _heal = 40f;

    private ItemPickUp _itemPicked;
    private IDamageable _target;
    private Health _health;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _itemPicked = GetComponent<ItemPickUp>();
        _target = GetComponent<IDamageable>();
    }

    private void OnEnable()
    {
        _itemPicked.onPicked += TryHeal;
    }

    private void OnDisable()
    {
        _itemPicked.onPicked -= TryHeal;
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
        return (item.TryGetComponent<FirstAidKit>(out FirstAidKit healthPack));
    }

    private void ApplyHeal()
    {
        Health healthComponent = _target.GetHealthComponent();

        if (healthComponent != null)
        {
            float newHealth = healthComponent.CurrentHealth + _heal;
            healthComponent.SetHealth(newHealth);

            Debug.Log($"{gameObject.name} восстановил {_heal} HP. " +
                  $"Текущее: {healthComponent.CurrentHealth}/{healthComponent.MaxHealth}");
        }
    }
}
