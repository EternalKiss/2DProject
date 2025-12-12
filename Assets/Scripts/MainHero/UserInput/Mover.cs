using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    public void Move(float direction, Rigidbody2D rigidbody)
    {
        rigidbody.linearVelocity = new Vector2(_moveSpeed * direction, rigidbody.linearVelocity.y);
    }
}
