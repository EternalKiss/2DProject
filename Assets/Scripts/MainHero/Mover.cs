using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;

    public void Jump(Rigidbody2D rigidbody)
    {
        rigidbody.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
    }

    public void Move(float direction, Rigidbody2D rigidbody)
    {
        rigidbody.linearVelocity = new Vector2(_moveSpeed * direction, rigidbody.linearVelocity.y);
    }
}
