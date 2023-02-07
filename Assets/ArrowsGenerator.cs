using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowsGenerator : MonoBehaviour
{
    public GameObject arrow;
    public float startDelay=0.1f;

    void Start(){
        Invoke(nameof(StartArcher),startDelay);
    }
    void StartArcher(){
        StartCoroutine(archer());
    }

    IEnumerator archer(){
        while(true){

            Instantiate(arrow, new Vector3(transform.position.x-transform.localScale.x*2,transform.position.y+transform.localScale.y/2,transform.position.z-0*transform.localScale.z/2), arrow.transform.rotation);
            yield return new WaitForSeconds(0.6f);
        }
    }
}
