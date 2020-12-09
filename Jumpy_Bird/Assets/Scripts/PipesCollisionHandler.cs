using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipesCollisionHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            if (!enabled) { return; }
            ObjectPoolManager.DestroyPooled(gameObject);
        }
    }
}
