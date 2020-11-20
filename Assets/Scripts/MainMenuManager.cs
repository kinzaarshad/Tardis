using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AdsManager.ShowBanner();
    }

    // Update is called once per frame
    void Update()
    {
//#if !UNITY_EDITOR
//        if (Input.GetKey(KeyCode.Escape))
//            LoadLevel(0);
//#endif
    }

    public void LoadLevel(int level)
    {
        var planet = (Planets) level;
        PlayerPrefs.SetString("CurrentPlanet", planet.ToString());
        AdsManager.ShowInterstitial();
        if (level < 5)
            SceneManager.LoadScene(2);
        else
            SceneManager.LoadScene(3);
    }

    public void Quit() =>
#if !UNITY_EDITOR
    Application.Quit();
#elif UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
}