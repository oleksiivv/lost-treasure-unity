using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    public AudioClip getKey,getGold;
    public AudioClip damageFire,damageArrow,damageSpider;
    public AudioClip deathLaught;
    public AudioClip openDoor;
    public AudioClip spiderDeath;

    private AudioSource src;

    void Awake(){
        gameObject.AddComponent<AudioSource>();

        src=gameObject.GetComponent<AudioSource>();
        src.loop=false;
        src.playOnAwake=false;
    }


    public void playFireDamage(){
        if(PlayerPrefs.GetInt("!sound")==0){
            src.clip=damageFire;
            src.Play();
        }
    }

    public void playArrowDamage(){
        if(PlayerPrefs.GetInt("!sound")==0){
            src.clip=damageArrow;
            src.Play();
        }
    }

    public void playGetKey(){
        if(PlayerPrefs.GetInt("!sound")==0){
            src.clip=getKey;
            src.Play();
        }
    }

    public void playGetGold(){
        if(PlayerPrefs.GetInt("!sound")==0){
            src.clip=getGold;
            src.Play();
        }
    }

    public void playDeathLaught(){
        if(PlayerPrefs.GetInt("!sound")==0){
            src.clip=deathLaught;
            src.Play();
        }
    }
    public void playOpenDoor(){
        if(PlayerPrefs.GetInt("!sound")==0){
            src.clip=openDoor;
            src.Play();
        }
    }

    public void playSpiderDeath(){
        if(PlayerPrefs.GetInt("!sound")==0){
            src.clip=spiderDeath;
            src.Play();
        }
    }

    public void getSpiderDamage(){
        if(PlayerPrefs.GetInt("!sound")==0){
            src.clip=damageSpider;
            src.Play();
        }
    }
    
}
