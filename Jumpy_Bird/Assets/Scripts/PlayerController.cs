using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using MText;

public class PlayerController : MonoBehaviour
{
    [Header("Forces")]
    [SerializeField] private float jumpForce = 7.5f;
    [SerializeField] private float fallForce = 100f;
    
    [Header("Particles")]
    [SerializeField] private ParticleSystem jumpVFX;
    
    [Header("Modular3DText")]
    [SerializeField] private Modular3DText startMessage;
    
    [Header("IsDead")]
    public bool isDead = false;
    
    Rigidbody _rigidBody;
    
    bool _isJumped = false;
    
    private void Start()
    {
        StartCoroutine(MessageDelay());
        _rigidBody = GetComponent<Rigidbody>();
    }
    
    IEnumerator MessageDelay()
    {
        yield return new WaitForSecondsRealtime(5f);
        startMessage.UpdateText("Touch The Screen To Start");
    }

    private void Update()
    {
        IsInputBoolean();
    }

    private void IsInputBoolean()
    {
        //checks keyboard(space key) or a mouse click(left click)
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            _isJumped = true;
        }

        //checks touch input
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                _isJumped = true;
            }
        }
    }

    private void FixedUpdate()
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
        if (_isJumped)
        {
            if (EventSystem.current.IsPointerOverGameObject() ||
                EventSystem.current.currentSelectedGameObject != null) { return; }
            Jump();
            _isJumped = false;
            startMessage.gameObject.SetActive(false);
        }
    }

    private void RespondToTouchInput()
    {
        if (_isJumped) 
        {
            if (EventSystem.current.IsPointerOverGameObject() ||
                EventSystem.current.currentSelectedGameObject != null) { return; }
            Jump();
            _isJumped = false;
            startMessage.gameObject.SetActive(false);
        }
    }
    private void Jump()
    {
        AudioManager.instance.Play("Jumping Sound");
        GetComponent<Animator>().SetBool("Fly", true);
        jumpVFX.Play();
        _rigidBody.velocity = Vector3.zero;
        _rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }   

    private void FallSpeed()
    {
        _rigidBody.AddForce(Vector3.down * (fallForce * Time.fixedDeltaTime));
    }

}
