using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VampirismAbillity : MonoBehaviour
{
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private Health _playerHealth;
    [SerializeField] private Viewer _viewer;
    [SerializeField] private Image _barView;

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
        _barView.enabled = true;

        if (!_onCooldown && _abilityCoroutine == null)
        {
            _abilityCoroutine = StartCoroutine(Use());
        }
        else
        {
            Debug.Log("Способность перезаряжется!");
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
        float elapsedTime = 0f;
        _viewer.CalculateCooldownView(elapsedTime, _cooldown);
        _barView.enabled = true;

        while (elapsedTime < _cooldown)
        {
            elapsedTime += Time.deltaTime;
            _viewer.CalculateCooldownView(elapsedTime, _cooldown);

            yield return null;
        }

        _onCooldown = false;
        _barView.enabled = false;
    }

    private IEnumerator Use()
    {
        _onCooldown = true;
        float elapsedTime = 0f;
        _viewer.ShowRadiusAndDuration(_abilityRadius);

        while (elapsedTime < _duration)
        {
            _viewer.CalculateDurationView(elapsedTime, _duration);

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
        _viewer.HideRadiusAndDuration();

    }
}
