using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondTrigger : MonoBehaviour
{
    public float Points = 100f;
    private GameMaster _gameMaster;
    private bool _isPickedUp = false;

    private void Start()
    {
        _gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !_isPickedUp)
        {
            _isPickedUp = true;
            _gameMaster.AddDiamondPoints(Points);
            Destroy(gameObject);
        }
    }
    
    public void SetPoints(float points)
    {
        Points = points;
    }
}
