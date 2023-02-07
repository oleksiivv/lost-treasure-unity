using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public GameObject buttonMutedSound, buttonNormalSound;
    public GameObject buttonMutedMusic, buttonNormalMusic;

    public GameObject undoPanel;
    public Dropdown quality;
    public AudioController audio;
    void Start()
    {
        //PlayerPrefs.SetInt("hi",6000);
        quality.GetComponent<Dropdown>().value=QualitySettings.GetQualityLevel();
        
        updateMusic();
        updateSound();
    }

    public void muteSound(){
        PlayerPrefs.SetInt("!sound",1);
        updateSound();

        //GetComponent<AudioSource>().enabled=false;
    }

    public void unmuteSound(){
        PlayerPrefs.SetInt("!sound",0);
        updateSound();

        //GetComponent<AudioSource>().enabled=true;
    }


    public void muteMusic(){
        PlayerPrefs.SetInt("!music",1);
        updateMusic();
    }

    public void unmuteMusic(){
        PlayerPrefs.SetInt("!music",0);
        updateMusic();
    }

    void updateSound(){
        if(PlayerPrefs.GetInt("!sound")==0){

            buttonMutedSound.SetActive(false);
            buttonNormalSound.SetActive(true);

        }
        else{
            buttonMutedSound.SetActive(true);
            buttonNormalSound.SetActive(false);
        }

        audio.updateMusic();
        audio.updateSound();
    }

    void updateMusic(){
        if(PlayerPrefs.GetInt("!music")==0){

            buttonMutedMusic.SetActive(false);
            buttonNormalMusic.SetActive(true);

        }
        else{
            buttonMutedMusic.SetActive(true);
            buttonNormalMusic.SetActive(false);
        }

        audio.updateMusic();
        audio.updateSound();
    }

    public void SetQuality(int qualityIndex){
        QualitySettings.SetQualityLevel(qualityIndex);
    }


    public GameObject loadingPanel;
    public void openScene(int id){
        Time.timeScale=1;
        loadingPanel.SetActive(true);
        Application.LoadLevelAsync(id);
    }

    public void showUndoPanel(){
        undoPanel.SetActive(true);
    }

    public void closeUndoPanel(){
        undoPanel.SetActive(false);
    }


    public void undoProgress(){
        
        PlayerPrefs.DeleteAll();
        closeUndoPanel();
        Application.LoadLevel(Application.loadedLevel);

    }
}
