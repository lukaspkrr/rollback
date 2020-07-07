using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : Damageable
{
    private Animator anim;
    public GameObject LifeBar;
    private float Life;
    // Start is called before the first frame update
    public override void Death(){
        anim.SetBool("die", true);
         StartCoroutine(timeOut());
    }

    IEnumerator timeOut() {
        yield return new WaitForSeconds(2);
         SceneManager.LoadScene(1);   
    }
     public override void ReduceLifeBar(int currentHealth, int maxHealth){
         anim.SetBool("hit", true);
         if(currentHealth > 0){
             Life = LifeBar.gameObject.transform.localScale.y * (float)currentHealth /100;
            LifeBar.gameObject.transform.localScale = new Vector3(Life ,LifeBar.gameObject.transform.localScale.y,LifeBar.gameObject.transform.localScale.z);
         } else{
             LifeBar.gameObject.transform.localScale = new Vector3(0,LifeBar.gameObject.transform.localScale.y,LifeBar.gameObject.transform.localScale.z);

         }
         Invoke("ReleaseDamage", 0.1f);
        //  anim.SetBool("hit", false);
    }
    void ReleaseDamage() {
        anim.SetBool("hit", false);
    }
    void Start(){
        anim =  GetComponent<Animator>();
        base.Start();
    }

}
