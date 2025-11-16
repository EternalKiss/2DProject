using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private SpriteRenderer _characterSprite;
    [SerializeField] private Rotater _rotater;
    [SerializeField] private Patroller _patroller;

    private float _stopDistance = 2f;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (_patroller.CalculateDistance() > _stopDistance)
        {
            Vector3 currentTargetPosition = _patroller.GetCurrentWaypointPosition();

            transform.position = Vector2.MoveTowards(
            transform.position,
            currentTargetPosition,
            _moveSpeed * Time.deltaTime);
        }
        else
        {
            Vector3 currentTargetPosition = _patroller.GetCurrentWaypointPosition();
            Vector2 nextDirection = (Vector2)transform.position - (Vector2)currentTargetPosition;

            float directionX = Mathf.Sign(nextDirection.x);

            _patroller.SetNewWayPoint();
            _rotater.Rotate(directionX);
        }
    }
}
