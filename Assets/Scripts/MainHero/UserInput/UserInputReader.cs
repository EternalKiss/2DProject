using UnityEngine;

public class UserInputReader : MonoBehaviour
{
    public const string Horizontal = "Horizontal";

    [SerializeField] private KeyCode AttackKey = KeyCode.F;
    [SerializeField] private KeyCode JumpKey = KeyCode.Space;
    [SerializeField] private KeyCode Abillity = KeyCode.E;

    private bool _isJump;
    private bool _isAttack;
    private bool _isCast;

    public float Direction { get; private set; }
    public bool IsJumpPressed { get; private set; }
    public bool IsAttackPressed { get; private set; }
    public bool IsCastPressed { get; private set; }

    private void Update()
    {
        Direction = Input.GetAxis(Horizontal);

        UserKeyCode();
    }

    private void UserKeyCode()
    {
        if (Input.GetKeyDown(JumpKey))
        {
            _isJump = true;
        }

        if (Input.GetKeyDown(AttackKey))
        {
            _isAttack = true;
        }

        if(Input.GetKeyDown(Abillity))
        {
             _isCast = true;
        }

        IsCastPressed = _isCast;
        IsAttackPressed = _isAttack;
        IsJumpPressed = _isJump;

        _isCast = false;
        _isAttack = false;
        _isJump = false;
    }
}
