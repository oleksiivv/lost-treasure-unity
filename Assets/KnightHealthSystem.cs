using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightHealthSystem : MonoBehaviour
{
    private KnightUI ui;
    private KnightMobileMove move;
    private BoxCollider box;

    public int healthCnt {get; set;}

    public static int addCnt=0;

    public bool study=false;




    void Start(){
        ui=GetComponent<KnightUI>();
        move = GetComponent<KnightMobileMove>();
        box = GetComponent<BoxCollider>();
        healthCnt=3;
        if(LastSavedPositionController.lastSavedPos!=Vector3.zero){
            damage();
            damage();
        }

        

        

        
    }

    public void damage(){
        if(healthCnt>0){
            healthCnt--;
        }
        if(healthCnt==0){
            move.rigidbody.AddForce(Vector3.up*10);
            move.animator.SetBool("die",true);
            box.size = new Vector3(1,1.2f,2);
            box.center = new Vector3(0,0.5f,-0.5f);
            Invoke(nameof(showDiePanel),1.5f);
        }

        for(int i=0;i<ui.hearts.Length;i++){
            if(i<healthCnt){
                ui.hearts[i].gameObject.SetActive(true);
            }
            else{
                ui.hearts[i].gameObject.SetActive(false);
            }
        }
    }

    public void showDiePanel()
    {
        if(ui.additionalLifePanel.activeSelf || ui.diePanel.activeSelf){
            return;
        }

        move.sound.playDeathLaught();
        if(addCnt%2==0 && !study){
            ui.showAdditionalLifePanel();
        }else{
          ui.diePanel.SetActive(true);
          if(PlayerPrefs.GetInt("KeysAt"+Application.loadedLevel.ToString())!=4){
              PlayerPrefs.SetInt("KeysAt"+Application.loadedLevel.ToString(),0);  
          }
          addCnt++;
        }

        
        //ui.diePanel.SetActive(true);
    }



    


}
