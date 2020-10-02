﻿using UnityEngine;
using System.Collections;

public class BallLauncher : MonoBehaviour {

    public GameObject ballPreFab;
    public float ballSpeed = 6.0f;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

       
        //float rotationSpeed = 0.5f;
        //float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        //float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

        //transform.localRotation = Quaternion.Euler(0, mouseX, 0) * transform.localRotation;
        //Camera camera = GetComponentInChildren<Camera>();
        //camera.transform.localRotation = Quaternion.Euler(-mouseY, 0, 0) * camera.transform.localRotation;
    }

    public void launch()
    {
        GameObject instance = Instantiate(ballPreFab);
        instance.transform.position = transform.position;
        Rigidbody rb = instance.GetComponent<Rigidbody>();
        Camera camera = GetComponentInChildren<Camera>();
        rb.velocity = camera.transform.rotation * Vector3.forward * ballSpeed;
    }
}
