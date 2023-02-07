using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CoinsCollect : MonoBehaviour
{
    public Slider coinsCollected;
    public int goldInLevel;

    public Text coinsLabel;

    

    void Start(){
        coinsCollected.minValue=0;
        coinsCollected.maxValue=goldInLevel;
        coinsCollected.value=PlayerPrefs.GetInt("CoinsCollectedAt"+Application.loadedLevel.ToString());
        coinsLabel.text = PlayerPrefs.GetInt("CoinsCollectedAt"+Application.loadedLevel.ToString()).ToString()+"/"+goldInLevel.ToString();
    }

    void Update(){
        // if(coinsCollected.value>=goldInLevel/2 && wall.gameObject.GetComponent<Animator>().enabled==false){
        //     wall.GetComponent<Animator>().enabled=true;
        // }
    }

    public void addCoins(int n=1){
        coinsCollected.value+=n;
        save();

        coinsLabel.text = coinsCollected.value.ToString()+"/"+goldInLevel.ToString();
    }

    public void save(){
        PlayerPrefs.SetInt("CoinsCollected",PlayerPrefs.GetInt("CoinsCollected")+Convert.ToInt32(coinsCollected.value)-PlayerPrefs.GetInt("CoinsCollectedAt"+Application.loadedLevel.ToString()));
        PlayerPrefs.SetInt("CoinsCollectedAt"+Application.loadedLevel.ToString(),Convert.ToInt32(coinsCollected.value));
    }


}
