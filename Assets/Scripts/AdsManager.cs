using System;
using System.Collections;
using System.Collections.Generic;
using EasyMobile;
using UnityEngine;

public class AdsManager : MonoBehaviour
{
    public static bool ShowingBanner;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (!RuntimeManager.IsInitialized())
            RuntimeManager.Init();
    }

    private void Start()
    {
                ShowingBanner = false;
    }

    public static void ShowBanner()
    {
        if (ShowingBanner) return;
        Advertising.ShowBannerAd(BannerAdPosition.Bottom, BannerAdSize.Banner);
        ShowingBanner = true;
    }

    public static void ShowInterstitial()
    {
        if (Advertising.IsInterstitialAdReady())
            Advertising.ShowInterstitialAd();
    }

    public static void ShowRewarded()
    {
        if (Advertising.IsRewardedAdReady())
        {
            Advertising.ShowRewardedAd();
            Advertising.RewardedAdCompleted += OnRewardedAdCompleted;
        }
    }

    public static void ShowVideoAd()
    {
        if (Advertising.IsAdRemoved())
            return;

        if (Advertising.IsRewardedAdReady())
        {
            Advertising.ShowRewardedAd();
        }
    }

    private static void OnRewardedAdCompleted(RewardedAdNetwork network, AdPlacement placement)
    {
//        QuestionsManager.Instance.UnlockLevel(QuestionsManager.Instance.levelNumber + 1);
    }
}