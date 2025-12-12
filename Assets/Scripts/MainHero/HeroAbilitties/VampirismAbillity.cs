using UnityEngine;
using System.Collections;

public class VampirismAbillity : MonoBehaviour
{
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private Health _playerHealth;
    [SerializeField] private SpriteRenderer _radiusSprite;

    private float _duration = 6f;
    private float _cooldown = 4f;
    private float _abilityRadius = 10f;
    private float _heal = 10f;
    private float _damage = 10f;
    private float _tickRate = 0.5f;
    private bool _onCooldown = false;

    private Coroutine _abilityCoroutine;

    public void ActivateAbility()
    {
        if (!_onCooldown && _abilityCoroutine == null)
        {
            _abilityCoroutine = StartCoroutine(Use());
        }
    }

    private IDamageable GetTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _abilityRadius, _enemyLayer);

        IDamageable nearestTarget = null;
        float minDistance = Mathf.Infinity;
        Vector3 playerPosition = transform.position;

        foreach (Collider2D collider in colliders)
        {
            IDamageable damageableTarget = collider.GetComponent<IDamageable>();

            if (damageableTarget != null)
            {
                float distance = Vector3.Distance(collider.transform.position, playerPosition);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestTarget = damageableTarget;
                }
            }
        }

        return nearestTarget;
    }

    private IEnumerator CooldownRoutine()
    {
        yield return new WaitForSeconds(_cooldown);
        _onCooldown = false;
    }

    private IEnumerator Use()
    {
        _onCooldown = true;
        float elapsedTime = 0f;
        ShowRadius();

        while (elapsedTime < _duration)
        {
            IDamageable currentTarget = GetTarget();

            if (currentTarget != null && _playerHealth != null)
            {
                currentTarget.TakeDamage(_damage, null);

                _playerHealth.Heal(_heal);
            }

            yield return new WaitForSeconds(_tickRate);
            elapsedTime += _tickRate;
        }

        _abilityCoroutine = null;
        StartCoroutine(CooldownRoutine());
        HideRadius();
    }

    private void ShowRadius()
    {
        if (_radiusSprite == null) return;

        _radiusSprite.enabled = true;

        float spritePPU = _radiusSprite.sprite.pixelsPerUnit;
        float Pi = 2f;
        float scale = (Pi * _abilityRadius) / spritePPU;

        _radiusSprite.transform.localScale = new Vector3(scale, scale, 1f);
        _radiusSprite.color = new Color(1f, 1f, 1f, 0.4f);
    }

    private void HideRadius()
    {
        _radiusSprite.enabled = false;
    }
}
