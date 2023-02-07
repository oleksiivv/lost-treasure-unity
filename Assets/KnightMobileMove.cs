using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightMobileMove : MonoBehaviour
{
    public Joystick joystick; 
    
    [HideInInspector()]
    public Rigidbody rigidbody;
    public float jumpForce;

    public Animator animator;
    private KnightTriggers triggers;
    private KnightHealthSystem healthSystem;

    [HideInInspector()]
    public SoundEffects sound;

    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        triggers = GetComponent<KnightTriggers>();
        healthSystem = GetComponent<KnightHealthSystem>();

        sound=gameObject.GetComponent<SoundEffects>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(PlayerPrefs.GetInt("KeysAt"+Application.loadedLevel.ToString()));
        if((joystick.Horizontal!=0 || joystick.Vertical!=0) && !triggers.damaged && healthSystem.healthCnt>0 && !triggers.win){

            if(collNumber>0 || totalCollsNumber>0){
                float heading = Mathf.Atan2(-joystick.Vertical,joystick.Horizontal);
                animator.gameObject.transform.rotation=Quaternion.Euler(0f,heading*Mathf.Rad2Deg,0f);

                animator.SetBool("run",true);
                ableToJump=true;
            }

             else if(collNumber==0 && totalCollsNumber==0 && atJump){
                 transform.Translate(Vector3.forward/7);
             }
             else if(isAttacking){
                 transform.Translate(Vector3.forward/15);
             }

             if(collNumber==0 && totalCollsNumber==0){
                 animator.SetBool("run",false);
             }

        }
        else{
            animator.SetBool("run",false);
        }

        if(Input.GetKeyUp(KeyCode.Space) && collNumber==1){
            jump();
        }

        //Debug.Log(collNumber.ToString()+" : "+totalCollsNumber.ToString());


        if(collNumber==0 && gameObject.transform.position.y<=-0.5f){
            animator.SetBool("die",true);
            collNumber=1;
        }

        //Debug.Log("Player attacks: "+isAttacking.ToString());

    }


    ///

    [HideInInspector()]
    public bool ableToJump=false;
    public bool isAttacking=false;

    [HideInInspector()]
    public int collNumber=0;
    [HideInInspector()]
    public int totalCollsNumber=0;
    private bool atJump=false;
    

    void OnCollisionEnter(Collision other){
        if(other.gameObject.tag=="ground")collNumber++;
        else if(!other.gameObject.name.ToUpper().Contains("DEFAULT")){
            animator.SetBool("run",true);
            animator.SetBool("jump",false);

            totalCollsNumber++;
        }

        
    }


    void OnCollisionStay(Collision other){
        if(IsInvoking(nameof(getDamage)))return;
        if(other.gameObject.tag=="spider" && !isAttacking){
            if(Mathf.Abs(other.transform.position.y-gameObject.transform.position.y)>0.6f){
                rigidbody.AddForce(Vector3.up*jumpForce/2);
                rigidbody.AddForce(Vector3.back*jumpForce*15);
                //rigidbody.AddRelativeTorque(Vector3.forward*1000);
                totalCollsNumber=0;
                return;
            }
            sound.getSpiderDamage();
            healthSystem.damage();
            //rigidbody.AddForce(Vector3.up*jumpForce);
            animator.SetBool("damage",true);

            Invoke(nameof(getDamage),1f);
        }
    }

    void getDamage(){
        return;
    }



    void OnCollisionExit(Collision other){
        if(other.gameObject.tag=="ground")collNumber--;
        else if(other.gameObject.tag!="spider"&& !other.gameObject.name.ToUpper().Contains("DEFAULT")){
             totalCollsNumber--;
        }

        // if(other.gameObject.name.ToUpper().Contains("ROTATINGOBSTACLE")){
        //     gameObject.transform.parent=null;
        // }

        
    }

    public void jump(){
        if(collNumber==1){
            rigidbody.AddForce(Vector3.up*jumpForce);
            animator.SetBool("jump",true);
            atJump=true;
            Invoke(nameof(jumpReset),1);
        }
    }

    public void attack(){
        isAttacking=true;
        animator.SetBool("attack",true);
        Invoke(nameof(attackReset),0.5f);
    }

    void attackReset(){
        isAttacking=false;
    }

    void jumpReset(){
        atJump=false;
    }
}
