using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;


public class Ads : MonoBehaviour
{
    private InterstitialAd _interstitialAd;

    private RewardedAd _rewardedAd;

    private string _interstitialAdID;

    private string _rewardedAdID;
    // Start is called before the first frame update
    void Start()
    {
        _interstitialAdID = "ca-app-pub-3940256099942544/1033173712";
        _rewardedAdID = "ca-app-pub-3940256099942544/5224354917";
        
        MobileAds.Initialize(InitializationStatus=>{});
        RequestInterstitial();
        RequestRewarded();
    }
    
    private void RequestInterstitial()
    {
        _interstitialAd = new InterstitialAd(_interstitialAdID);
        _interstitialAd.OnAdLoaded += HandleOnAdLoaded;
        AdRequest request = new AdRequest.Builder().Build();
        _interstitialAd.LoadAd(request);
    }
    
    private void RequestRewarded()
    {
        _rewardedAd = new RewardedAd(_interstitialAdID);
        _rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        _rewardedAd.OnAdClosed += HandleRewardedAdClosed;
        _rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        AdRequest request = new AdRequest.Builder().Build();
        _rewardedAd.LoadAd(request);
    }

  

    public void ShowInterstitial()
    {
        if (_interstitialAd.IsLoaded())
        {
            _interstitialAd.Show();
            RequestInterstitial();
        }
    }

    public void ShowRewarded()
    {
        if (_rewardedAd.IsLoaded())
        {
            _rewardedAd.Show();
            RequestRewarded();
        }
    }
    
    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
    }
    
    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        RequestRewarded();
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        RequestRewarded();
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        RequestRewarded();
    }


}
