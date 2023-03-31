using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using System;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Settings;

public class RewardAd : MonoBehaviour
{
    string sceneName;
    public GameObject shop;
    public TextMeshProUGUI MoneyTMP;
    public TextMeshProUGUI CheckTMP;
    public TextMeshProUGUI CheckSkinTMP;
    float money;
    string adUnitID;
    string adUnitIDExtraLife;
    RewardedAd odullureklam;
    RewardedAd skinodullureklam;
    RewardedAd canodullureklam;

    private LocalizeStringEvent localizedStringEvent;
    // Start is called before the first frame update

    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        adUnitID = "ca-app-pub-3138384441939461/6525686769";
        adUnitIDExtraLife = "ca-app-pub-3138384441939461/4186511619";
        if (sceneName == "EndGame")
        {
            
                if (PlayerPrefs.GetInt("CanHakki") == 1)
                {
                    this.canodullureklam = new RewardedAd(adUnitIDExtraLife);

                    this.canodullureklam.OnUserEarnedReward += canver;
                    this.canodullureklam.OnAdLoaded += canadloaded;

                    AdRequest request3 = new AdRequest.Builder().Build();

                    this.canodullureklam.LoadAd(request3);
                    
                }
                else
                {
                PlayerPrefs.SetInt("CanHakki", 1);
                CheckTMP.text = " ";
                }
            
            

        }
        else
        {
            this.odullureklam = new RewardedAd(adUnitID);
            this.skinodullureklam = new RewardedAd(adUnitID);

            // Called when an ad request has successfully loaded.
            this.odullureklam.OnAdLoaded += HandleRewardedAdLoaded;
            this.odullureklam.OnUserEarnedReward += oyuncuyaodulver;
            this.odullureklam.OnAdClosed += HandleRewardedAdClosed;

            this.skinodullureklam.OnUserEarnedReward += skinver;
            this.skinodullureklam.OnAdLoaded += skinadloaded;


            AdRequest request = new AdRequest.Builder().Build();
            AdRequest request2 = new AdRequest.Builder().Build();

            this.odullureklam.LoadAd(request);
            this.skinodullureklam.LoadAd(request2);
        }
            
        


    }

    private void canadloaded(object sender, EventArgs e)
    {
        CheckTMP.text = LocalizationSettings.StringDatabase.GetLocalizedString("Shop", "Reklam Ýzle");
        
        
        
    }

    private void canver(object sender, Reward e)
    {
        PlayerPrefs.SetInt("CanHakki", 0);
        SceneManager.LoadScene(6);
    }

    private void Update()
    {
        if (PlayerPrefs.HasKey("SkinReward"))
        {
            float a = PlayerPrefs.GetFloat("SkinReward");
            if ( a == 1)
            {
                if (shop !=null)
                {
                    shop.GetComponent<Shop>().Player13Buy();
                }
               

                PlayerPrefs.SetFloat("SkinReward", 0);
            }
        }
    }

    private void skinadloaded(object sender, EventArgs e)
    {
        CheckSkinTMP.text = LocalizationSettings.StringDatabase.GetLocalizedString("Shop", "Reklam Ýzle");
    }

    private void skinver(object sender, Reward e)
    {
        PlayerPrefs.SetFloat("SkinReward", 1);
        //shop.GetComponent<Shop>().Player13Buy();
    }

    public void UserChoseToWatchAd()
    {
        if (this.odullureklam.IsLoaded())
        {
            this.odullureklam.Show();
        }
    }
    public void UserChoseToWatchAdSkin()
    {
        if (this.skinodullureklam.IsLoaded())
        {
            this.skinodullureklam.Show();
        }
    }
    public void UserChoseToWatchExtraHealth()
    {
        if (PlayerPrefs.GetInt("CanHakki")==1)
        {
            if (this.canodullureklam.IsLoaded())
            {
                this.canodullureklam.Show();
                
            }
        }
       
    }

    private void oyuncuyaodulver(object sender, Reward e)
    {
       
            money = PlayerPrefs.GetFloat("Money");
            money += 20;

            MoneyTMP.text = money.ToString();
            PlayerPrefs.SetFloat("Money", money);


    }
    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        if (CheckTMP != null)
        {
            
            CheckTMP.text =LocalizationSettings.StringDatabase.GetLocalizedString("Shop", "Reklam Ýzle");
        }
        
    }

  

 



    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        if (CheckTMP != null)
        {
            CheckTMP.text = " ";
        }
        
    }
}
