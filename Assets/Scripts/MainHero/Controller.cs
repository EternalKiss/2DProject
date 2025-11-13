using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private Vector3 _groundCheckOffSet;
    [SerializeField] private SpriteRenderer _characterSprite;
    [SerializeField] private LayerMask _groundMask;

    private Vector3 _input;
    private Rigidbody2D _rigidBody;
    private CharacterAnimations _characterAnimations;
    private bool _isRunning;
    private bool _isGrounded;

    private void Awake()
    {
        _characterAnimations = GetComponent<CharacterAnimations>();
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
        CheckGround();
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if( _isGrounded )
            {
                Jump();
            }
        }

        _characterAnimations.IsRunning = _isRunning;
        _characterAnimations.IsFlying = IsFlying();
    }

    private void CheckGround()
    {
        float rayLength = 1.5f;
        Vector3 rayStartPosition = transform.position + _groundCheckOffSet;
        RaycastHit2D hit = Physics2D.Raycast(rayStartPosition, Vector3.down, rayLength, _groundMask);

        if (hit.collider != null)
        {
            _isGrounded = true;
        }
        else
        {
            _isGrounded = false;
        }
    }

    private bool IsFlying()
    {
        return !_isGrounded && _rigidBody.linearVelocity.y < 0.1f;
    }

    private void Move()
    {
        _input = new Vector2(Input.GetAxis("Horizontal"), 0);
        transform.position += _input * _speed * Time.deltaTime;
        _isRunning = _input.x != 0 ? true : false;

        if(_input.x != 0)
        {
            _characterSprite.flipX = _input.x > 0 ? false : true;
        }

        _characterAnimations.IsRunning = _isRunning;
    }

    private void Jump()
    {
        _rigidBody.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
        _characterAnimations.Jump();
    }
}
