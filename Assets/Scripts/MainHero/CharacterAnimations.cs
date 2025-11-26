using UnityEngine;

public class CharacterAnimations : MonoBehaviour
{
    private const string SpeedParam = "MoveSpeed";
    private const string IsRunningParam = "IsRunning";
    private const string IsFlyingParam = "IsFlying";
    private const string JumpTrigger = "Jump";
    private const string AttackTrigger = "Attack";

    private int _speedHash;
    private int _isRunningHash;
    private int _isFlyingHash;
    private int _jumpHash;
    private int _attackHash;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        _speedHash = Animator.StringToHash(SpeedParam);
        _isRunningHash = Animator.StringToHash(IsRunningParam);
        _isFlyingHash = Animator.StringToHash(IsFlyingParam);
        _jumpHash = Animator.StringToHash(JumpTrigger);
        _attackHash = Animator.StringToHash(AttackTrigger);
    }

    public void Jump()
    {
        if (_animator.GetBool(_isFlyingHash) == false)
        {
            _animator.SetTrigger(_jumpHash);
        }
    }

    public void Attack()
    {
        if(_animator.GetBool(_attackHash) == false)
        {
            _animator.SetTrigger(_attackHash);
        }
    }

    public void SetSpeed(float value)
    {
        _animator.SetFloat(_speedHash, value);
    }

    public void SetIsFlying(bool isFlying)
    {
        _animator.SetBool(_isFlyingHash, isFlying);
        isFlying = _animator.GetBool(_isFlyingHash);
    }

    public void SetIsRunning(bool isRunning)
    {
        _animator.SetBool(_isRunningHash, isRunning);
        isRunning = _animator.GetBool(_isRunningHash);
    }
}
