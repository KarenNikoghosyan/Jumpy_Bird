using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipesMovement : MonoBehaviour
{
    [SerializeField] float movementFactor = 20f;
    Rigidbody rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        PipeMovement();
    }

    private void PipeMovement()
    {
        Vector3 currentPosX = new Vector3((transform.position.x - (movementFactor * Time.deltaTime)), transform.position.y, -0);
        rigidBody.MovePosition(currentPosX);
    }
}
