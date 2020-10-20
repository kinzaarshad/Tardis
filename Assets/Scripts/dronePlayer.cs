using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dronePlayer : MonoBehaviour
{
    public ProjectileArc ProjectileArc;
    private Rigidbody rigidbody;
    public ScoreKeeper sk;
    private BallLauncher ballLauncher;

    /// Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        ballLauncher = GetComponent<BallLauncher>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
            ballLauncher.launch();

        if (forward || Input.GetAxis("Vertical") > 0)
        {
            var delta = transform.forward * 5f;
            rigidbody.AddForce(delta);
        }

        else if (backward || Input.GetAxis("Vertical") < 0)
        {
            var delta = -transform.forward * 5f;
            rigidbody.AddForce(delta);
        }
        else
        {
            rigidbody.Sleep();
        }


        Vector3 direction = transform.forward;
//        Ray ray = Camera.main.ScreenPointToRay(direction);

//        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue /*, LayerMask.GetMask("Ground")*/))
        if (Physics.Raycast(transform.position, transform.forward,
            out RaycastHit hit /*, LayerMask.GetMask("Ground")*/))
            direction = hit.point - transform.position;
/*
        float yOffset = -direction.y;
        direction = Math3d.ProjectVectorOnPlane(Vector3.up, direction);
        float distance = direction.magnitude;

        currentSpeed = ProjectileMath.LaunchSpeed(distance, yOffset, Physics.gravity.magnitude, angle * Mathf.Deg2Rad);
        TimeOfFlight = currentTimeOfFlight;
        ProjectileArc.UpdateArc(20f, distance, Physics.gravity.magnitude, currentAngle * Mathf.Deg2Rad, direction, true);
        
        Vector3 direction = transform.position - sk.ring.transform.position;
*/

        direction = Math3d.ProjectVectorOnPlane(Vector3.up, direction);
//        float distance = Vector3.Distance(transform.position, sk.ring.transform.position);
        float distance = direction.magnitude;
        ProjectileArc.transform.position = transform.position + (transform.forward * 5f);
        ProjectileArc.UpdateArc(ballLauncher.ballSpeed, distance, Physics.gravity.magnitude,
            Quaternion.Angle(Quaternion.LookRotation(transform.forward, Vector3.up), Quaternion.identity) *
            Mathf.Deg2Rad, direction, true);
    }

    private bool forward, backward;

    public void onForwardEnter() => forward = true;

    public void onForwardExit() => forward = false;

    public void onBackwardEnter() => backward = true;

    public void onBackwardExit() => backward = false;
}