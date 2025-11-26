using System.Collections.Generic;
using UnityEngine;

public class Patroller : MonoBehaviour
{
    [SerializeField] private List<Transform> _wayPoints = new List<Transform>();

    private TargetDetector _targetDetector;
    private int _currentWaypointIndex = 0;

    private void Awake()
    {
        _targetDetector = GetComponent<TargetDetector>();
    }

    private void OnEnable()
    {
        _targetDetector.TargetDetected += DetectNewTarget;
    }

    private void OnDisable()
    {
        _targetDetector.TargetDetected -= DetectNewTarget;
    }

    public void SetNewWayPoint()
    {
        _currentWaypointIndex++;

        if (_currentWaypointIndex >= _wayPoints.Count)
        {
            _currentWaypointIndex = 0;
        }
    }

    public float CalculateDistance(Vector3 targetPosition)
    {
        return Vector2.Distance(transform.position, targetPosition);
    }

    public Vector3 GetCurrentWaypointPosition()
    {
        if (_wayPoints.Count == 0)
        {
            return transform.position;
        }

        return _wayPoints[_currentWaypointIndex].position;
    }

    public void DetectNewTarget(Transform target)
    {
        Enemy enemy = GetComponent<Enemy>();

        if (enemy != null)
        {
            enemy.SetTarget(target);
        }
        else
        {
            enemy.ClearTarget(target);
        }
    }
}
