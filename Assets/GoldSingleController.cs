using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldSingleController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("Gold"+gameObject.name.ToString()+"@"+Application.loadedLevel.ToString())==1){
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
