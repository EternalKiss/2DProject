using UnityEngine;

public class CharacterAnimations : MonoBehaviour
{
    private Animator _animator;

    public bool IsRunning {  private get; set; }
    public bool IsFlying { private get; set; } 

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _animator.SetBool("IsRunning", IsRunning);
        _animator.SetBool("IsFlying", IsFlying);
    }

    public void Jump()
    {
        if (_animator.GetBool("IsFlying") == false)
        {
            _animator.SetTrigger("Jump");
        }
    }
}
