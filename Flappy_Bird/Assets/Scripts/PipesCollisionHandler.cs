using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipesCollisionHandler : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Finish")
        {
            if (!enabled) { return; }
            ObjectPoolManager.DestroyPooled(gameObject);
        }
    }
}
