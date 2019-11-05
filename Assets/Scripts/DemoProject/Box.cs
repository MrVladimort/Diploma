using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private Animator _animator;

    public Transform spawnPosition;
    public GameObject potion;
    
    // Start is called before the first frame update
    void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
    }
    
    // Update is called once per frame

    public void RandomDrop()
    {
        _animator.SetTrigger("Destroyed");
        Invoke("SpawRandom", 0.3f);
    }

    private void SpawRandom()
    {
        int number = Random.Range(1, 4);
        
        if (number == 1)
            Instantiate(potion, spawnPosition.position, Quaternion.identity);
        
        Destroy(gameObject);
    }
}
