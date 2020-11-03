using System.Collections.Generic;
using UnityEngine;

public class ProjectileArc : MonoBehaviour
{
    [SerializeField] int iterations = 20;

    [SerializeField] Color errorColor;

    private Color initialColor;
    private LineRenderer lineRenderer;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        initialColor = lineRenderer.material.color;
    }

    public void UpdateArc(float speed, float distance, float gravity, float angle, Vector3 direction, bool valid)
    {
        Vector2[] arcPoints = ProjectileMath.ProjectileArcPoints(iterations, speed, distance, gravity, angle);
        List<Vector3> points3d = new List<Vector3>();

        for (int i = 0; i < arcPoints.Length; i++)
        {
            Vector3 point = new Vector3(0, arcPoints[i].y, arcPoints[i].x);
            /*Vector3 nextPoint = Vector3.zero;
            if ((i + 1) < arcPoints.Length)
                nextPoint = new Vector3(0, arcPoints[i + 1].y, arcPoints[i + 1].x);

            Ray ray = new Ray(point, nextPoint - point);
            if (!Physics.Raycast(ray, float.MaxValue, LayerMask.GetMask("Ground")))*/
                points3d.Add(point);
        }

        lineRenderer.positionCount = points3d.Count;
        lineRenderer.SetPositions(points3d.ToArray());

        transform.rotation = Quaternion.LookRotation(direction);

        lineRenderer.material.color = valid ? initialColor : errorColor;
    }

    public void UpdateArcBounce(float speed, float distance, float gravity, float angle, Vector3 direction, bool valid)
    {
        Vector2[] arcPoints = ProjectileMath.ProjectileArcPoints(iterations, speed, distance, gravity, angle);
        List<Vector3> points3d = new List<Vector3>();

        for (int i = 0; i < arcPoints.Length; i++)
        {
            Vector3 point = new Vector3(0, arcPoints[i].y, arcPoints[i].x);
            Vector3 nextPoint = Vector3.zero;
            if ((i + 1) < arcPoints.Length)
                nextPoint = new Vector3(0, arcPoints[i + 1].y, arcPoints[i + 1].x);
//            if (!Physics.Linecast(point, nextPoint))
//                points3d.Add(point);
            if (!Physics.SphereCast(point, Vector3.Distance(point, nextPoint), nextPoint - point, out RaycastHit hit))
                points3d.Add(point);
        }

        lineRenderer.positionCount = points3d.Count;
        lineRenderer.SetPositions(points3d.ToArray());

        transform.rotation = Quaternion.LookRotation(direction);

        lineRenderer.material.color = valid ? initialColor : errorColor;
    }
}