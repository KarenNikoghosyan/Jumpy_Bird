using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float jumpForce = 7.5f;
    [SerializeField] float fallForce = 100f;
    [SerializeField] ParticleSystem jumpVFX;
    Rigidbody rigidBody;

    public bool isDead = false;
    bool isJumped = false;
    
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        IsInputBoolean();
    }

    private void IsInputBoolean()
    {
        //checks keyboard(space key) or a mouse click(left click)
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            isJumped = true;
        }

        //checks touch input
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                isJumped = true;
            }
        }
    }

    void FixedUpdate()
    {
        if (Time.timeScale == 0) { return; }
        if (!isDead)
        {
            RespondToJumpInput();
            RespondToTouchInput();
            FallSpeed();
        }
        
    }

    private void RespondToJumpInput()
    {
        if (isJumped)
        {

        if (EventSystem.current.IsPointerOverGameObject() ||
            EventSystem.current.currentSelectedGameObject != null) { return; }
            Jump();
            isJumped = false;
        }
    }

    private void RespondToTouchInput()
    {
        if (isJumped) 
        {
        if (EventSystem.current.IsPointerOverGameObject() ||
            EventSystem.current.currentSelectedGameObject != null) { return; }
            Jump();
            isJumped = false;
        }
    }
    private void Jump()
    {
        AudioManager.instance.Play("Jumping Sound");
        GetComponent<Animator>().SetBool("Fly", true);
        jumpVFX.Play();
        rigidBody.velocity = Vector3.zero;
        rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }   

    private void FallSpeed()
    {
        rigidBody.AddForce(Vector3.down * (fallForce * Time.fixedDeltaTime));
    }

}
