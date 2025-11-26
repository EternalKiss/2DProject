using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100f;

    private float _currentHealth;

    public float MaxHealth => _maxHealth;
    public float CurrentHealth => _currentHealth;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void SetHealth(float value)
    {
        _currentHealth = Mathf.Clamp(value, 0f, _maxHealth);
    }

    public void SetMaxHealth(float newMax)
    {
        _maxHealth = newMax;
        if (_currentHealth > _maxHealth)
            _currentHealth = _maxHealth;
    }
}
