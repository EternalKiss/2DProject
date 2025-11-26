using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private SpriteRenderer _characterSprite;
    [SerializeField] private Rotater _rotater;
    [SerializeField] private Patroller _patroller;
    [SerializeField] private AnimationsController _animationsController;
    [SerializeField] private TargetDetector _detector;
    [SerializeField] private DealDamage _dealDamage;
    [SerializeField] private Health _health;

    private bool _isRunning;
    private float _stopDistance = 5f;
    private float _damage = 20f;
    private float _attackDistance = 10f;
    private float _attackDelay = 1f;

    private Transform _target;

    public Health GetHealthComponent() => _health;
    public bool IsAlive => _health.CurrentHealth > 0;

    private void FixedUpdate()
    {
        Move();
        _detector.Detector();
    }

    private void Move()
    {
        if (_target != null)
        {

            Vector3 targetPosition = _target.position;
            float distanceToTarget = Vector2.Distance(transform.position, targetPosition);

            float directionX = Mathf.Sign(targetPosition.x - transform.position.x);
            _rotater.Rotate(directionX);

            if (distanceToTarget > _stopDistance)
            {
                transform.position = Vector2.MoveTowards(
                    transform.position,
                    targetPosition,
                    _moveSpeed * Time.deltaTime
                );
            }
            else
            {
                if (_dealDamage.TryAttack(_damage, _attackDistance, directionX, _attackDelay))
                    _animationsController.Attack();
            }
        }
        else
        {
            Vector3 currentTargetPosition = _patroller.GetCurrentWaypointPosition();
            float distanceToTarget = _patroller.CalculateDistance(currentTargetPosition);

            if (distanceToTarget > _stopDistance)
            {
                transform.position = Vector2.MoveTowards(
                    transform.position,
                    currentTargetPosition,
                    _moveSpeed * Time.deltaTime
                );
            }
            else
            {
                Vector2 nextDirection = (Vector2)transform.position - (Vector2)currentTargetPosition;
                float directionX = Mathf.Sign(nextDirection.x);
                _patroller.SetNewWayPoint();
                _rotater.Rotate(directionX);
            }
        }

        _isRunning = _moveSpeed > 0;
        _animationsController.SetIsRunning(_isRunning);
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    public void ClearTarget(Transform target)
    {
        _target = null;
    }

    public void TakeDamage(float damage, GameObject attacker)
    {
        _health.SetHealth(_health.CurrentHealth - damage);

        if (!IsAlive) Destroy(gameObject);
    }
}
