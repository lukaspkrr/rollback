using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Damage : MonoBehaviour
{
    // Start is called before the first frame update
    private SpriteRenderer spriteRenderer;
    public float damageTime = 0.1f;

    public GameObject damageText;
    public Transform damageTextPosition;
    public Animator anim;
    public Damageable damageable;
    private void Awake (){
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim =  GetComponent<Animator>();
    }
    public void TakeDamage(int damage){
        anim.SetBool("hit", true);
        Invoke("ReleaseDamage", damageTime);
        damageable.TakeDamage(damage);
        GameObject newDamageText = Instantiate(damageText, damageTextPosition.position, Quaternion.identity);
        newDamageText.GetComponentInChildren<Text>().text = damage.ToString();
        Destroy(newDamageText, 1);
    }

    void ReleaseDamage() {
        anim.SetBool("hit", false);
    }
}
