using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PotionTrigger : MonoBehaviour
{
    [FormerlySerializedAs("Points")] public float points = 40f;
    private bool _isPickedUp = false;


    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !_isPickedUp)
        {
            _isPickedUp = true;
            other.gameObject.GetComponent<DemoPlayer>().ApplyHealing(points);
            Destroy(gameObject);
        }
    }

    public void SetPoints(float points)
    {
        points = points;
    }
}
