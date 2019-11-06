using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObject : MonoBehaviour
{
    protected Vector3 direction = new Vector3(-1, 0, 0);
    private bool _isFacingRight;
    
    protected void Flip(Vector2 move)
    {
        if (move.x < -0.01f && !_isFacingRight) FlipGameObject();
        else if (move.x > 0.01f && _isFacingRight) FlipGameObject();
    }

    protected void FlipGameObject()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 mirrorScale = gameObject.transform.localScale;
        mirrorScale.x *= -1;
        gameObject.transform.localScale = mirrorScale;
    }
    
    protected void ChangeDirection()
    {
        direction *= -1;
    }

    public static bool IsNotSameDirection(float a, float b)
    {
        return !(a < 0 && b < 0 || a > 0 && b > 0);
    }
}
