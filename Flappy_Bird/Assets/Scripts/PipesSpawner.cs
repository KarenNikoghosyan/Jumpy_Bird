using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class PipesSpawner : MonoBehaviour
{
    [SerializeField] GameObject pipe;
    [SerializeField] Transform pipesParentTransform;
    [SerializeField] float secondsBetweenSpawns = 2.5f;

    [SerializeField] float minPipeHeight = -8.5f;
    [SerializeField] float maxPipeHeight = 0.5f;

    int numOfPipes = 0;

    public List<MeshRenderer> pipes = new List<MeshRenderer>();

    void Start()
    {
        StartCoroutine(RepeatedlySpawnPipes());
    }

    public void ChangeRandomColor(bool isChangeable)
    {
        if (isChangeable)
        {
            int randomColor = Random.Range(0, 15);
            for (int i=0; i< pipes.Count; i+=2) 
            {
                switch (randomColor)
                {

                    case 0:
                        pipes[i].material.color = Color.blue;
                        pipes[i+1].material.color = Color.blue;
                        break;
                    case 1:
                        pipes[i].material.color = Color.red;
                        pipes[i+1].material.color = Color.red;
                        break;
                    case 2:
                        pipes[i].material.color = Color.green;
                        pipes[i+1].material.color = Color.green;
                        break;
                    case 3:
                        pipes[i].material.color = new Color32(165, 42, 42, 255); // Brown
                        pipes[i+1].material.color = new Color32(165, 42, 42, 255); // Brown
                        break;
                    case 4:
                        pipes[i].material.color = Color.gray;
                        pipes[i+1].material.color = Color.gray;
                        break;
                    case 5:
                        pipes[i].material.color = Color.cyan;
                        pipes[i+1].material.color = Color.cyan;
                        break;
                    case 6:
                        pipes[i].material.color = Color.magenta;
                        pipes[i+1].material.color = Color.magenta;
                        break;
                    case 7:
                        pipes[i].material.color = new Color32(255, 165, 0, 255); // Orange
                        pipes[i+1].material.color = new Color32(255, 165, 0, 255); // Orange
                        break;
                    case 8:
                        pipes[i].material.color = new Color32(128, 0, 128, 255); //Purple
                        pipes[i+1].material.color = new Color32(128, 0, 128, 255); //Purple
                        break;
                    case 9:
                        pipes[i].material.color = new Color32(0, 0, 128, 255); // Navy
                        pipes[i+1].material.color = new Color32(0, 0, 128, 255); // Navy
                        break;
                    case 10:
                        pipes[i].material.color = new Color32(238, 130, 238, 255); // Violet
                        pipes[i+1].material.color = new Color32(238, 130, 238, 255); // Violet
                        break;
                    case 11:
                        pipes[i].material.color = new Color32(64, 224, 208, 255); // Turquoise
                        pipes[i+1].material.color = new Color32(64, 224, 208, 255); // Turquoise
                        break;
                    case 12:
                        pipes[i].material.color = new Color32(245, 245, 220, 255); // Beige
                        pipes[i+1].material.color = new Color32(245, 245, 220, 255); // Beige
                        break;
                    case 13:
                        pipes[i].material.color = Color.yellow;
                        pipes[i+1].material.color = Color.yellow;
                        break;
                    case 14:
                        pipes[i].material.color = new Color32(128, 0, 0, 255); // Maroon
                        pipes[i+1].material.color = new Color32(128, 0, 0, 255); // Maroon
                        break;
                    case 15:
                        pipes[i].material.color = new Color32(70, 130, 180, 255); // Steel blue
                        pipes[i+1].material.color = new Color32(70, 130, 180, 255); // Steel blue
                        break;
                }
            }
            isChangeable = false;
        }
    }

    IEnumerator RepeatedlySpawnPipes()
    {
        while (true)
        {
            float randomYRange = Random.Range(minPipeHeight, maxPipeHeight);
            GameObject newPipe = ObjectPoolManager.CreatePooled(pipe, new Vector3(25f, randomYRange, 0f), Quaternion.identity);
            newPipe.transform.parent = pipesParentTransform;
            AddPipesToList(newPipe);
            numOfPipes++;
            yield return new WaitForSeconds(secondsBetweenSpawns);
        }
    }

    private void AddPipesToList(GameObject newPipe)
    {
        pipes.Add(newPipe.transform.GetChild(0).GetComponent<MeshRenderer>());
        pipes.Add(newPipe.transform.GetChild(1).GetComponent<MeshRenderer>());
    }
}
