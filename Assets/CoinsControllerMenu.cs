using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsControllerMenu : MonoBehaviour
{
    public Text coinsTxt;

    void Start(){
        int coins=PlayerPrefs.GetInt("CoinsCollected");

        if(coins<=0){
            coinsTxt.gameObject.SetActive(false);
        }
        else{
            coinsTxt.gameObject.SetActive(true);
            coinsTxt.GetComponent<Text>().text=coins.ToString();
        }
    }
}
