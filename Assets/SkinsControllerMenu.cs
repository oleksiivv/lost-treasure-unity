using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinsControllerMenu : MonoBehaviour
{
    public GameObject[] skins;
    // Start is called before the first frame update
    void Start()
    {
        foreach(var skin in skins){
            skin.gameObject.SetActive(false);
        }
        skins[PlayerPrefs.GetInt("CurrentPlayerSkin")].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
