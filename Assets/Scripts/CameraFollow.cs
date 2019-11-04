using UnityEngine;
using UnityEngine.Serialization;

public class CameraFollow : MonoBehaviour
{
    [FormerlySerializedAs("Speed")] public float speed = 15f;
    [FormerlySerializedAs("InterpVelocity")] public float interpVelocity;
    [FormerlySerializedAs("Target")] public GameObject target;
    [FormerlySerializedAs("Offset")] public Vector3 offset;
    
    Vector3 _targetPos;

    // Use this for initialization
    void Start()
    {
        _targetPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            var cameraPosition = transform.position;
            var playerPosition = target.transform.position;
            
            Vector3 posNoZ = cameraPosition;
            posNoZ.z = playerPosition.z;

            Vector3 targetDirection = playerPosition - posNoZ;

            interpVelocity = targetDirection.magnitude * speed;

            _targetPos = cameraPosition + Time.deltaTime * interpVelocity * targetDirection.normalized;

            cameraPosition = Vector3.Lerp(cameraPosition, _targetPos + offset, 0.25f);
            
            transform.position = cameraPosition;
        }
    }
}