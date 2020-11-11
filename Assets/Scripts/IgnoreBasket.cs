using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreBasket : MonoBehaviour
{
    internal bool ignore;

    // Start is called before the first frame update
    void Start()
    {
        ignore = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ball"))
            ignore = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ball"))
            ignore = false;
    }
}