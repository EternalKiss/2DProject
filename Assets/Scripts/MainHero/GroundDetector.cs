using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    [SerializeField] private Vector3 _groundCheckOffSet;
    [SerializeField] private LayerMask _groundMask;

    public bool IsGround { get; private set; }

    private void Update()
    {
        Grounded();
    }

    public void Grounded()
    {
        float rayLength = 3f;

        Vector3 rayStartPosition = transform.position + _groundCheckOffSet;
        RaycastHit2D hit = Physics2D.Raycast(rayStartPosition, Vector3.down, rayLength, _groundMask);

        if (hit.collider != null)
        {
            IsGround = true;
        }
        else
        {
            IsGround = false;
        }
    }

    public bool IsFlying(Rigidbody2D rigidBody)
    {
        return !IsGround;
    }
}
