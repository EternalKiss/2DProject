using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private List<Transform> _targets = new List<Transform>();
    [SerializeField] private SpriteRenderer _characterSprite;

    private float _stopDistance = 2f;
    private int currentWaypointIndex = 0;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Transform currentTarget = _targets[currentWaypointIndex];
        Vector2 toTarget = currentTarget.position - transform.position;
        float distance = toTarget.magnitude;

        if (distance > _stopDistance)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                currentTarget.position,
                _moveSpeed * Time.deltaTime);
        }
        else
        {
            SetNewTarget();
            Turn();
        }
    }

    private void SetNewTarget()
    {
        currentWaypointIndex++;

        if (currentWaypointIndex >= _targets.Count)
        {
            currentWaypointIndex = 0;
        }
    }

    private void Turn()
    {
        if (_characterSprite != null)
        {
           _characterSprite.flipX = !_characterSprite.flipX;
        }
    }
}
