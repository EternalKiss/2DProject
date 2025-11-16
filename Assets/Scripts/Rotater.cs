using UnityEngine;

public class Rotater : MonoBehaviour
{
    public void Rotate(float directionX)
    {
        if (directionX > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (directionX < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
