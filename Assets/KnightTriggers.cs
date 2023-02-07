using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class KnightTriggers : MonoBehaviour
{
    private KnightUI ui;
    private KnightHealthSystem healthSystem;
    private Rigidbody rigidbody;

    private KnightMobileMove move;
    private KeysController keys;
    private CoinsCollect coins;

    public bool damaged=false;

    public ParticleSystem[] particles;

    [HideInInspector()]
    public bool win=false;
    public KeySingleController[] keysObj;

    private bool started=false;

    public bool lastLevel=false;

    void Start(){
        win=false;
        ui = gameObject.GetComponent<KnightUI>();
        healthSystem = gameObject.GetComponent<KnightHealthSystem>();
        rigidbody = gameObject.GetComponent<Rigidbody>();

        move = gameObject.GetComponent<KnightMobileMove>();
        coins=gameObject.GetComponent<CoinsCollect>();
        keys=GetComponent<KeysController>();

        // if(LastSavedPositionController.lastSavedPos!=Vector3.zero){
        //     foreach(var key in keysObj){
        //         if(PlayerPrefs.GetInt())
        //     }
        // }

        Invoke(nameof(startTriggerDetect),1);
    }

    void startTriggerDetect(){
        started=true;
    }

    void OnTriggerEnter(Collider other){
        if(!started)return;
        if(other.gameObject.tag=="Respawn"){
            healthSystem.healthCnt=0;
            
            if(!ui.additionalLifePanel.activeSelf && !ui.diePanel.activeSelf){
                healthSystem.showDiePanel();
            }
            //ui.showAdditionalLifePanel();
            //ui.diePanel.SetActive(true);
        }
        else if(other.gameObject.tag=="arrow"){
            healthSystem.damage();
            rigidbody.AddForce(Vector3.right*2000);
            if(healthSystem.healthCnt>0)move.animator.SetBool("damage",true);
            damaged=true;
            if(!IsInvoking(nameof(reserDamage)))Invoke(nameof(reserDamage),0.5f);

            move.sound.playArrowDamage();
        }

        else if(other.gameObject.tag=="fire"){
            healthSystem.damage();
            rigidbody.AddForce(Vector3.back*2000);
            if(healthSystem.healthCnt>0)move.animator.SetBool("damage",true);
            damaged=true;
            if(!IsInvoking(nameof(reserDamage)))Invoke(nameof(reserDamage),0.5f);

            move.sound.playFireDamage();

        }
        else if(other.gameObject.tag=="gold"){
            PlayerPrefs.SetInt("Gold"+other.gameObject.name.ToString()+"@"+Application.loadedLevel.ToString(),1);
            coins.addCoins(Convert.ToInt32(other.gameObject.name));
            other.gameObject.SetActive(false);
            particles[0].Play();
            move.sound.playGetGold();
        }
        else if(other.gameObject.tag=="key"){
            PlayerPrefs.SetInt("Key"+other.gameObject.name.ToString()+"@"+Application.loadedLevel.ToString(),1);

            keys.add();
            keys.save();
            other.gameObject.SetActive(false);
            particles[1].Play();
            move.sound.playGetKey();
        }
        else if(other.gameObject.tag=="Finish"){
            PlayerPrefs.SetInt("studied",1);
            win=true;
            PlayerPrefs.SetInt("KeysAt"+Application.loadedLevel.ToString(),4);
            //keys.save();
            coins.save();
            finishLevel();
            

            if(!lastLevel)ui.resultText.text="Collected: "+coins.coinsCollected.value.ToString()+" gold ignots";
        }
        else if(other.gameObject.tag=="checkPoint"){
            LastSavedPositionController.lastSavedPos=other.gameObject.transform.position;
        }

        else if(other.gameObject.tag=="UpperCheckpoint"){
            move.collNumber=0;
            move.totalCollsNumber=0;
            Debug.Log("000000000000000000000000");
        }
    }

    void reserDamage(){
        damaged=false;
    }

    void finishLevel(){
        ui.winPanel.SetActive(true);
        Invoke(nameof(showFinishUI),1.6f);
    }

    void showFinishUI(){
        ui.winUI.SetActive(true);
    }
}
