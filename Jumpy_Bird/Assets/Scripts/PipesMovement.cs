using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PipesMovement : MonoBehaviour
{
    //points to a single memory location which affects all the pipes
    public static float movementFactor = 7f;
    Rigidbody rigidBody;

    public static bool Stop = false;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (Stop) return;
        PipeMovement();
    }

    public void PipeMovement(bool stop = false)
    {
        if (stop) 
        {
            Stop = true;
            return;  
        }
        Vector3 currentPosX = new Vector3((transform.position.x - (movementFactor * Time.fixedDeltaTime)), transform.position.y, 0f);
        rigidBody.MovePosition(currentPosX);
    }

}
