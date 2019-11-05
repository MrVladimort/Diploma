using UnityEngine;

public class TrapTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<DemoPlayer>().ApplyTrapDamage();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<DemoPlayer>().ApplyTrapDamage();
        }
    }
}