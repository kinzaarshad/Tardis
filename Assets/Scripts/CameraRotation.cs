using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class CameraRotation : MonoBehaviour
{
    Vector3 FirstPoint;
    Vector3 SecondPoint;
    float xAngle;
    float yAngle;

    private Vector2 touch0StartPosition;
    private float touch0StartTime;
    private Vector2 touch0LastPosition;
    private bool isTouching;
    private Camera cam;


    /// <summary> Called as soon as the player touches the screen. The argument is the screen position. </summary>
    public event Action<Vector2> onStartTouch;

    /// <summary> Called as soon as the player stops touching the screen. The argument is the screen position. </summary>
    public event Action<Vector2> onEndTouch;

    /// <summary> Called if the player completed a quick tap motion. The argument is the screen position. </summary>
    public event Action<Vector2> onTap;

    /// <summary> Called if the player swiped the screen. The argument is the screen movement delta. </summary>
    public event Action<Vector2> onSwipe;

    /// <summary> Called if the player pinched the screen. The arguments are the distance between the fingers before and after. </summary>
    public event Action<float, float> onPinch;

    // Use this for initialization
    void Start()
    {
        xAngle = 0;
        yAngle = 0;
        transform.rotation = Quaternion.Euler(yAngle, xAngle, 0);
        cam = Camera.main;
        
        // id = -1 means the finger is not being tracked
        leftFingerId = -1;
        rightFingerId = -1;

        // only calculate once
        halfScreenWidth = Screen.width / 2;

        // calculate the movement input dead zone
//        moveInputDeadZone = Mathf.Pow(Screen.height / moveInputDeadZone, 2);
    }

    // Update is called once per frame
    void Update()
    {
        GetTouchInput();
        
        if (rightFingerId != -1) {
            // Ony look around if the right finger is being tracked
//            Debug.Log("Rotating");
            LookAround();
        }
        //float rotationSpeed = 0.5f;
        //float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        // float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

        //transform.localRotation = Quaternion.Euler(0, mouseX, 0) * transform.localRotation;
        //Camera camera = GetComponentInChildren<Camera>();
        //camera.transform.localRotation = Quaternion.Euler(-mouseY, 0, 0) * camera.transform.localRotation;
    }

    private void LateUpdate()
    {
        
//#if UNITY_EDITOR
//        xAngle += Input.GetAxis("Mouse X") * 20f;
//#elif !UNITY_EDITOR
//            xAngle += Input.GetTouch(0).position.x;
//#endif
//
//#if UNITY_EDITOR
//        yAngle -= Input.GetAxis("Mouse Y") * 20f;
//#elif !UNITY_EDITOR
//            yAngle += Input.GetTouch(0).position.y;
//#endif
//
//        transform.rotation = Quaternion.Euler(yAngle, xAngle, 0.0f);

//        int touchCount = Input.touches.Length;
//
//        if (touchCount == 1)
//        {
//            Touch touch = Input.GetTouch(0);
//
//            switch (touch.phase)
//            {
//                case TouchPhase.Began:
//                {
//                    if (!IsPointerOverUIObject())
//                    {
//                        touch0StartPosition = touch.position;
//                        touch0StartTime = Time.time;
//                        touch0LastPosition = touch0StartPosition;
//
//                        isTouching = true;
//
//                        if (onStartTouch != null) onStartTouch(touch0StartPosition);
//                    }
//
//                    break;
//                }
//
//                case TouchPhase.Moved:
//                {
//                    touch0LastPosition = touch.position;
//
//                    if (touch.deltaPosition != Vector2.zero && isTouching)
//                    {
//                        OnSwipe(touch.deltaPosition);
//                    }
//
//                    break;
//                }
//
//                case TouchPhase.Ended:
//                {
//                    if (Time.time - touch0StartTime <= 0.4f
//                        && Vector2.Distance(touch.position, touch0StartPosition) <= 40f
//                        && isTouching)
//                    {
//                        OnClick(touch.position);
//                    }
//
//                    if (onEndTouch != null) onEndTouch(touch.position);
//                    isTouching = false;
//                    break;
//                }
//
//                case TouchPhase.Stationary:
//                case TouchPhase.Canceled:
//                    break;
//            }
//        }
//        else
//        {
//            if (isTouching)
//            {
//                if (onEndTouch != null) onEndTouch(touch0LastPosition);
//                isTouching = false;
//            }
//        }
    }

//    void OnClick(Vector2 position)
//    {
//        if (onTap != null && (!IsPointerOverUIObject()))
//        {
//            onTap(position);
//        }
//    }
//
//    void OnSwipe(Vector2 deltaPosition)
//    {
//        if (onSwipe != null)
//        {
//            onSwipe(deltaPosition);
//        }
//
//        transform.position -= (cam.ScreenToWorldPoint(deltaPosition) - cam.ScreenToWorldPoint(Vector2.zero));
//    }

    int leftFingerId, rightFingerId;
    float halfScreenWidth;
    Vector2 lookInput;
    float cameraPitch;
    Vector2 moveTouchStartPosition;

    void GetTouchInput()
    {
        // Iterate through all the detected touches
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch t = Input.GetTouch(i);

            // Check each touch's phase
            switch (t.phase)
            {
                case TouchPhase.Began:

                    if (t.position.x < halfScreenWidth && leftFingerId == -1)
                    {
                        // Start tracking the left finger if it was not previously being tracked
                        leftFingerId = t.fingerId;

                        // Set the start position for the movement control finger
                        moveTouchStartPosition = t.position;
                    }
                    else if (t.position.x > halfScreenWidth && rightFingerId == -1)
                    {
                        // Start tracking the rightfinger if it was not previously being tracked
                        rightFingerId = t.fingerId;
                    }

                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:

                    if (t.fingerId == leftFingerId)
                    {
                        // Stop tracking the left finger
                        leftFingerId = -1;
                        Debug.Log("Stopped tracking left finger");
                    }
                    else if (t.fingerId == rightFingerId)
                    {
                        // Stop tracking the right finger
                        rightFingerId = -1;
                        Debug.Log("Stopped tracking right finger");
                    }

                    break;
                case TouchPhase.Moved:

                    // Get input for looking around
                    if (t.fingerId == rightFingerId)
                    {
                        lookInput = t.deltaPosition * 10 * Time.deltaTime;
                    }
                    else if (t.fingerId == leftFingerId)
                    {
                        // calculating the position delta from the start position
//                        moveInput = t.position - moveTouchStartPosition;
                    }

                    break;
                case TouchPhase.Stationary:
                    // Set the look input to zero if the finger is still
                    if (t.fingerId == rightFingerId)
                    {
                        lookInput = Vector2.zero;
                    }

                    break;
            }
        }
    }

    void LookAround()
    {
        // vertical (pitch) rotation
        cameraPitch = Mathf.Clamp(cameraPitch - lookInput.y, -90f, 90f);
        transform.localRotation = Quaternion.Euler(cameraPitch, 0, 0);

        // horizontal (yaw) rotation
        transform.Rotate(transform.up, lookInput.x);
    }

    public bool IsPointerOverUIObject()
    {
        if (EventSystem.current == null) return false;
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
#if UNITY_EDITOR
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
#elif !UNITY_EDITOR
        eventDataCurrentPosition.position = new Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
#endif
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}