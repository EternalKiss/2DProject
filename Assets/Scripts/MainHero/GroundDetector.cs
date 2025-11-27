using System.Collections;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    [SerializeField] private Vector3 _groundCheckOffSet;
    [SerializeField] private LayerMask _groundMask;

    public bool IsGround { get; private set; }

    private Coroutine _activeCoroutine;


    private void OnEnable()
    {
        if (_activeCoroutine == null)
        {
            StartCoroutine(Ground());
        }
    }

    private void OnDisable()
    {
        if (_activeCoroutine != null)
        {
            StopCoroutine(_activeCoroutine);
        }
    }

    private IEnumerator Ground()
    {
        float rayLength = 3f;
        var wait = new WaitForSeconds(0.1f);

        while (enabled)
        {
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

            yield return wait;
        }
    }

    public bool IsFlying(Rigidbody2D rigidBody)
    {
        return !IsGround;
    }
}
