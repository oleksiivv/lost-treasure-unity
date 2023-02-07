using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySingleController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // if(LastSavedPositionController.lastSavedPos!=Vector3.zero && PlayerPrefs.GetInt("Key"+gameObject.name.ToString()+"@"+Application.loadedLevel.ToString())==1){
        //     PlayerPrefs.SetInt("Key"+gameObject.name.ToString()+"@"+Application.loadedLevel.ToString(),0);
        // }
        if(PlayerPrefs.GetInt("KeysAt"+Application.loadedLevel.ToString())==4){
            gameObject.SetActive(false);
            Destroy(gameObject);
                
        }
        if(PlayerPrefs.GetInt("Key"+gameObject.name.ToString()+"@"+Application.loadedLevel.ToString())==1){
            
            if(PlayerPrefs.GetInt("KeysAt"+Application.loadedLevel.ToString())==4){
                gameObject.SetActive(false);
                Destroy(gameObject);
                
            }

            if(LastSavedPositionController.lastSavedPos!=Vector3.zero){
                Debug.Log("FUUUUCK");
                PlayerPrefs.SetInt("Key"+gameObject.name.ToString()+"@"+Application.loadedLevel.ToString(),0);
                gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }
    }

    void check()
    {
        // if(LastSavedPositionController.lastSavedPos!=Vector3.zero && PlayerPrefs.GetInt("Key"+gameObject.name.ToString()+"@"+Application.loadedLevel.ToString())==1){
        //     PlayerPrefs.SetInt("Key"+gameObject.name.ToString()+"@"+Application.loadedLevel.ToString(),0);
        // }
        if(PlayerPrefs.GetInt("KeysAt"+Application.loadedLevel.ToString())==4){
            gameObject.SetActive(false);
            Destroy(gameObject);
                
        }
        if(PlayerPrefs.GetInt("Key"+gameObject.name.ToString()+"@"+Application.loadedLevel.ToString())==1){
            
            if(PlayerPrefs.GetInt("KeysAt"+Application.loadedLevel.ToString())==4){
                gameObject.SetActive(false);
                Destroy(gameObject);
                
            }

            if(LastSavedPositionController.lastSavedPos!=Vector3.zero){
                Debug.Log("FUUUUCK");
                PlayerPrefs.SetInt("Key"+gameObject.name.ToString()+"@"+Application.loadedLevel.ToString(),0);
                gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        check();
        //Debug.Log(LastSavedPositionController.lastSavedPos);
        transform.Rotate(0,2,0);
    }
}
