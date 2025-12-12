using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100f;

    private float _currentHealth = 100f;

    public event Action<float, float> HealthChanged;

    public float[] Default { get; private set; }

    private void Awake()
    {
        _currentHealth = _maxHealth;
        Default = new float[] { _currentHealth, _maxHealth };
    }

    public void Heal(float heal)
    {
        if (heal > 0)
        {
            _currentHealth += heal;
            _currentHealth = CheckValidHealth();
        }

        HealthChanged?.Invoke(_currentHealth, _maxHealth);
    }

    public void TakeDamage(float damage)
    {
        if (damage > 0)
        {
            _currentHealth -= damage;
            _currentHealth = CheckValidHealth();
        }

        HealthChanged?.Invoke(_currentHealth, _maxHealth);
    }

    public float CheckValidHealth()
    {
        if (_currentHealth > _maxHealth)
        {
            return _maxHealth;
        }

        if (_currentHealth < 0)
        {
            return 0;
        }

        return _currentHealth;
    }
}
