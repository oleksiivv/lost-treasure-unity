using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewFireObstacle : MonoBehaviour
{
    public ParticleSystem fire;

    public float startDelay=0.1f;

    void Start(){
        Invoke(nameof(StartFire),startDelay);
    }

    void StartFire(){
        StartCoroutine(fireSwitcher());
    }


    IEnumerator fireSwitcher(){
        while(true){
            yield return new WaitForSeconds(2);
            if(fire.isStopped){
                fire.Play();
                fire.gameObject.GetComponent<BoxCollider>().enabled=true;
            }
            else {
                fire.Stop();
                fire.gameObject.GetComponent<BoxCollider>().enabled=false;
            }
        }
    }
}
