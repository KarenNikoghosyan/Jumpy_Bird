using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CloudCollisionHandler : MonoBehaviour
{
    [SerializeField] private ParticleSystem waterSplashVFX;
   
    private Rigidbody cloudRigidbody;
    private bool isKinematic = true;

    public static bool isFirstTouch = false;
    
    private void Start()
    {
        isFirstTouch = false;
        cloudRigidbody = GetComponent<Rigidbody>();
        cloudRigidbody.isKinematic = true;
    }

    private void Update()
    {
        if (!isKinematic) return;
        
        IsInputTouch();
        IsInputKbMouse();
        
        if (!isKinematic)
        {
            cloudRigidbody.isKinematic = false;
        }
    }

    private void IsInputTouch()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if (EventSystem.current.IsPointerOverGameObject() ||
                    EventSystem.current.currentSelectedGameObject != null) { return; }
                
                isFirstTouch = true;
                isKinematic = false;
            }
        }
    }

    private void IsInputKbMouse()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject() ||
                EventSystem.current.currentSelectedGameObject != null) { return; }
            
            isFirstTouch = true;
            isKinematic = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            AudioManager.instance.Play("Water Splash Sound");
            waterSplashVFX.Play();
            Invoke("DestroyOnDelay", 2f);
        }
    }
    
    private void DestroyOnDelay()
    {
        Destroy(gameObject);
    }
}
