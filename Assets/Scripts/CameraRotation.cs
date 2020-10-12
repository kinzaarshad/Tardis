using UnityEngine;
using System.Collections;

public class CameraRotation : MonoBehaviour
{
    Vector3 FirstPoint;
    Vector3 SecondPoint;
    float xAngle;
    float yAngle;
    float xAngleTemp;
    float yAngleTemp;

    // Use this for initialization
    void Start()
    {
        xAngle = 0;
        yAngle = 0;
        this.transform.rotation = Quaternion.Euler(yAngle, xAngle, 0);
    }

    // Update is called once per frame
    void Update()
    {
#if !UNITY_EDITOR
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                FirstPoint = Input.GetTouch(0).position;
                xAngleTemp = xAngle;
                yAngleTemp = yAngle;
            }

            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                SecondPoint = Input.GetTouch(0).position;
                xAngle = xAngleTemp + (SecondPoint.x - FirstPoint.x) * 180 / Screen.width;
                yAngle = yAngleTemp + (SecondPoint.y - FirstPoint.y) * 90 / Screen.height;
                this.transform.rotation = Quaternion.Euler(yAngle, xAngle, 0.0f);
            }
        }

#elif UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            FirstPoint = Input.mousePosition;
            xAngleTemp = xAngle;
            yAngleTemp = yAngle;
        }

        if (Input.GetMouseButtonUp(0))
        {
            SecondPoint = Input.mousePosition;
            xAngle = xAngleTemp + (SecondPoint.x - FirstPoint.x) * 180 / Screen.width;
            yAngle = yAngleTemp + (SecondPoint.y - FirstPoint.y) * 90 / Screen.height;
            this.transform.rotation = Quaternion.Euler(yAngle, xAngle, 0.0f);
        }

#endif
        //float rotationSpeed = 0.5f;
        //float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        // float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

        //transform.localRotation = Quaternion.Euler(0, mouseX, 0) * transform.localRotation;
        //Camera camera = GetComponentInChildren<Camera>();
        //camera.transform.localRotation = Quaternion.Euler(-mouseY, 0, 0) * camera.transform.localRotation;
    }
}