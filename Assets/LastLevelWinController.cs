using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LastLevelWinController : MonoBehaviour
{
    public Text totalGold; 
    public Text lastLevelGold;

    public GameObject winPanel;
    private bool showed=false;

    void Start(){
        if(PlayerPrefs.GetInt("win",-1)==1){
            for(int i=0;i<gameObject.transform.childCount;i++){
                gameObject.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.name.ToUpper().Contains("RAIDER")){
            //if(winPanel.activeSelf && !showed){
                if(PlayerPrefs.GetInt("win",-1)==-1){
                    PlayerPrefs.SetInt("CoinsCollected",PlayerPrefs.GetInt("CoinsCollected")+150000);
                    PlayerPrefs.SetInt("win",1);
                }
                else{
                    for(int i=0;i<gameObject.transform.childCount;i++){
                        gameObject.transform.GetChild(i).gameObject.SetActive(false);
                    }
                }
                lastLevelGold.GetComponent<Text>().text="Collected in mission #18: "+PlayerPrefs.GetInt("CoinsCollectedAt"+Application.loadedLevel.ToString()).ToString()+" gold ignots";
                totalGold.GetComponent<Text>().text="Total collected: "+PlayerPrefs.GetInt("CoinsCollected").ToString();
            //}
        }
    }
}
