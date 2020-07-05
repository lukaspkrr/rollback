using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Damageable
{
    private Animator anim;
    // Start is called before the first frame update
    public override void Death(){
        anim.SetBool("die", true);
        Debug.Log("morreu");
        Invoke("DestroyAferDeath", 1f);
    }
    public override void ReduceLifeBar(int currentHealth, int maxHealth){
        Debug.Log("teste");
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
    }

}
