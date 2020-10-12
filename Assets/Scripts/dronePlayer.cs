using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dronePlayer : MonoBehaviour
{
    public GameObject player;

    //public GameObject camera;
    int value;
    bool connected = false;

    /// Start is called before the first frame update
    void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void onForward()
    {
        var sideSpeed = 10;
        Vector3 deltaPosition = transform.forward * sideSpeed;
        player.transform.Translate(deltaPosition * Time.fixedDeltaTime);
    }

    public void onStop()
    {
        var sideSpeed = 10;
        Vector3 deltaPosition = transform.forward * sideSpeed;
        player.transform.Translate(-deltaPosition * Time.fixedDeltaTime);
    }
}