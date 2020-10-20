using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameRate : MonoBehaviour
{
    [Range(30,120)]
    [SerializeField] int framerate;

    void Awake()
    {
        //Application.targetFrameRate = framerate;
    }
}
