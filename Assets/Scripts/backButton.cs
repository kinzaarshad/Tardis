using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class backButton : MonoBehaviour
{
    public Text score;
    public int sceneNumber=1;
    GameObject immortal;
   // Start is called before the first frame update
    void Start()
    {
//        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Application.platform == RuntimePlatform.Android){
            if (Input.GetKey(KeyCode.Escape)){
                SceneManager.LoadScene(sceneNumber);
                }
        }*/
    }
    public void onButtonPress()
    {
        
        SceneManager.LoadScene(0);
    }
}
