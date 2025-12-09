using System.Linq.Expressions;
using UnityEngine;

public class Rotater : MonoBehaviour
{
    public bool IsFacingRight { get; private set; } = true;

    public void Rotate(float directionX)
    {
        if (directionX > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            IsFacingRight = true;
        }
        else if (directionX < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            IsFacingRight = false;
        }
    }
}
