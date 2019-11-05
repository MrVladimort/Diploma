using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MoveablePlatform : MonoBehaviour
{
    [FormerlySerializedAs("TargetPosition")]
    public Transform targetPosition;

    [FormerlySerializedAs("Speed")] public float speed;

    private bool _moveToTarget = true;
    private Vector3 _initPosition;
    private Vector3 _targetPosition;
    private Vector3 _direction;
    private Rigidbody2D _rb;

    // Start is called before the first frame update
    void Start()
    {
        _initPosition = transform.position;
        _targetPosition = targetPosition.position;
        _direction = (_targetPosition - _initPosition).normalized;
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_moveToTarget)
        {
            if (Vector3.Distance(_targetPosition, transform.position) < 0.1f) ChangeDirection();
        }
        else
        {
            if (Vector3.Distance(_initPosition, transform.position) < 0.1f) ChangeDirection();
        }

        _rb.velocity = _direction * speed;
    }

    private void ChangeDirection()
    {
        _moveToTarget = !_moveToTarget;
        _direction *= -1;
    }
}