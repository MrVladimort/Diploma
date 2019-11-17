using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float smoothing = 0.1f;

    public float offset = 0.5f;

    public Vector2 maxPosition;

    public Vector2 minPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (transform.position != target.position)
        {
            var position = transform.position;
            var targetPosition = new Vector3(target.position.x, target.position.y + offset, position.z);

            targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);
            
            position = Vector3.Lerp(position, targetPosition, smoothing);
            transform.position = position;
        }
    }
}