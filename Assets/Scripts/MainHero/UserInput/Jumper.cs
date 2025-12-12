using UnityEngine;

public class Jumper : MonoBehaviour
{
    [SerializeField] private float _jumpForce;

    public void Jump(Rigidbody2D rigidbody)
    {
        rigidbody.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
    }
}
