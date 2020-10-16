using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dronePlayer : MonoBehaviour
{
    public ProjectileArc ProjectileArc;
    private Rigidbody rigidbody;
    public ScoreKeeper sk;

    /// Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
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

        Vector3 direction = sk.ring.transform.position - transform.position;
        direction = Math3d.ProjectVectorOnPlane(Vector3.up, direction);
        float distance = Vector3.Distance(transform.position, sk.ring.transform.position);
        ProjectileArc.UpdateArc(5f, distance, Physics.gravity.magnitude, 45 * Mathf.Deg2Rad, direction, true);
    }

    private bool forward, backward;

    public void onForwardEnter() => forward = true;

    public void onForwardExit() => forward = false;

    public void onBackwardEnter() => backward = true;

    public void onBackwardExit() => backward = false;
}