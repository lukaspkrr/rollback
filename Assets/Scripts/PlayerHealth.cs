using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Damageable
{
    private Animator anim;
    public GameObject LifeBar;
    // Start is called before the first frame update
    public override void Death(){
        anim.SetBool("die", true);
        Debug.Log("morreu");
    }

     public override void ReduceLifeBar(int currentHealth, int maxHealth){
         anim.SetBool("hit", true);
         if(currentHealth > 0){
            LifeBar.gameObject.transform.localScale = new Vector3((float)currentHealth/100,1,1);
         } else{
             LifeBar.gameObject.transform.localScale = new Vector3(0,1,1);

         }
         Invoke("ReleaseDamage", 0.1f);
        //  anim.SetBool("hit", false);
    }
    void ReleaseDamage() {
        anim.SetBool("hit", false);
    }
    void Start()
    {
        anim =  GetComponent<Animator>();
        base.Start();
    }

}
