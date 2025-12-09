using TMPro;
using UnityEngine;

public class HealthText : HealthBarViewer
{
    [SerializeField] private TextMeshProUGUI _viewer;

    private void Start()
    {
        float[] defaultHealth = SetDefaultCurrentHealth();

        for (int i = 0; i < 1; i++)
        {
            _viewer.text = $"{defaultHealth[i]} / {defaultHealth[++i]}";
        }
    }

    protected override void SetValue(float health, float maxHealth)
    {
        _viewer.text = $"{health} / {maxHealth}";
    }
}
