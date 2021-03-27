using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using MText;

public class CloudCollisionHandler : MonoBehaviour
{
    [SerializeField] private ParticleSystem waterSplashVFX;
    [SerializeField] private Modular3DText startMessage;
    
    private Rigidbody cloudRigidbody;
    private bool isKinematic = true;

    public static bool isFirstTouch = false;

    private void Start()
    {
        StartCoroutine(MessageDelay());
        isFirstTouch = false;
        cloudRigidbody = GetComponent<Rigidbody>();
        cloudRigidbody.isKinematic = true;
    }

    IEnumerator MessageDelay()
    {
        yield return new WaitForSecondsRealtime(5f);
        startMessage.UpdateText("Touch The Screen To Start");
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
                startMessage.gameObject.SetActive(false);
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
            startMessage.gameObject.SetActive(false);
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
