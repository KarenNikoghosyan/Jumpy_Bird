using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class PipesSpawner : MonoBehaviour
{
    [SerializeField] GameObject pipes;
    [SerializeField] Transform pipesParentTransform;
    [SerializeField] float secondsBetweenSpawns = 2.5f;

    [SerializeField] float minPipeHeight = -8.5f;
    [SerializeField] float maxPipeHeight = 0.5f;

    int numOfPipes = 0;

    void Start()
    {
        StartCoroutine(RepeatedlySpawnPipes());
    }

    IEnumerator RepeatedlySpawnPipes()
    {
        while (true)
        {
            float randomYRange = Random.Range(minPipeHeight, maxPipeHeight);
            GameObject newPipe = ObjectPoolManager.CreatePooled(pipes, new Vector3(28f, randomYRange, 0f), Quaternion.identity);
            newPipe.transform.parent = pipesParentTransform;
            numOfPipes++;
            print(numOfPipes);
            yield return new WaitForSeconds(secondsBetweenSpawns);
        }
    }
}
