using UnityEngine;

public class UserInputReader : MonoBehaviour
{
    public const string Horizontal = "Horizontal";

    [SerializeField] private KeyCode AttackKey = KeyCode.F;
    [SerializeField] private KeyCode JumpKey = KeyCode.Space;

    private bool _isJump;
    private bool _isAttack;

    public float Direction { get; private set; }
    public bool IsJumpPressed { get; private set; }
    public bool IsAttackPressed { get; private set; }

    private void Update()
    {
        Direction = Input.GetAxis(Horizontal);

        if (Input.GetKeyDown(JumpKey))
        {
            _isJump = true;
        }

        if (Input.GetKeyDown(AttackKey))
        {
            _isAttack = true;
        }

        IsAttackPressed = _isAttack;
        IsJumpPressed = _isJump;

        _isAttack = false;
        _isJump = false;
    }
}
