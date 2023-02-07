using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudyBoxSwitcher : MonoBehaviour
{
    public int id=1;
    public RaiderStudy study;


    void OnTriggerEnter(Collider other){
        if(other.gameObject.name.ToUpper().Contains("RAIDER")){
            study.show(id);
        }
    }
}
