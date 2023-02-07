using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelsControlSystem : MonoBehaviour
{
    public int level0NumberInBuild=1;

    public Image[] level;
    public Image[] padlock;

    public Color32 availableLevelColor, unavailableLevelColor;

    public MainMenu menu;

    public GameObject winText;


    void Start(){
        updateLevels();
    }


    public void openLevel(int id){

        if(id!=1 && PlayerPrefs.GetInt("KeysAt"+(id-1).ToString(),-1)!=4)return;

        menu.openScene(id-1+level0NumberInBuild);
    }


    void updateLevels(){
        level[0].GetComponent<Image>().color=availableLevelColor;
        padlock[0].gameObject.SetActive(false);
        for(int i=1;i<level.Length;i++){
            if(PlayerPrefs.GetInt("KeysAt"+(i).ToString(),-1)!=4){
                level[i].GetComponent<Image>().color=unavailableLevelColor;
                padlock[i].gameObject.SetActive(true);
            }
            else{
                level[i].GetComponent<Image>().color=availableLevelColor;
                padlock[i].gameObject.SetActive(false);
            }
        }

        if(PlayerPrefs.GetInt("win")==1){
            winText.SetActive(true);
        }
        else{
            winText.SetActive(false);
        }
    }

}
