using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class Damageable : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public UnityEvent  onDeath;
     public GameObject damageText;
    public Transform damageTextPosition;
    // Start is called before the first frame update
    protected void Start()
    {
        currentHealth = maxHealth;
        
    }

    public void TakeDamage(int amount){
        currentHealth -= amount;
        // onDamage.Invoke();
        ReduceLifeBar(currentHealth,maxHealth);
        if(currentHealth <= 0){
            onDeath.Invoke();
            Death();
        }
    }

    public abstract void Death();
    public abstract void ReduceLifeBar(int currentHealth, int maxHealth);

    private void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "Arrow"){
        GameObject newDamageText = Instantiate(damageText, damageTextPosition.position, Quaternion.identity);
        newDamageText.GetComponentInChildren<Text>().text = "1";
        Destroy(newDamageText, 1);
        }
   }

}
