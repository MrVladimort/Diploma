using Enemies;
using UnityEngine;
using UnityEngine.Serialization;

public class Weapon : MonoBehaviour
{
    [FormerlySerializedAs("MaxDamage")] public float maxDamage = 50f;
    [FormerlySerializedAs("MinDamage")] public float minDamage = 35f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<DemoEnemy>().ApplyDamage(maxDamage); // TODO
        }
        else if (other.CompareTag("Box"))
        {
            other.GetComponent<Box>().RandomDrop();
        }
    }
}