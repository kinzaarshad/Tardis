using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BasketballSceneManager : MonoBehaviour
{
    public Material[] planetMaterials;

    private float[] gravity = {10.44f, 8.87f, 11.15f, 0.62f, 1.62f};


    public Button infoButton;
    public DOTweenAnimation doTweenAnimation;
    public GameObject TutorialPanel;

    private bool showingInfo;

    // Start is called before the first frame update
    void Awake()
    {
        var var = PlayerPrefs.GetString("CurrentPlanet", "Saturn");
        var ind = (int) Enum.Parse(typeof(Planets), var);
        ind -= 5;
        RenderSettings.skybox = planetMaterials[ind];
        Vector3 temp = Physics.gravity;
        temp.y = -gravity[ind];
        Physics.gravity = temp;

        showingInfo = false;

        if (PlayerPrefs.GetInt("BasketballTutorialShown", 0).Equals(0))
            ShowTutorial();
        
        AdsManager.ShowBanner();
    }

    public void ShowInfo()
    {
        doTweenAnimation.DOPlayForward();
        infoButton.onClick.RemoveAllListeners();
        infoButton.onClick.AddListener(HideInfo);
    }

    public void HideInfo()
    {
        doTweenAnimation.DOPlayBackwards();
        infoButton.onClick.RemoveAllListeners();
        infoButton.onClick.AddListener(ShowInfo);
    }

    void ShowTutorial() => TutorialPanel.SetActive(true);
    
    public void HideTutorial()
    {
        Destroy(TutorialPanel);
//        PlayerPrefs.SetInt("BasketballTutorialShown", 1);
    }

    public void GoBack()
    {
        AdsManager.ShowRewarded();
        SceneManager.LoadSceneAsync("Menu");
    }
    
}