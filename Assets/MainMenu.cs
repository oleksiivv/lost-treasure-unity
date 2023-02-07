using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject loadingPanel;
    public GameObject map, mapUI;

    public GameObject startStudyPanel;

    public Animator[] characters;

    void Update(){
        foreach(var character in characters)character.SetBool("run",false);
    }



    public void startGame(){
        if(PlayerPrefs.GetInt("studied")!=1){
            startStudyPanel.SetActive(true);
        }else{
            map.SetActive(true);
            Invoke(nameof(showMapUI),1f);
        }
    }
    public void openScene(int id){

        loadingPanel.SetActive(true);
        Application.LoadLevelAsync(id);
    }

    void showMapUI(){
        mapUI.SetActive(true);
    }

    public void closeLevelsPanel(){
        mapUI.SetActive(false);
        map.SetActive(false);
    }


    public void startStudy(int id){
        openScene(id);
    }

    public void skipStudy(){
        startStudyPanel.SetActive(false);
        PlayerPrefs.SetInt("studied",1);
        map.SetActive(true);
        Invoke(nameof(showMapUI),1f);
    }

    public void rate(){
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.VertexStudioGames.TombRaiderPlatformer");
    }
}
