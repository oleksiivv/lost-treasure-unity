using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class BaseUI : MonoBehaviour
{
    public GameObject loadingPanel;
    
    public void openScene(int id){
        Time.timeScale=1;
        LastSavedPositionController.lastSavedPos=Vector3.zero;
        loadingPanel.SetActive(true);
        Application.LoadLevel(id);
    }

#if UNITY_IOS
    private string appId = "ca-app-pub-4962234576866611~7807718464";
    private string bannerId="ca-app-pub-4962234576866611/4259688833";
#else
    private string appId = "ca-app-pub-4962234576866611~2451698824";
    private string bannerId="ca-app-pub-4962234576866611/9839152333";
#endif

    private BannerView _bannerView;
    void Start(){
        RequestConfiguration requestConfiguration =
            new RequestConfiguration.Builder()
            .SetSameAppKeyEnabled(true).build();
        MobileAds.SetRequestConfiguration(requestConfiguration);

        MobileAds.Initialize(initStatus => {
          //CreateBannerView();
          //LoadBannerAd();
        });
    }

    //baner
    public void CreateBannerView()
    {
        Debug.Log("Creating banner view");

        // If we already have a banner, destroy the old one.
        if (_bannerView != null)
        {
            DestroyBannerView();
        }

        // Create a 320x50 banner at top of the screen
        _bannerView = new BannerView(bannerId, AdSize.Banner, AdPosition.Top);
    }

    public void LoadBannerAd()
    {
        // create an instance of a banner view first.
        if(_bannerView == null)
        {
            CreateBannerView();
        }

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        Debug.Log("Loading banner ad.");
        _bannerView.LoadAd(adRequest);
    }

    public void DestroyBannerView()
    {
        if (_bannerView != null)
        {
            Debug.Log("Destroying banner view.");
            _bannerView.Destroy();
            _bannerView = null;
        }
    }
}
