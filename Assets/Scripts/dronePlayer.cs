using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dronePlayer : MonoBehaviour
{
    /// Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (forward || Input.GetAxis("Vertical") > 0)
        {
            var delta = transform.forward * 5f;
            transform.position += (delta * Time.fixedDeltaTime);
        }

        if (backward || Input.GetAxis("Vertical") < 0)
        {
            var delta = -transform.forward * 5f;
            transform.position += (delta * Time.fixedDeltaTime);
        }
    }

    private bool forward, backward;

    public void onForwardEnter()
    {
        forward = true;
    }

    public void onForwardExit()
    {
        forward = false;
    }

    public void onBackwardEnter()
    {
        backward = true;
    }

    public void onBackwardExit()
    {
        backward = false;
    }
}