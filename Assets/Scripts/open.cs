using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class open : MonoBehaviour
{
   
    public int scene;
    public void OnButtonPress()
    {
        //Screen.orientation = ScreenOrientation.LandscapeLeft;
        SceneManager.LoadScene(scene);
    }


}