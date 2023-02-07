using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeysController : MonoBehaviour
{
    public Text countText;

    [HideInInspector()]
    public int count{get; private set;}

    public int keysAtLevel;
    public WallController wall;


    void Start(){
        count=PlayerPrefs.GetInt("KeysAt"+Application.loadedLevel.ToString());

        if(count==4 || LastSavedPositionController.lastSavedPos!=Vector3.zero){
            if(count>3)count=3;
            if(count<0)count=0;
            countText.text=count.ToString()+"/"+keysAtLevel.ToString();
        }
        else{
            PlayerPrefs.SetInt("KeysAt"+Application.loadedLevel.ToString(),0);
            count=0;
            countText.text="0/"+keysAtLevel.ToString();
        }
    }

    void Update(){
        if(keysAtLevel==count && wall.closed){
            wall.closed=false;
        }
    }

    public void add(int n=1){
        count+=n;
        countText.text=count.ToString()+"/"+keysAtLevel.ToString();
    }

    public void save(){
        PlayerPrefs.SetInt("KeysAt"+Application.loadedLevel.ToString(),count);
    }
    
    
}
