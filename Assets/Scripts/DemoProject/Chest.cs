using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Chest : MonoBehaviour
{
    private bool _showDialog = false;
    private bool _opened = false;
    private Animator _animator;
    private BoxCollider2D _collider; 

    [FormerlySerializedAs("Item")] public GameObject item;
    [FormerlySerializedAs("Text")] public GameObject text;
    [FormerlySerializedAs("SpawnPosition")]public Transform spawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<BoxCollider2D>();
        text.SetActive(false);
    }

    // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !_opened)
        {
            text.SetActive(true);
            _showDialog = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            text.SetActive(false);
            _showDialog = false;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetButtonDown("Action") && !_opened)
            {
                _opened = true;
                text.SetActive(false);
                _collider.enabled = false;
                _animator.SetTrigger("Open");
                Invoke("Spawn", 0.2f);
            }
        }
    }

    private void Spawn()
    {
        Instantiate(item, spawnPosition.position, Quaternion.identity);
    }
}