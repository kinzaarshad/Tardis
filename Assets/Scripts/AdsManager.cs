using System;
using System.Collections;
using System.Collections.Generic;
using EasyMobile;
using UnityEngine;

public class AdsManager : MonoBehaviour
{
    private void Awake()
    {
        if (!RuntimeManager.IsInitialized())
            RuntimeManager.Init();
    }

    public static void ShowBanner()
    {
        Advertising.ShowBannerAd(BannerAdPosition.Bottom, BannerAdSize.Leaderboard);
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

    private static void OnRewardedAdCompleted(RewardedAdNetwork network, AdPlacement placement)
    {
//        QuestionsManager.Instance.UnlockLevel(QuestionsManager.Instance.levelNumber + 1);
    }
}