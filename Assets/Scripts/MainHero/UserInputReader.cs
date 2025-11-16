using UnityEngine;

public class UserInputReader : MonoBehaviour
{
    public const string Horizontal = "Horizontal";

    private bool _isJump;
    
    public float Direction {  get; private set; }
    public bool IsJumpPressed { get; private set; }

    private void Update()
    {
        Direction = Input.GetAxis(Horizontal);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _isJump = true;
        }

        IsJumpPressed = _isJump;
        _isJump = false;
    }
}
