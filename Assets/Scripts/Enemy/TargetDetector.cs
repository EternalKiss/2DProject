using System;
using UnityEngine;

public class TargetDetector : MonoBehaviour
{
    [SerializeField] private LayerMask _targetLayer;

    private Transform _targetPlace;
    private float _detectionDistance = 8f;

    public event Action<Transform> TargetDetected;

    public void Detector()
    {
        bool isFacingRight = Mathf.Approximately(transform.rotation.eulerAngles.y, 0f);
        Vector2 direction = isFacingRight ? Vector2.right : Vector2.left;

        Vector2 rayOrigin = (Vector2)transform.position + new Vector2(0, 1.5f);
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, direction, _detectionDistance, _targetLayer);

        if (hit.collider != null && hit.collider != gameObject)
        {
            _targetPlace = hit.collider.transform;
            TargetDetected?.Invoke(_targetPlace);
        }
        else
        {
            TargetDetected?.Invoke(null);
        }
    }
}
