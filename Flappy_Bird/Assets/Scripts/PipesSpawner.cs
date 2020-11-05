using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;


public class PipesSpawner : MonoBehaviour
{

    public Material material;

    [SerializeField] float secondsBetweenSpawns = 2f;

    [SerializeField] GameObject pipePrefab;
    [SerializeField] Transform pipesParentTransform;

    [SerializeField] float minPipeHeight = -8.8f;
    [SerializeField] float maxPipeHeight = 1f;

    TextMeshProUGUI pipeScore;
    int numOfPipes = 0;
    
    void Start()
    {
        pipeScore = FindObjectOfType<PlayerCollisionHandler>().pipeScore;
        StartCoroutine(RepeatedlySpawnPipes());
    }

    public void SetRandomColor(bool isChangeable)
    {
        if (isChangeable)
        {
            int randomColor = Random.Range(0, 20);
                switch (randomColor)
                {
                    case 0:
                        material.color = Color.blue;
                        pipeScore.color = Color.blue;
                    break;
                    case 1:
                        material.color = Color.red;
                        pipeScore.color = Color.red;
                    break;
                    case 2:
                        material.color = Color.green;
                        pipeScore.color = Color.green;
                    break;
                    case 3:
                        material.color = new Color32(165, 42, 42, 255); // Brown
                        pipeScore.color = new Color32(165, 42, 42, 255); // Brown
                    break;
                    case 4:
                        material.color = Color.gray;
                        pipeScore.color = Color.gray;
                    break;
                    case 5:
                        material.color = Color.cyan;
                        pipeScore.color = Color.cyan;
                    break;
                    case 6:
                        material.color = Color.magenta;
                        pipeScore.color = Color.magenta;
                    break;
                    case 7:
                        material.color = new Color32(255, 165, 0, 255); // Orange
                        pipeScore.color = new Color32(255, 165, 0, 255); // Orange
                    break;
                    case 8:
                        material.color = new Color32(128, 0, 128, 255); //Purple
                        pipeScore.color = new Color32(128, 0, 128, 255); //Purple
                    break;
                    case 9:
                        material.color = new Color32(0, 0, 128, 255); // Navy
                        pipeScore.color = new Color32(0, 0, 128, 255); // Navy
                    break;
                    case 10:
                        material.color = new Color32(238, 130, 238, 255); // Violet
                        pipeScore.color = new Color32(238, 130, 238, 255); // Violet
                    break;
                    case 11:
                        material.color = new Color32(64, 224, 208, 255); // Turquoise
                        pipeScore.color = new Color32(64, 224, 208, 255); // Turquoise
                    break;
                    case 12:
                        material.color = new Color32(245, 245, 220, 255); // Beige
                        pipeScore.color = new Color32(245, 245, 220, 255); // Beige
                    break;
                    case 13:
                        material.color = Color.yellow;
                        pipeScore.color = Color.yellow;
                    break;
                    case 14:
                        material.color = new Color32(128, 0, 0, 255); // Maroon
                        pipeScore.color = new Color32(128, 0, 0, 255); // Maroon
                    break;
                    case 15:
                        material.color = new Color32(70, 130, 180, 255); // Steel blue
                        pipeScore.color = new Color32(70, 130, 180, 255); // Steel blue
                    break;
                    case 16:
                        material.color = new Color32(205, 92, 92, 255); // Indian red
                        pipeScore.color = new Color32(205, 92, 92, 255); // Indian red
                    break;
                    case 17:
                        material.color = new Color32(245, 245, 245, 255); // White smoke
                        pipeScore.color = new Color32(245, 245, 245, 255); // White smoke
                    break;
                    case 18:
                        material.color = Color.black;
                        pipeScore.color = Color.black;
                    break;
                    case 19:
                        material.color = new Color32(218, 165, 32, 255); // Golden rod
                        pipeScore.color = new Color32(218, 165, 32, 255); // Golden rod
                    break;
                    case 20:
                        material.color = new Color32(128, 128, 0, 255); // Olive
                        material.color = new Color32(128, 128, 0, 255); // Olive
                    break;
            }
                isChangeable = false;
        }
    }

    IEnumerator RepeatedlySpawnPipes()
    {
        while (true)
        {
            GameObject newPipe = PipeSpawner();
            numOfPipes++;
            yield return new WaitForSeconds(secondsBetweenSpawns);
        }
    }

    private GameObject PipeSpawner()
    {
        float randomYRange = Random.Range(minPipeHeight, maxPipeHeight);
        GameObject newPipe = ObjectPoolManager.CreatePooled(pipePrefab, new Vector3(29.5f, randomYRange, 0f), Quaternion.identity);
        newPipe.transform.parent = pipesParentTransform;
        return newPipe;
    }

}
