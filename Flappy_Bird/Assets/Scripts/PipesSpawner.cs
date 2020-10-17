using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class PipesSpawner : MonoBehaviour
{
    [SerializeField] GameObject pipes;
    [SerializeField] Transform pipesParentTransform;
    [SerializeField] float secondsBetweenSpawns = 2f;

    public float minPipeHeight = -8.5f;
    public float maxPipeHeight = 0.5f;

    int numOfPipes = 0;

    void Start()
    {
        StartCoroutine(RepeatedlySpawnPipes());  
    }

    IEnumerator RepeatedlySpawnPipes()
    {
        while (numOfPipes < 5)
        {
            float randomYRange = Random.Range(minPipeHeight, maxPipeHeight);
            GameObject newPipe = Instantiate(pipes, new Vector3(28f, randomYRange, 0f), Quaternion.identity);
            newPipe.transform.parent = pipesParentTransform;
            numOfPipes++;
            yield return new WaitForSeconds(secondsBetweenSpawns);
        }
    }
}
