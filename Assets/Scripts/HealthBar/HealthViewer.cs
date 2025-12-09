using UnityEngine;
using UnityEngine.UI;

public class HealthViewer : HealthBarViewer
{
    [SerializeField] protected Image _healthBar;

    protected override void SetValue(float health, float maxHealth)
    {
        _healthBar.fillAmount = health / maxHealth;
    }
}
