using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinController : MonoBehaviour
{
    public GameObject[] skins;

    private GameObject player;

    private CameraMovement cameraMovement;
    void Awake()
    {
        cameraMovement = gameObject.GetComponent<CameraMovement>();
        cameraMovement.player=skins[PlayerPrefs.GetInt("CurrentPlayerSkin")];

        foreach(var skin in skins){
            skin.gameObject.SetActive(false);
        }
        skins[PlayerPrefs.GetInt("CurrentPlayerSkin")].SetActive(true);

        foreach(var skin in skins){
            if(!skin.activeSelf)Destroy(skin.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
