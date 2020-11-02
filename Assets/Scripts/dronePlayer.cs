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


        Vector3 direction = transform.position;

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit,
            LayerMask.GetMask("Ground")))
        {
            direction = hit.point - transform.position;
        }

        direction = Math3d.ProjectVectorOnPlane(Vector3.up, direction);
        float distance = direction.magnitude;

        ProjectileArc.transform.position = transform.position;

        float angle = Quaternion.Angle(Quaternion.LookRotation(transform.forward, Vector3.up), Quaternion.identity);

        ProjectileArc.UpdateArc(ballLauncher.ballSpeed, distance, Physics.gravity.magnitude, angle * Mathf.Deg2Rad,
            direction, true);
    }

/*    
    void FixedUpdate()
    {
        Vector3 last_pos = transform.position;
        Vector3 velocity = transform.forward * 20;
        ProjectileArc.GetComponent<LineRenderer>().SetVertexCount(1);
        ProjectileArc.GetComponent<LineRenderer>().SetPosition(0, last_pos);
        int i = 1;
        while (i < 200)
        {
            velocity += Physics.gravity * Time.fixedDeltaTime;
            RaycastHit hit;
            if (Physics.Linecast(last_pos, (last_pos + (velocity * Time.fixedDeltaTime)), out hit))
            {
                velocity = Vector3.Reflect(velocity * 1f, hit.normal);
                last_pos = hit.point;
            }

            ProjectileArc.GetComponent<LineRenderer>().SetVertexCount(i + 1);
            ProjectileArc.GetComponent<LineRenderer>().SetPosition(i, last_pos);
            last_pos += velocity * Time.fixedDeltaTime;
            i++;
        }
    }
*/

    private bool forward, backward;

    public void onForwardEnter() => forward = true;

    public void onForwardExit() => forward = false;

    public void onBackwardEnter() => backward = true;

    public void onBackwardExit() => backward = false;
}