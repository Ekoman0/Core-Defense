using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using GoogleMobileAds.Api;
public class UnityAds : MonoBehaviour
{
    string _adUnitId = "ca-app-pub-3138384441939461/3899523421";
    private InterstitialAd interstitialAd;
    private void Start()
    {
        
        MobileAds.Initialize(initStatus => { });
        //this.RequestInterstitial();
        LoadInterstitialAd();
    }
  
    public void ShowInterstitial()
    {
        if (interstitialAd != null && interstitialAd.CanShowAd())
        {
            
            interstitialAd.Show();
        }
        else
        {
           
        }
    }
    public void LoadInterstitialAd()
    {
        // Clean up the old ad before loading a new one.
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
            interstitialAd = null;
        }

      
        
        var adRequest = new AdRequest.Builder().Build();

        
        InterstitialAd.Load(_adUnitId, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
              // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    
                    return;
                }
          

                interstitialAd = ad;
                ShowInterstitial();
            });
    }
}
