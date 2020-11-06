using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CannonSceneManager : MonoBehaviour
{
    public GameObject Terrain;
    public Material[] planetMaterials;

    private float[] gravity = {3.7f, 8.87f, 9.8f, 3.7f, 24.79f};

    public Button infoButton;
    public DOTweenAnimation doTweenAnimation;
   
    private bool showingInfo;
    enum Planets
    {
        Mercury = 0,
        Venus = 1,
        Earth = 2,
        Mars = 3,
        Jupiter = 4
    }

    // Start is called before the first frame update
    void Awake()
    {
        var var = PlayerPrefs.GetString("CurrentPlanet", "Mars");
        var ind = (int) Enum.Parse(typeof(Planets), var);
        Terrain.GetComponent<Renderer>().material = planetMaterials[ind];
        Vector3 temp = Physics.gravity;
        temp.y = -gravity[ind];
        Physics.gravity = temp;
    }

    // Update is called once per frame
    void Update()
    {
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
    
    

}