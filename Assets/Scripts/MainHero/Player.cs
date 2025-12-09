using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private UserInputReader _inputReader;
    [SerializeField] private Rotater _rotater;
    [SerializeField] private Mover _mover;
    [SerializeField] private Jumper _jumper;
    [SerializeField] private GroundDetector _groundDetector;
    [SerializeField] private CharacterAnimations _characterAnimations;
    [SerializeField] private DamageDealer _dealDamage;
    [SerializeField] private Health _health;

    private float _damage = 15;
    private float _attackDistance = 6f;
    private float _attackDelay = 0.1f;
    private Rigidbody2D _rigidbody;
    private bool _isRunning;

    public Health GetHealthComponent() => _health;
    public bool IsAlive => _health.CheckValidHealth() > 0;

    private void Awake()
    {
        _characterAnimations = GetComponent<CharacterAnimations>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_inputReader.Direction != 0)
        {
            _mover.Move(_inputReader.Direction, _rigidbody);
            _rotater.Rotate(_inputReader.Direction);
        }

        if (_inputReader.IsJumpPressed && _groundDetector.IsGround)
        {
            _jumper.Jump(_rigidbody);
            _characterAnimations.Jump();
        }

        if(_inputReader.IsAttackPressed)
        {
            _dealDamage.TryAttack(_damage, _attackDistance, _inputReader.Direction, _attackDelay);
            _characterAnimations.Attack();
        }

        _isRunning = _inputReader.Direction != 0;
        _characterAnimations.SetIsRunning(_isRunning);
        _characterAnimations.SetIsFlying(_groundDetector.IsFlying(_rigidbody));
    }

    public void TakeDamage(float damage, GameObject attacker)
    {
        if (!IsAlive) return;

        _health.TakeDamage(damage);

        if (!IsAlive)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
