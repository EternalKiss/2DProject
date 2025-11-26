using Unity.VisualScripting;
using UnityEngine;

public class UserInputReader : MonoBehaviour
{
    public const string Horizontal = "Horizontal";

    private bool _isJump;
    private bool _isAttack;

    public float Direction { get; private set; }
    public bool IsJumpPressed { get; private set; }
    public bool IsAttackPressed { get; private set; }

    private void Update()
    {
        Direction = Input.GetAxis(Horizontal);

        if (Input.GetKeyDown(KeyCode.Space))
        {
               _isJump = true;
        }

        if(Input.GetKeyDown(KeyCode.F))
        {
            _isAttack = true;
        }

        IsAttackPressed = _isAttack;
        IsJumpPressed = _isJump;

        _isAttack = false;
        _isJump = false;
    }
}
