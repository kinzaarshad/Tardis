using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkyCamera : MonoBehaviour
{
    [SerializeField] float incline = 45;

    [SerializeField] float minDistance = 10;

    [SerializeField] float maxDistance = 50;

    [SerializeField] float speed = 10;

    [SerializeField] float acceleration = 50;

    [SerializeField] float decceleration = 70;

    [SerializeField] float zoomSensitivity = 1;

    private Vector3 velocity;
    private Vector3 position;

    private float currentDistance;


    void Awake()
    {
        velocity = Vector3.zero;

        currentDistance = (minDistance + maxDistance) * 0.5f;

        transform.rotation = Quaternion.AngleAxis(incline, Vector3.right);
    }

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject() || IsPointerOverUIObject()) return;

        float zoomInput = -Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity;

        currentDistance = Mathf.Clamp(currentDistance + zoomInput, minDistance, maxDistance);

        Vector3 moveInput = new Vector3(Input.GetAxis("Mouse X"), 0, Input.GetAxis("Mouse Y"));

        if (moveInput != Vector3.zero)
            velocity = Vector3.MoveTowards(velocity, moveInput.normalized * speed, acceleration * Time.deltaTime);
        else
            velocity = Vector3.MoveTowards(velocity, Vector3.zero, decceleration * Time.deltaTime);


        Vector3 temp = position + velocity;
        temp.x = Mathf.Clamp(temp.x, -50f, 50f);
        temp.z = Mathf.Clamp(temp.z, -50f, 50f);
        position = temp;

        transform.position = -transform.forward * currentDistance + position;
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
        return results.Count > 0 || EventSystem.current.currentSelectedGameObject != null;
    }
}