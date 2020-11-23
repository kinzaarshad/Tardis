using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CannonSceneManager : MonoBehaviour
{
    public GameObject Terrain;
    public Material[] planetMaterials;

    private float[] gravity = {3.7f, 8.87f, 9.8f, 3.7f, 24.79f};

    public Button infoButton;
    public DOTweenAnimation doTweenAnimation;
    public GameObject TutorialPanel;

    private bool showingInfo;

    public static CannonSceneManager Instance;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
            Instance = this;

        var var = PlayerPrefs.GetString("CurrentPlanet", "Mercury");
        var ind = (int) Enum.Parse(typeof(Planets), var);
        Terrain.GetComponent<Renderer>().material = planetMaterials[ind];
        Vector3 temp = Physics.gravity;
        temp.y = -gravity[ind];
        Physics.gravity = temp;

        showingInfo = false;

        if (PlayerPrefs.GetInt("CannonTutorialShown", 0).Equals(0))
            ShowTutorial();
        AdsManager.ShowingBanner = false;
    }

    private void Update()
    {
        if (!AdsManager.ShowingBanner)
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
        PlayerPrefs.SetInt("CannonTutorialShown", 1);
    }

    public void GoBack()
    {
        AdsManager.ShowVideoAd();
        SceneManager.LoadSceneAsync("Menu");
    }
}