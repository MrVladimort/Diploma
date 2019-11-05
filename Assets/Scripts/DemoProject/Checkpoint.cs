using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private GameMaster _gameMaster;

    private void Start()
    {
        _gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _gameMaster.SetCheckPoint(transform.position);
        }
    }
}