using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PipesMovement : MonoBehaviour
{
    [SerializeField] float movementFactor = 7f;
    Rigidbody rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        PipeMovement();
    }
     
    private void PipeMovement()
    {
        Vector3 currentPosX = new Vector3((transform.position.x - (movementFactor * Time.fixedDeltaTime)), transform.position.y, 0f);
        rigidBody.MovePosition(currentPosX);
    }

}
