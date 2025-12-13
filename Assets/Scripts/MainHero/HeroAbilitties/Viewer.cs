using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Viewer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _radiusSprite;
    [SerializeField] private Image _durationFilling;
    [SerializeField] private Image _cooldownFilling;
    [SerializeField] private VampirismAbillity _vampirismAbillity;

    private void OnEnable()
    {
        _vampirismAbillity.AbillityActivated += StartAbillityCoroutine;
        _vampirismAbillity.AbillityDeactivated += CalculateCooldownView;
    }

    private void OnDisable()
    {
        _vampirismAbillity.AbillityActivated -= StartAbillityCoroutine;
        _vampirismAbillity.AbillityDeactivated -= CalculateCooldownView;
    }

    private void StartAbillityCoroutine(float elapsedTime, float duration, float abillityRadius)
    {
        StartCoroutine(ShowAbillityViewCoroutine(elapsedTime, duration, abillityRadius));
    }

    private IEnumerator ShowAbillityViewCoroutine(float elapsedTime, float duration, float abillityRadius)
    {
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            CalculateDurationView(elapsedTime, duration, abillityRadius);
            ShowRadiusAndDuration(abillityRadius);

            yield return null;
        }

        HideRadiusAndDuration();
    }

    private void CalculateDurationView(float elapsedTime, float duration, float abillityRadius)
    {
        float progress = elapsedTime / duration;
        _durationFilling.fillAmount = Mathf.Clamp01(1f - progress);
        ShowRadiusAndDuration(abillityRadius);
    }

    private void CalculateCooldownView(float elapsedTime, float cooldown)
    {
        float progress = elapsedTime / cooldown;
        _cooldownFilling.fillAmount = Mathf.Clamp01(progress);
    }

    private void ShowRadiusAndDuration(float abilityRadius)
    {
        if (_radiusSprite == null) return;

        _radiusSprite.enabled = true;
        _durationFilling.enabled = true;
        _cooldownFilling.enabled = false;

        float spritePPU = _radiusSprite.sprite.pixelsPerUnit;
        float Pi = 2f;
        float scale = (Pi * abilityRadius) / spritePPU;

        _radiusSprite.transform.localScale = new Vector3(scale, scale, 1f);
        _radiusSprite.color = new Color(1f, 1f, 1f, 0.4f);
    }

    private void HideRadiusAndDuration()
    {
        _radiusSprite.enabled = false;
        _durationFilling.enabled = false;
        _cooldownFilling.enabled = true;
    }
}
