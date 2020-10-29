using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipesCollisionHandler : MonoBehaviour
{
    List<GameObject> pipes;
    void Start()
    {
        pipes = FindObjectOfType<PipesSpawner>().pipes;    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Finish")
        {
            if (!enabled) { return; }
            RemovePipesFromList();
            ObjectPoolManager.DestroyPooled(gameObject);
        }
    }

    private void RemovePipesFromList()
    {
        pipes.Remove(pipes[1]);
        pipes.Remove(pipes[0]);
    }
}
