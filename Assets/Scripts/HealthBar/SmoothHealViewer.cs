using System.Collections;
using UnityEngine;

public class SmoothHealViewer : HealthViewer
{
    [SerializeField] private float _smoothSpeed = 2f;

    private IEnumerator _smoothChangeCoroutine;

    private void OnDisable()
    {
        if (_smoothChangeCoroutine != null)
        {
            StopCoroutine(_smoothChangeCoroutine);
        }
    }

    protected override void SetValue(float health, float maxHealth)
    {
        if (_smoothChangeCoroutine != null)
        {
            StopCoroutine(_smoothChangeCoroutine);
        }

        _smoothChangeCoroutine = SmoothChangeRoutine(health / maxHealth);
        StartCoroutine(_smoothChangeCoroutine);
    }

    private IEnumerator SmoothChangeRoutine(float targetFillAmount)
    {

        float startFillAmount = _healthBar.fillAmount;
        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * _smoothSpeed;
            _healthBar.fillAmount = Mathf.Lerp(startFillAmount, targetFillAmount, Mathf.Clamp01(elapsedTime));

            yield return null;
        }

        _healthBar.fillAmount = targetFillAmount;
        _smoothChangeCoroutine = null;
    }
}
