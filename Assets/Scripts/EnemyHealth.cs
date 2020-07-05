﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Damageable
{
    private Animator anim;
    private float Life;

    private float OriginalScale;

    private GameObject BossLifeBar;
    // Start is called before the first frame update
    public override void Death(){
        anim.SetBool("die", true);
        Invoke("DestroyAferDeath", 1f);
    }
    public override void ReduceLifeBar(int currentHealth, int maxHealth){

        if(BossLifeBar ){
            Life = OriginalScale * (float)currentHealth /maxHealth;
            float LifeY = BossLifeBar.gameObject.transform.localScale.y;
            float LifeZ = BossLifeBar.gameObject.transform.localScale.z;
            if(currentHealth > 0){
                
                BossLifeBar.gameObject.transform.localScale = new Vector3(Life ,LifeY,LifeZ);
            } else{
                BossLifeBar.gameObject.transform.localScale = new Vector3(0,LifeY,LifeZ);
            }
        }
        
        
        anim.SetBool("hit", true);
        Invoke("ReleaseDamage", 0.1f);
    }
    void ReleaseDamage() {
        anim.SetBool("hit", false);
    }
    void DestroyAferDeath() {
        Destroy(gameObject);
    }
    void Start()
    {
        anim =  GetComponent<Animator>();
        base.Start();
        try {
        BossLifeBar = GameObject.FindWithTag("BossLifeBar");
            if(BossLifeBar){
                OriginalScale = BossLifeBar.gameObject.transform.localScale.x;
            }
        }
        catch (System.Exception) {
            Debug.Log("SemBarra");
        }
        
    }

}
