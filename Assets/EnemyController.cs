using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public CameraMovement cam;
    private GameObject player;
    private bool startMove=false;

    private Animator animator;
    private bool attack=false;
    private bool alive=true;

    // Start is called before the first frame update
    void Start()
    {
        player=cam.player;
        animator = gameObject.GetComponent<Animator>();
        
    }


    void Update(){
        if(alive && player.gameObject.GetComponent<KnightHealthSystem>().healthCnt>0){
            if(!attack && startMove){
                animator.SetBool("run",true);

                Vector3 newDir = Vector3.RotateTowards(transform.forward, (player.transform.position - transform.position), 1, 0.0F);

                var lookRot=Quaternion.LookRotation(new Vector3(newDir.x,transform.position.y,newDir.z));
                
                var eulerAngles= lookRot.eulerAngles;
                transform.eulerAngles = new Vector3(0,eulerAngles.y,0);

                gameObject.transform.position=Vector3.MoveTowards(transform.position,
                                             new Vector3(player.transform.position.x,transform.position.y,player.transform.position.z),
                                              0.03f*Time.timeScale);
            }
            else{
                if(!attack)animator.SetBool("run",false);
            }
        }
        else{
            animator.SetBool("run",false);
        }
    }

    void OnTriggerStay(Collider other){
        if(other.gameObject.name.ToUpper().Contains("RAIDER")){
            startMove=true;
        }
    }

    void OnTriggerExit(Collider other){
        if(other.gameObject.name.ToUpper().Contains("RAIDER")){
            
            startMove=false;
            
        }
    }
    

    void OnCollisionStay(Collision other){
        if(other.gameObject.name.ToUpper().Contains("RAIDER")){
            
            if(other.gameObject.GetComponent<KnightHealthSystem>().healthCnt>0 && alive){
                if(!alive)return;
                if(Mathf.Abs(other.transform.position.y-transform.position.y)>0.6f){
                    startMove=false;
                    //other.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.forward*-100);
                    return;
                }
                attack=true;
                if(player.GetComponent<KnightMobileMove>().isAttacking){
                    animator.SetBool("damage",true);
                    Invoke(nameof(startBecomingSmall), 1f);
                    alive=false;
                    gameObject.tag="Untagged";
                    player.GetComponent<KnightMobileMove>().totalCollsNumber=0;
                    Destroy(GetComponent<Rigidbody>());
                    player.GetComponent<KnightMobileMove>().sound.playSpiderDeath();
                    return;
                }
                
                else{
                    animator.SetBool("attack",true);
                }
            }
        }
    }

    void OnCollisionExit(Collision other){
        if(other.gameObject.name.ToUpper().Contains("RAIDER")){
            attack=false;
        }
        if(other.gameObject.tag=="ground"){

            GetComponent<Rigidbody>().isKinematic=false;
            GetComponent<Rigidbody>().useGravity=true;
            GetComponent<Rigidbody>().AddForce(Vector3.down*100);
            
        }
    }

    void startBecomingSmall(){
        StartCoroutine(becomeSmaller(transform.localScale/2));
    }
    IEnumerator becomeSmaller(Vector3 requiredScale){
        while((int)transform.localScale.y!=(int)requiredScale.y){
            transform.localScale*=0.99f;
            yield return new WaitForEndOfFrame();
        }
    }

    
}
