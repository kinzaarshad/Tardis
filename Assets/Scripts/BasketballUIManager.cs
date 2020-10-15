using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BasketballUIManager : MonoBehaviour
{
    public Button infoButton;
    public DOTweenAnimation doTweenAnimation;

    private bool showingInfo;

    // Start is called before the first frame update
    void Start()
    {
        showingInfo = false;
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