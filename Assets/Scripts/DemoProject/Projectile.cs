using Enemies;
using UnityEngine;
using UnityEngine.Serialization;

public class Projectile : MonoBehaviour
{
    [FormerlySerializedAs("Speed")] public float speed = 10f;
    [FormerlySerializedAs("Damage")] public float damage = 30f;

    private bool _redirected = false;
    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void SetTarget(Transform target)
    {
        var rb = GetComponent<Rigidbody2D>();
        rb.velocity = (target.position - rb.transform.position).normalized * speed;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Player":
                if (!_redirected)
                {
                    other.gameObject.GetComponent<DemoPlayer>().ApplyDamage(damage);
                    Destroy(gameObject);
                }
                break;
            case "PlayerWeapon":
                if (!_redirected)
                {
                    _rb.velocity *= -1;
                    _redirected = true;
                }
                break;
            case "Enemy":
                if (_redirected)
                {
                    other.gameObject.GetComponent<DemoEnemy>().ApplyDamage(damage);
                    Destroy(gameObject);
                }
                break;
            case "Ground":
                Destroy(gameObject);
                break;
        }
    }
}