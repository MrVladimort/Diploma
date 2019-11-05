using UnityEngine;
using UnityEngine.Serialization;

public class SpawnEnemyTrigger : MonoBehaviour
{
    [FormerlySerializedAs("Enemy")] public GameObject enemy;
    [FormerlySerializedAs("SpawnPosition")] public Transform spawnPosition;

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Instantiate(enemy, spawnPosition.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
    
}