using UnityEngine;

public class AnimationsController : MonoBehaviour
{
    private const string SpeedParam = "MoveSpeed";
    private const string IsRunningParam = "IsRunning";
    private const string AttackTrigger = "Attack";

    [SerializeField] private Animator _animator;

    private int _speedHash;
    private int _isRunningHash;
    private int _attackHash;


    private void Awake()
    {
        _speedHash = Animator.StringToHash(SpeedParam);
        _isRunningHash = Animator.StringToHash(IsRunningParam);
        _attackHash = Animator.StringToHash(AttackTrigger);
    }

    private void Update()
    {
        bool isRunning = _animator.GetBool(_isRunningHash);
    }

    public void Attack()
    {
        if (_animator.GetBool(_attackHash) == false)
        {
            _animator.SetTrigger(_attackHash);
        }
    }

    public void SetSpeed(float value)
    {
        _animator.SetFloat(_speedHash, value);
    }

    public void SetIsRunning(bool isRunning)
    {
        _animator.SetBool(_isRunningHash, isRunning);
    }
}
