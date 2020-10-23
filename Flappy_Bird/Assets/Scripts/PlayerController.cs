using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float jumpForce = 7f;
    Rigidbody rigidBody;
    
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        RespondToJumpInput();
        RespondToTouchInput();
    }

    private void RespondToJumpInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }
    private void RespondToTouchInput()
    {
        if (Input.touchCount > 0) 
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Jump();
            }
        }
    }

    private void Jump()
    {
        rigidBody.velocity = Vector3.zero;
        rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }   
}
