using UnityEngine;

public class DealDamage : MonoBehaviour
{
    [SerializeField] private LayerMask targetLayers;

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
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, direction, attackDistance, targetLayers);
        Debug.DrawRay(rayOrigin, direction * attackDistance, Color.red, 0.5f);

        if (hit.collider != null)
        {
            IDamageable damageable = hit.collider.GetComponent<IDamageable>();

            if (damageable != null)
            {
                damageable.TakeDamage(damage, gameObject);
                Debug.Log($"Нанесён урон {damage} цели {hit.collider.name}");

                _nextAttackTime = Time.time + attackDelay;
                return true;
            }
        }

        _nextAttackTime = Time.time + attackDelay;

        return false;
    }
}
