using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    public bool closed;
    private bool startOpen=false;
    public int speed=1;
    private Vector3 startPos;

    

    void Start(){
        closed=true;

        if(speed<0){
            startPos=transform.position;
            transform.position=new Vector3(transform.position.x,transform.position.y+5,transform.position.z);
        }
    }

    void Update(){
        if(startOpen){
            if(speed>0)transform.position = Vector3.MoveTowards(transform.position,new Vector3(transform.position.x,transform.position.y+5,transform.position.z),0.1f*speed);
        }
        if(speed<0){
            transform.position = Vector3.MoveTowards(transform.position,startPos,0.1f*-speed);
        }
    }

    void OnCollisionEnter(Collision other){
        if(other.gameObject.name.ToUpper().Contains("RAIDER")){
            if(!closed){
                
                if(speed>0){
                    startOpen=true;
                    other.gameObject.GetComponent<KnightMobileMove>().sound.playOpenDoor();
                }
            }
        }
    }

}
