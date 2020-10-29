using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PipesMovement : MonoBehaviour
{
    [SerializeField] float movementFactor = 4f;
    [SerializeField] float movementSpeed = 0.1f; // todo remove later
    Rigidbody rigidBody;

    List<GameObject> pipes;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        pipes = FindObjectOfType<PipesSpawner>().pipes;
    }

    void FixedUpdate()
    {
        PipeMovement();
    }

    public void SpeedUpMovement(bool isValid)
    {
        if (isValid)
        {
            for (int i = 0; i < pipes.Count; i++)
            {
                pipes[i].GetComponent<PipesMovement>().movementFactor += movementSpeed;
            }
            isValid = false;
        }
    }
     
    private void PipeMovement()
    {
        Vector3 currentPosX = new Vector3((transform.position.x - (movementFactor * Time.fixedDeltaTime)), transform.position.y, 0f);
        rigidBody.MovePosition(currentPosX);
    }

}
