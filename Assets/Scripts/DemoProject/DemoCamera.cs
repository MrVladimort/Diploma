using UnityEngine;
using UnityEngine.Serialization;

public class DemoCamera : MonoBehaviour
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
    void FixedUpdate()
    {
        if (target)
        {
            Vector3 posNoZ = transform.position;
            posNoZ.z = target.transform.position.z;

            Vector3 targetDirection = target.transform.position - posNoZ;

            interpVelocity = targetDirection.magnitude * speed;

            _targetPos = transform.position + targetDirection.normalized * interpVelocity * Time.deltaTime;

            transform.position = Vector3.Lerp(transform.position, _targetPos + offset, 0.25f);
        }
    }
}