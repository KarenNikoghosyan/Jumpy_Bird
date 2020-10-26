using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PipesMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1f; // todo remove later
    Rigidbody rigidBody;
    [SerializeField] float movementFactor = 4f;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        //movementFactor = FindObjectOfType<PipesSpawner>().movementFactor;
    }

    void FixedUpdate()
    {
        PipeMovement();
    }

    public void SpeedUpMovement(bool isValid2)
    {
        if (isValid2)
        {
            movementFactor += movementSpeed;
            isValid2 = false; 
        }
    }
     
    private void PipeMovement()
    {
        Vector3 currentPosX = new Vector3((transform.position.x - (movementFactor * Time.fixedDeltaTime)), transform.position.y, 0f);
        rigidBody.MovePosition(currentPosX);
    }

}
