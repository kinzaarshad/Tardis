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
        AdsManager.ShowInterstitial();
    }

    // Update is called once per frame
    void Update()
    {
#if !UNITY_EDITOR
        if (Input.GetKey(KeyCode.Escape))
            LoadLevel(0);
#endif
        AdsManager.ShowInterstitial();
    }

    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);
    }

    public void Quit() =>
#if !UNITY_EDITOR
    Application.Quit();
#elif UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
}