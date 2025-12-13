using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class VampirismAbillity : MonoBehaviour
{
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private Health _playerHealth;
    [SerializeField] private Image _barView;

    private float _duration = 6f;
    private float _cooldown = 4f;
    private float _radius = 10f;
    private float _heal = 10f;
    private float _damage = 10f;
    private float _tickRate = 0.5f;
    private bool _onCooldown = false;

    private Coroutine _abilityCoroutine;

    public event Action<float, float, float> AbillityActivated;
    public event Action<float, float> AbillityDeactivated;

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
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _radius, _enemyLayer);

        IDamageable nearestTarget = null;
        float minDistance = Mathf.Infinity;
        Vector3 playerPosition = transform.position;

        foreach (Collider2D collider in colliders)
        {
            IDamageable damageableTarget = collider.GetComponent<IDamageable>();

            if (damageableTarget != null)
            {
                if (transform.position.IsEnoughClose(collider.transform.position, _radius))
                {
                    float distance = transform.position.SqrDistance(collider.transform.position);

                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        nearestTarget = damageableTarget;
                    }
                }

            }
        }

        return nearestTarget;
    }

    private IEnumerator CooldownRoutine()
    {
        float elapsedTime = 0f;
        _barView.enabled = true;
        AbillityDeactivated?.Invoke(elapsedTime, _cooldown);

        while (elapsedTime < _cooldown)
        {
            AbillityDeactivated?.Invoke(elapsedTime, _cooldown);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        _onCooldown = false;
        _barView.enabled = false;
    }

    private IEnumerator Use()
    {
        float elapsedTime = 0f;
        _onCooldown = true;
        AbillityActivated?.Invoke(elapsedTime, _duration, _radius);
        var waitInstruction = new WaitForSeconds(_tickRate);

        while (elapsedTime < _duration)
        {
            elapsedTime += Time.deltaTime;
            IDamageable currentTarget = GetTarget();

            if (currentTarget != null && _playerHealth != null)
            {
                currentTarget.TakeDamage(_damage);

                _playerHealth.Heal(_heal);
            }

            elapsedTime += _tickRate;
            yield return waitInstruction;
        }

        _abilityCoroutine = null;
        StartCoroutine(CooldownRoutine());
    }
}
