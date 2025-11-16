using System.Collections.Generic;
using UnityEngine;

public class Patroller : MonoBehaviour
{
    [SerializeField] private List<Transform> _wayPoints = new List<Transform>();

    private int _currentWaypointIndex = 0;

    public void SetNewWayPoint()
    {
        _currentWaypointIndex++;

        if (_currentWaypointIndex >= _wayPoints.Count)
        {
            _currentWaypointIndex = 0;
        }
    }

    public float CalculateDistance()
    {
        Transform currentTarget = _wayPoints[_currentWaypointIndex];
        Vector2 toTarget = currentTarget.position - transform.position;
        return toTarget.magnitude;
    }

    public Vector3 GetCurrentWaypointPosition()
    {
        return _wayPoints[_currentWaypointIndex].position;
    }
}
