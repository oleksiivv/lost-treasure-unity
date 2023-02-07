using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaiderStudy : MonoBehaviour
{
    public GameObject[] studyPanels;
    public GameObject winPanel, winUI;

    private int current=-1;

    void Start(){
        show(0);
    }

    public void show(int id){
        if(id<studyPanels.Length && id>current){
            foreach(var panel in studyPanels)panel.SetActive(false);
        
            studyPanels[id].SetActive(true);
            current=id;
        }

        if(id>=studyPanels.Length){
            foreach(var panel in studyPanels)panel.SetActive(false);
        }
    }


    public void skipStudy(){
        Time.timeScale=1;
        PlayerPrefs.SetInt("studied",1);
        winPanel.SetActive(true);
        Invoke(nameof(showFinishUI),1.6f);
    }

    void showFinishUI(){
        winUI.SetActive(true);
    }
}
