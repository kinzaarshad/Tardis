using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class helpScript : MonoBehaviour
{
    public GameObject placeholder;
    public int number;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void buttonPress()
    {
        if (number == 1)
        {
            placeholder.GetComponent<Text>().text="think left and right to move the spaceship"; 
        }
        if (number == 2)
        {
            placeholder.GetComponent<Text>().text = "think left and right to move the cyborg";
        }
        if (number == 3)
        {
            placeholder.GetComponent<Text>().text = "think left and right to move the gun and push to shoot";
        }
        if (number == 4)
        {
            placeholder.GetComponent<Text>().text = "think push to move forward and neutral to stop";
        }
        if (number == 5)
        {
            placeholder.GetComponent<Text>().text = "think left,right,push and pull to move the ball ";
        }
        if (number == 6)
        {
            placeholder.GetComponent<Text>().text = "think push,lift,drop and neutral to move the drone";
        }
        if (number == 7)
        {
            placeholder.GetComponent<Text>().text = "think push to move forward and neutral to stop";
        }
        if (number == 8)
        {
            placeholder.GetComponent<Text>().text = "think push, pull, left and right to move the car";
        }
        if (number == 9)
        {
            placeholder.GetComponent<Text>().text = "think lift, drop and netral to control the player.think push to shoot";
        }
        if (number == 10)
        {
            placeholder.GetComponent<Text>().text = "think push to grab the ball and push again to shoot";
        }
    }
}
