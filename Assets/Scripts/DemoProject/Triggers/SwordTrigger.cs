using UnityEngine;

public class SwordTrigger : MonoBehaviour
{
    private GameMaster _gameMaster;

    private void Start()
    {
        _gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();

//        if (_gameMaster.haveSword) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<DemoPlayer>().TakeSword();
        }
        
        Destroy(gameObject);
    }
}
