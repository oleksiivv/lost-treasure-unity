using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonsListener : MonoBehaviour
{
    public Button jump, attack, pause;
    public Button[] resume,restart;
    public Button skipAd,resumeByAd;

    public GameObject[] skins;

    private GameObject player;
    private KnightMobileMove knight;
    private KnightUI knightUI;

    void Start(){
        player = skins[PlayerPrefs.GetInt("CurrentPlayerSkin")];
        knight=player.GetComponent<KnightMobileMove>();
        knightUI = player.GetComponent<KnightUI>();

        jump.onClick.AddListener(knight.jump);
        attack.onClick.AddListener(knight.attack);

        skipAd.onClick.AddListener(knightUI.skipAdditionalLifePanel);
        resumeByAd.onClick.AddListener(knightUI.resumeByAd);
        pause.onClick.AddListener(knightUI.pause);

        foreach(var btn in resume){
            btn.onClick.AddListener(knightUI.resume);
        }

        foreach(var btn in restart){
            btn.onClick.AddListener(knightUI.restart);
        }

    }
}
