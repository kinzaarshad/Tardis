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


/*        float currentAngle = 45;

        Vector3 velocity = transform.rotation * Vector3.forward * ballLauncher.ballSpeed;
        Vector3 point = transform.position + velocity;
        print(point);
        Vector3 direction = point - transform.position;
        float yOffset = -direction.y;
        direction = Math3d.ProjectVectorOnPlane(Vector3.up, direction);
        float distance = direction.magnitude;

        float angle0, angle1;
        bool targetInRange =
            ProjectileMath.LaunchAngle(ballLauncher.ballSpeed, distance, yOffset, Physics.gravity.magnitude, out angle0,
                out angle1);


        currentAngle = angle1;

        ProjectileArc.transform.position = transform.position;
        ProjectileArc.UpdateArc(ballLauncher.ballSpeed, distance, Physics.gravity.magnitude,
            currentAngle * Mathf.Deg2Rad,
            direction, true);*/
    }

    private bool forward, backward;

    public void onForwardEnter() => forward = true;

    public void onForwardExit() => forward = false;

    public void onBackwardEnter() => backward = true;

    public void onBackwardExit() => backward = false;
}