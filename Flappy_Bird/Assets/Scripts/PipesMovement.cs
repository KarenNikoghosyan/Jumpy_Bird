using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PipesMovement : MonoBehaviour
{
    [SerializeField] float movementFactor = 7f;
    Rigidbody rigidBody;

    List<GameObject> pipes;

    void Awake()
    {
    }

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        pipes = FindObjectOfType<PipesSpawner>().pipes;
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
