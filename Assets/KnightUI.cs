using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.Advertisements;
//using GoogleMobileAds.Api;
using System;
using Yodo1.MAS;

public class KnightUI : MonoBehaviour
{
    public GameObject pausePanel, diePanel,winPanel,winUI;
    public GameObject loadingPanel;

    public GameObject additionalLifePanel;
    public Image loadingAdditionalLifePanel;
    public Text resultText;

    public Image[] hearts;
    private string gameID="4155550";

    private string appId="ca-app-pub-4962234576866611~2451698824";

    void Start(){
        loadingPanel.SetActive(false);
        //Advertisement.Initialize(gameID,false);

        loadingAdditionalLifePanel=loadingAdditionalLifePanel.GetComponent<Image>();

        if(LastSavedPositionController.lastSavedPos!=Vector3.zero){
            gameObject.transform.position = LastSavedPositionController.lastSavedPos;
        }

        //MobileAds.Initialize(appId);
        //InitAdmob();

        InitializeSdk();
        SetPrivacy(true, false, false);
        SetDelegates();
    }

    public static int addCnt=0;
    public void pause(){
        Time.timeScale=0;
        pausePanel.SetActive(true);

        // if(Yodo1U3dMas.IsInterstitialAdLoaded() && addCnt%2==1){
        //     Yodo1U3dMas.ShowInterstitialAd();
        // }
        // addCnt++;
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

        // if(Yodo1U3dMas.IsInterstitialAdLoaded() && addCnt%2==1){
        //     Yodo1U3dMas.ShowInterstitialAd();
        // }
        // addCnt++;
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
        //resumed=true;
        /*if (Advertisement.IsReady("rewardedVideo"))
        {
            loadingSpeed=0;
              var options = new ShowOptions { resultCallback = HandleShowResult };
              Advertisement.Show("rewardedVideo", options);
        }
         else{
             
             ShowRewardBasedVideo();
         }*/

         if(Yodo1U3dMas.IsRewardedAdLoaded()){
                loadingSpeed=0;
                Yodo1U3dMas.ShowRewardedAd();
            }

        
    }

    private void SetPrivacy(bool gdpr, bool coppa, bool ccpa)
    {
        Yodo1U3dMas.SetGDPR(gdpr);
        Yodo1U3dMas.SetCOPPA(coppa);
        Yodo1U3dMas.SetCCPA(ccpa);
    }

    private void InitializeSdk()
    {
        Yodo1U3dMas.InitializeSdk();
    }

    private void SetDelegates()
    {
        Yodo1U3dMas.SetInitializeDelegate((bool success, Yodo1U3dAdError error) =>
        {
            Debug.Log("[Yodo1 Mas] InitializeDelegate, success:" + success + ", error: \n" + error.ToString());

            if (success)
            {
                //StartCoroutine(BannerCoroutine());
            }
            else
            {

            }
        });

        Yodo1U3dMas.SetBannerAdDelegate((Yodo1U3dAdEvent adEvent, Yodo1U3dAdError error) =>
        {
            Debug.Log("[Yodo1 Mas] BannerdDelegate:" + adEvent.ToString() + "\n" + error.ToString());
            switch (adEvent)
            {
                case Yodo1U3dAdEvent.AdClosed:
                    Debug.Log("[Yodo1 Mas] Banner ad has been closed.");
                    break;
                case Yodo1U3dAdEvent.AdOpened:
                    Debug.Log("[Yodo1 Mas] Banner ad has been shown.");
                    break;
                case Yodo1U3dAdEvent.AdError:
                    Debug.Log("[Yodo1 Mas] Banner ad error, " + error.ToString());
                    break;
            }
        });

        Yodo1U3dMas.SetInterstitialAdDelegate((Yodo1U3dAdEvent adEvent, Yodo1U3dAdError error) =>
        {
            Debug.Log("[Yodo1 Mas] InterstitialAdDelegate:" + adEvent.ToString() + "\n" + error.ToString());
            switch (adEvent)
            {
                case Yodo1U3dAdEvent.AdClosed:
                    Debug.Log("[Yodo1 Mas] Interstital ad has been closed.");
                    break;
                case Yodo1U3dAdEvent.AdOpened:
                    Debug.Log("[Yodo1 Mas] Interstital ad has been shown.");
                    break;
                case Yodo1U3dAdEvent.AdError:
                    Debug.Log("[Yodo1 Mas] Interstital ad error, " + error.ToString());
                    break;
            }

        });

        Yodo1U3dMas.SetRewardedAdDelegate((Yodo1U3dAdEvent adEvent, Yodo1U3dAdError error) =>
        {
            Debug.Log("[Yodo1 Mas] RewardVideoDelegate:" + adEvent.ToString() + "\n" + error.ToString());
            switch (adEvent)
            {
                case Yodo1U3dAdEvent.AdClosed:
                    Debug.Log("[Yodo1 Mas] Reward video ad has been closed.");
                    break;
                case Yodo1U3dAdEvent.AdOpened:
                    Debug.Log("[Yodo1 Mas] Reward video ad has shown successful.");
                    break;
                case Yodo1U3dAdEvent.AdError:
                    loadingSpeed=1;
                    if(PlayerPrefs.GetInt("KeysAt"+Application.loadedLevel.ToString())!=4){
                        PlayerPrefs.SetInt("KeysAt"+Application.loadedLevel.ToString(),0);  
                        PlayerPrefs.SetInt("Keykey@"+Application.loadedLevel.ToString(),0);
                        PlayerPrefs.SetInt("Keykey (1)@"+Application.loadedLevel.ToString(),0);
                        PlayerPrefs.SetInt("Keykey (2)@"+Application.loadedLevel.ToString(),0);
                    }
                    Debug.Log("[Yodo1 Mas] Reward video ad error, " + error);
                    break;
                case Yodo1U3dAdEvent.AdReward:
                    loadingSpeed=0;
                    Debug.Log("The ad was successfully shown.");
                    resumed=true;
                    additionalLifePanel.SetActive(false);
                    loadingPanel.SetActive(true);
                    Application.LoadLevel(Application.loadedLevel);
                    KnightHealthSystem.addCnt=1;
                    Debug.Log("[Yodo1 Mas] Reward video ad reward, give rewards to the player.");
                    break;
            }

        });
    }

    /*
    private void HandleShowResult(ShowResult result)
          {
            switch (result)
            {
              case ShowResult.Finished:
                loadingSpeed=0;
                Debug.Log("The ad was successfully shown.");
                resumed=true;
                additionalLifePanel.SetActive(false);
                loadingPanel.SetActive(true);
                Application.LoadLevel(Application.loadedLevel);
                KnightHealthSystem.addCnt=1;
                
                break;

              case ShowResult.Skipped:
                loadingSpeed=1;
                if(PlayerPrefs.GetInt("KeysAt"+Application.loadedLevel.ToString())!=4){
                    PlayerPrefs.SetInt("KeysAt"+Application.loadedLevel.ToString(),0);  
                    PlayerPrefs.SetInt("Keykey@"+Application.loadedLevel.ToString(),0);
                    PlayerPrefs.SetInt("Keykey (1)@"+Application.loadedLevel.ToString(),0);
                    PlayerPrefs.SetInt("Keykey (2)@"+Application.loadedLevel.ToString(),0);
                }
                

                Debug.Log("The ad was skipped before reaching the end.");
                break;

              case ShowResult.Failed:
                loadingSpeed=1;
                if(PlayerPrefs.GetInt("KeysAt"+Application.loadedLevel.ToString())!=4){
                    PlayerPrefs.SetInt("KeysAt"+Application.loadedLevel.ToString(),0);  
                    PlayerPrefs.SetInt("Keykey@"+Application.loadedLevel.ToString(),0);
                    PlayerPrefs.SetInt("Keykey (1)@"+Application.loadedLevel.ToString(),0);
                    PlayerPrefs.SetInt("Keykey (2)@"+Application.loadedLevel.ToString(),0);
                }
                Debug.LogError("The ad failed to be shown.");
                break;
            }
          }



    private RewardBasedVideoAd rewardBasedVideo;
    void InitAdmob(){
        this.rewardBasedVideo = RewardBasedVideoAd.Instance;
     // RewardBasedVideoAd is a singleton, so handlers should only be registered once.
        this.rewardBasedVideo.OnAdLoaded += this.HandleRewardBasedVideoLoaded;
        this.rewardBasedVideo.OnAdFailedToLoad += this.HandleRewardBasedVideoFailedToLoad;
        this.rewardBasedVideo.OnAdOpening += this.HandleRewardBasedVideoOpened;
        this.rewardBasedVideo.OnAdStarted += this.HandleRewardBasedVideoStarted;
        this.rewardBasedVideo.OnAdRewarded += this.HandleRewardBasedVideoRewarded;
        this.rewardBasedVideo.OnAdClosed += this.HandleRewardBasedVideoClosed;
        this.rewardBasedVideo.OnAdLeavingApplication += this.HandleRewardBasedVideoLeftApplication;

        RequestRewardBasedVideo();
    }
    public void Clickonrewardreqest()
    {
        this.RequestRewardBasedVideo ();
        //Debug.Log ("s");
    }
    public void Clickonrewardshow()
    {
        this.ShowRewardBasedVideo ();
    }
    private void RequestRewardBasedVideo()
    {
        string adUnitId = "ca-app-pub-4962234576866611/2507076120";
        rewardBasedVideo.LoadAd (new AdRequest.Builder ().Build (), adUnitId);
    }
    private void ShowRewardBasedVideo()
    {
        if (this.rewardBasedVideo.IsLoaded())
        {
            loadingSpeed=0;
            this.rewardBasedVideo.Show();
        }
        else
        {
            // if (Advertisement.IsReady("rewardedVideo"))
            // {
            //     loadingSpeed=0;
            //     var options = new ShowOptions { resultCallback = HandleShowResult };
            //     Advertisement.Show("rewardedVideo", options);
            // }
            
            MonoBehaviour.print("Reward based video ad is not ready yet");
        }
    }
    
    public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoLoaded event received");
    }
    public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        loadingSpeed=1;
        if(PlayerPrefs.GetInt("KeysAt"+Application.loadedLevel.ToString())!=4){
            PlayerPrefs.SetInt("KeysAt"+Application.loadedLevel.ToString(),0);  
            PlayerPrefs.SetInt("Keykey@"+Application.loadedLevel.ToString(),0);
            PlayerPrefs.SetInt("Keykey (1)@"+Application.loadedLevel.ToString(),0);
            PlayerPrefs.SetInt("Keykey (2)@"+Application.loadedLevel.ToString(),0);
        }
        MonoBehaviour.print(
            "HandleRewardBasedVideoFailedToLoad event received with message: " + args.Message);
    }
    public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoOpened event received");
    }
    public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoStarted event received");
    }
    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        loadingSpeed=1;
        if(PlayerPrefs.GetInt("KeysAt"+Application.loadedLevel.ToString())!=4){
            PlayerPrefs.SetInt("KeysAt"+Application.loadedLevel.ToString(),0);  
            PlayerPrefs.SetInt("Keykey@"+Application.loadedLevel.ToString(),0);
            PlayerPrefs.SetInt("Keykey (1)@"+Application.loadedLevel.ToString(),0);
            PlayerPrefs.SetInt("Keykey (2)@"+Application.loadedLevel.ToString(),0);
        }
        MonoBehaviour.print("HandleRewardBasedVideoClosed event received");
    }
    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        loadingSpeed=0;
        Debug.Log("The ad was successfully shown.");
        resumed=true;
        additionalLifePanel.SetActive(false);
        loadingPanel.SetActive(true);
        Application.LoadLevel(Application.loadedLevel);
        KnightHealthSystem.addCnt=1;
    }
    public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
    {
        loadingSpeed=1;
        if(PlayerPrefs.GetInt("KeysAt"+Application.loadedLevel.ToString())!=4){
            PlayerPrefs.SetInt("KeysAt"+Application.loadedLevel.ToString(),0);  
            PlayerPrefs.SetInt("Keykey@"+Application.loadedLevel.ToString(),0);
            PlayerPrefs.SetInt("Keykey (1)@"+Application.loadedLevel.ToString(),0);
            PlayerPrefs.SetInt("Keykey (2)@"+Application.loadedLevel.ToString(),0);
        }
                

        Debug.Log("The ad was skipped before reaching the end.");
        MonoBehaviour.print("HandleRewardBasedVideoLeftApplication event received");
    }
    */
}

