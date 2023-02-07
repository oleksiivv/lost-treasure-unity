using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject player;
    private KnightMobileMove playerMove;

    Vector3 offset;
    void Start()
    {

        offset=player.transform.position-transform.position;
        playerMove=player.GetComponent<KnightMobileMove>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerMove.ableToJump){
            transform.position=new Vector3(player.transform.position.x-offset.x,player.transform.position.y-offset.y,player.transform.position.z-offset.z);
        }
        else{
            transform.position=new Vector3(player.transform.position.x-offset.x,transform.position.y,player.transform.position.z-offset.z);
        }
    }
}
