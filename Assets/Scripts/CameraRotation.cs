using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class CameraRotation : MonoBehaviour
{
    Vector3 FirstPoint;
    Vector3 SecondPoint;
    float xAngle;
    float yAngle;

    private bool clicked;

    // Use this for initialization
    void Start()
    {
        xAngle = 0;
        yAngle = 0;
        transform.rotation = Quaternion.Euler(yAngle, xAngle, 0);
        clicked = false;
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0) && !clicked)
#elif !UNITY_EDITOR
        if (Input.GetTouch(0).phase == TouchPhase.Began)
#endif
        {
            clicked = true;
        }

#if UNITY_EDITOR
        if (Input.GetMouseButtonUp(0))
#elif !UNITY_EDITOR
        if (Input.GetTouch(0).phase == TouchPhase.Ended)
#endif
        {
            clicked = false;
        }

        if (clicked  && !TouchingUI())
        {
#if UNITY_EDITOR
            xAngle += Input.GetAxis("Mouse X") * 20f;
#elif !UNITY_EDITOR
            xAngle += Input.GetTouch(0).position.x;
#endif

#if UNITY_EDITOR
            yAngle -= Input.GetAxis("Mouse Y") * 20f;
#elif !UNITY_EDITOR
            yAngle += Input.GetTouch(0).position.y;
#endif

            transform.rotation = Quaternion.Euler(yAngle, xAngle, 0.0f);
        }
        //float rotationSpeed = 0.5f;
        //float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        // float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

        //transform.localRotation = Quaternion.Euler(0, mouseX, 0) * transform.localRotation;
        //Camera camera = GetComponentInChildren<Camera>();
        //camera.transform.localRotation = Quaternion.Euler(-mouseY, 0, 0) * camera.transform.localRotation;
    }
    
        bool TouchingUI() => EventSystem.current.IsPointerOverGameObject();

}