using UnityEngine;
using UnityEngine.UI;

public class Viewer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _radiusSprite;
    [SerializeField] private Image _durationFilling;
    [SerializeField] private Image _cooldownFilling;

    public void CalculateDurationView(float elapsedTime, float duration)
    {
        float progress = elapsedTime / duration;
        _durationFilling.fillAmount = Mathf.Clamp01(1f - progress);
    }

    public void CalculateCooldownView(float elapsedTime, float cooldown)
    {
        float progress = elapsedTime / cooldown;
        _cooldownFilling.fillAmount = Mathf.Clamp01(progress);
    }

    public void ShowRadiusAndDuration(float abilityRadius)
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

    public void HideRadiusAndDuration()
    {
        _radiusSprite.enabled = false;
        _durationFilling.enabled = false;
        _cooldownFilling.enabled = true;
    }
}
