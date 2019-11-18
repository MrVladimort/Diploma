using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad;
    public Vector2 playerPosition;
    public VectorValue playerStorage;
    
    private LevelChanger levelChanger;
    
    private void Start()
    {
        levelChanger = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>().GetLevelChanger();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            playerStorage.initialValue = playerPosition;
            levelChanger.FadeToLevel(sceneToLoad);
        }
    }
}
