using UnityEngine;

public class PlanetMouseOrbit : MonoBehaviour
{
    Transform target;
    float distance = 10.0f;
    float xSpeed = 250.0f;
    float ySpeed = 120.0f;
    float yMinLimit = -20;
    float yMaxLimit = 80;
    float zoomRate = 25;

    private float x = 0.0f;
    private float y = 0.0f;

    [AddComponentMenu("Camera-Control/Mouse Orbit")]
    void Start()
    {
        var angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
    }

    void Update()
    {
        if (target)
        {
            x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

            distance += -(Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime) * zoomRate * Mathf.Abs(distance);


            y = ClampAngle(y, yMinLimit, yMaxLimit);

            var rotation = Quaternion.Euler(y, x, 0);
            var position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;

            transform.rotation = rotation;
            transform.position = position;
        }
    }

    static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }
}