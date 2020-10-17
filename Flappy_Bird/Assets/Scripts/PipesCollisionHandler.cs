using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipesCollisionHandler : MonoBehaviour
{
    float minPipeHeight;
    float maxPipeHeight;

    void Start()
    {
        minPipeHeight = FindObjectOfType<PipesSpawner>().minPipeHeight;
        maxPipeHeight = FindObjectOfType<PipesSpawner>().maxPipeHeight;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Finish")
        {
            float randomYRange = Random.Range(minPipeHeight, maxPipeHeight);
            transform.position = new Vector3(28f, randomYRange, 0);
        }
    }
}
