using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CustomGravity : MonoBehaviour
{
    [SerializeField] private float gravityScale = 1f;

    public static float globalGravity = -9.81f;
    
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.useGravity = false;
    }

    private void FixedUpdate()
    {
        Vector3 gravity = globalGravity * gravityScale * Vector3.up; // -9.81 * 1 * (0, 1, 0) => (0, -9.81, 0)
        _rigidbody.AddForce(gravity, ForceMode.Acceleration);
    }
}
