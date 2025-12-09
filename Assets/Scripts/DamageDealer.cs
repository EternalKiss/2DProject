using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private LayerMask _targetLayers;

    private float _nextAttackTime;

    public void TakeDamage(float health, float damage)
    {
        health -= damage;
    }

    public bool TryAttack(float damage, float attackDistance, float moveDirection, float attackDelay)
    {
        if (Time.time < _nextAttackTime)
        {
            return false;
        }

        Vector2 direction = moveDirection > 0 ? Vector2.right : Vector2.left;

        Vector2 rayOrigin = (Vector2)transform.position + new Vector2(0, 1.5f);
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, direction, attackDistance, _targetLayers);

        if (hit.collider != null)
        {
            IDamageable damageable = hit.collider.GetComponent<IDamageable>();

            if (damageable != null)
            {
                damageable.TakeDamage(damage, gameObject);

                _nextAttackTime = Time.time + attackDelay;
                return true;
            }
        }

        _nextAttackTime = Time.time + attackDelay;

        return false;
    }
}
