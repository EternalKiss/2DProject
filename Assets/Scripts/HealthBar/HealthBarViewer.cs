using UnityEngine;

public class HealthBarViewer : MonoBehaviour
{
    [SerializeField] protected Health _playerHealth;

    private void OnEnable()
    {
        _playerHealth.HealthChanged += SetValue;
    }

    private void OnDisable()
    {
        _playerHealth.HealthChanged -= SetValue;
    }

    protected virtual void SetValue(float health, float maxHealth) { }

    protected float[] SetDefaultCurrentHealth()
    {
         return _playerHealth.Default;
    }
}
