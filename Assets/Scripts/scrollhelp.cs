using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scrollhelp : MonoBehaviour
{
    public GameObject content;
    float min;
    float max;   
  // Start is called before the first frame update
    void Start()
    {
        min=content.transform.position.x;
        max = min - 2500;
    }

    // Update is called once per frame
   void Update()
    {

        print(content.transform.position);
        if (content.transform.position.x > min)
        {
            Vector3 pos = content.transform.position;
            pos.x = min;
            content.transform.position = pos;
           
        }
        if (content.transform.position.x < max)
        {
            Vector3 pos = content.transform.position;
            pos.x = max;
            content.transform.position = pos;

        }


    }

}
