using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;
using System;

public class KnightUI : MonoBehaviour
{
    public GameObject pausePanel, diePanel,winPanel,winUI;
    public GameObject loadingPanel;

    public GameObject additionalLifePanel;
    public Image loadingAdditionalLifePanel;
    public Text resultText;

    public Image[] hearts;

#if UNITY_IOS
    private string appId = "ca-app-pub-4962234576866611~7807718464";
    private string rewardedId = "ca-app-pub-4962234576866611/2363820094";
#else
    private string appId = "ca-app-pub-4962234576866611~2451698824";
    private string rewardedId = "ca-app-pub-4962234576866611/8362419131";
#endif

    private BannerView _bannerView;

    void Start(){
        loadingPanel.SetActive(false);

        loadingAdditionalLifePanel=loadingAdditionalLifePanel.GetComponent<Image>();

        if(LastSavedPositionController.lastSavedPos!=Vector3.zero){
            gameObject.transform.position = LastSavedPositionController.lastSavedPos;
        }

        RequestConfiguration requestConfiguration =
            new RequestConfiguration.Builder()
            .SetSameAppKeyEnabled(true).build();
        MobileAds.SetRequestConfiguration(requestConfiguration);

        MobileAds.Initialize(initStatus => {
          InitAdmobRewarded();
        });
    }

    public static int addCnt=0;
    public void pause(){
        Time.timeScale=0;
        pausePanel.SetActive(true);
    }

    public void resume(){
        Time.timeScale=1;
        pausePanel.SetActive(false);
    }

    public void openScene(int id){
        Time.timeScale=1;
        LastSavedPositionController.lastSavedPos=Vector3.zero;
        loadingPanel.SetActive(true);
        Application.LoadLevel(id);
    }

    public void restart(){
        Time.timeScale=1;
        LastSavedPositionController.lastSavedPos=Vector3.zero;
        openScene(Application.loadedLevel);
    }


    public void showAdditionalLifePanel(){
        additionalLifePanel.SetActive(true);
        StartCoroutine(showingAdditionalLifePanel());
    }

    public void skipAdditionalLifePanel(){
        additionalLifePanel.SetActive(false);
        diePanel.SetActive(true);
        resumed=true;
        LastSavedPositionController.lastSavedPos=Vector3.zero;

        if(PlayerPrefs.GetInt("KeysAt"+Application.loadedLevel.ToString())!=4){
                PlayerPrefs.SetInt("KeysAt"+Application.loadedLevel.ToString(),0);  
                PlayerPrefs.SetInt("Keykey@"+Application.loadedLevel.ToString(),0);
                PlayerPrefs.SetInt("Keykey (1)@"+Application.loadedLevel.ToString(),0);
                PlayerPrefs.SetInt("Keykey (2)@"+Application.loadedLevel.ToString(),0);
        }
    }

    private bool resumed=false;
    private int loadingSpeed=1;
    IEnumerator showingAdditionalLifePanel(){
        while(loadingAdditionalLifePanel.fillAmount!=1){
            loadingAdditionalLifePanel.fillAmount+=0.0025f*loadingSpeed;
            yield return new WaitForSeconds(0.01f);
        }

        if(!resumed){
            additionalLifePanel.SetActive(false);
            diePanel.SetActive(true);
            LastSavedPositionController.lastSavedPos=Vector3.zero;
            if(PlayerPrefs.GetInt("KeysAt"+Application.loadedLevel.ToString())!=4){
              PlayerPrefs.SetInt("KeysAt"+Application.loadedLevel.ToString(),0);  
            }
        }
    }

    public void resumeByAd(){
        ShowRewardBasedVideo();
    }

    private RewardedAd _rewardedAd;
    void InitAdmobRewarded(){
      if (_rewardedAd != null)
      {
            _rewardedAd.Destroy();
            _rewardedAd = null;
      }

      Debug.Log("Loading the rewarded ad.");

      // create our request used to load the ad.
      var adRequest = new AdRequest();

      // send the request to load the ad.
      RewardedAd.Load(rewardedId, adRequest,
          (RewardedAd ad, LoadAdError error) =>
          {
              // if error is not null, the load request failed.
              if (error != null || ad == null)
              {
                  return;
              }
              _rewardedAd = ad;
              RegisterReloadHandler(_rewardedAd);
              RegisterEventHandlers(_rewardedAd);
          });
    }

    private bool ShowRewardBasedGoogleVideo()
    {
      const string rewardMsg =
        "Rewarded ad rewarded the user";

        if (_rewardedAd != null && _rewardedAd.CanShowAd())
        {
            loadingSpeed=0;
            _rewardedAd.Show((Reward reward) =>
            {
              Success();
            });

            return true;
        }

        return false;
    }

    private void RegisterEventHandlers(RewardedAd ad)
    {
      // Raised when the ad is estimated to have earned money.
      ad.OnAdPaid += (AdValue adValue) =>
      {
          Success();
      };
      // Raised when an impression is recorded for an ad.
      ad.OnAdImpressionRecorded += () =>
      {
          Debug.Log("Rewarded ad recorded an impression.");
      };
      // Raised when a click is recorded for an ad.
      ad.OnAdClicked += () =>
      {
          Debug.Log("Rewarded ad was clicked.");
      };
      // Raised when an ad opened full screen content.
      ad.OnAdFullScreenContentOpened += () =>
      {
          Debug.Log("Rewarded ad full screen content opened.");
      };
      // Raised when the ad closed full screen content.
      ad.OnAdFullScreenContentClosed += () =>
      {
          Debug.Log("Rewarded ad full screen content closed.");
      };
      // Raised when the ad failed to open full screen content.
      ad.OnAdFullScreenContentFailed += (AdError error) =>
      {
        loadingSpeed=1;
        if(PlayerPrefs.GetInt("KeysAt"+Application.loadedLevel.ToString())!=4){
            PlayerPrefs.SetInt("KeysAt"+Application.loadedLevel.ToString(),0);  
            PlayerPrefs.SetInt("Keykey@"+Application.loadedLevel.ToString(),0);
            PlayerPrefs.SetInt("Keykey (1)@"+Application.loadedLevel.ToString(),0);
            PlayerPrefs.SetInt("Keykey (2)@"+Application.loadedLevel.ToString(),0);
        }
      };
  }

  private void RegisterReloadHandler(RewardedAd ad)
  {
      // Raised when the ad closed full screen content.
      ad.OnAdFullScreenContentClosed += () =>
      {
          Debug.Log("Rewarded Ad full screen content closed.");
          // Reload the ad so that we can show another as soon as possible.
          InitAdmobRewarded();
      };
      // Raised when the ad failed to open full screen content.
      ad.OnAdFullScreenContentFailed += (AdError error) =>
      {
          Debug.LogError("Rewarded ad failed to open full screen content " +
                        "with error : " + error);
          // Reload the ad so that we can show another as soon as possible.
          InitAdmobRewarded();
      };
  }

    private bool ShowRewardBasedVideo()
    {
        return ShowRewardBasedGoogleVideo();
    }

    public void Success(){
        loadingSpeed=0;
        Debug.Log("The ad was successfully shown.");
        resumed=true;
        additionalLifePanel.SetActive(false);
        loadingPanel.SetActive(true);
        Application.LoadLevel(Application.loadedLevel);
        KnightHealthSystem.addCnt=1;
    }
}

