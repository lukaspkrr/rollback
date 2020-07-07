using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBreath : MonoBehaviour
{
    // Start is called before the first frame update
    public float breathingTime;
    public float breathReloadTime;
    private float breathTimer = 0f;

    public Animator anim;
    void Start()
    {
        
        anim =  GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update(){
        Breath();
    }

    void Breath(){
        breathTimer += Time.deltaTime;
        if(breathTimer >= breathReloadTime){
            anim.SetBool("attack", true);
            Invoke("FinishBreath",breathingTime);
        }
    }


    void FinishBreath(){
        anim.SetBool("attack", false);
        breathTimer = 0f;
        breathTimer += Time.deltaTime;
    }
}
