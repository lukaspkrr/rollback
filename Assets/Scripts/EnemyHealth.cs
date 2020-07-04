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
    }
    void Start()
    {
        anim =  GetComponent<Animator>();
        base.Start();
    }

}
