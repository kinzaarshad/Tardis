using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCollisionObject : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ball"))
        {
            Destroy(other.gameObject);
        }
    }
}