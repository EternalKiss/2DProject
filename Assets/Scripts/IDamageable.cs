
using UnityEngine;

public interface IDamageable
{
    void TakeDamage(float damage);
    Health GetHealthComponent();
}
