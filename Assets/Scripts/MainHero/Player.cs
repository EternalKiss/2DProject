using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _characterSprite;
    [SerializeField] private UserInputReader _inputReader;
    [SerializeField] private Rotater _rotater;
    [SerializeField] private Mover _mover;
    [SerializeField] private GroundDetector _groundDetector;
    [SerializeField] private CharacterAnimations _characterAnimations;

    private Rigidbody2D _rigidbody;
    private bool _isRunning;

    private void Awake()
    {
        _characterAnimations = GetComponent<CharacterAnimations>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _characterAnimations.SetIsFlying(_groundDetector.IsFlying(_rigidbody));
        _isRunning = _inputReader.Direction != 0;
        _characterAnimations.SetIsRunning(_isRunning);

        if (_inputReader.Direction != 0)
        {
            _mover.Move(_inputReader.Direction, _rigidbody);
            _rotater.Rotate(_inputReader.Direction);
        }

        if (_inputReader.IsJumpPressed && _groundDetector.IsGround)
        {
            _mover.Jump(_rigidbody);
            _characterAnimations.Jump();
        }
    }
}
