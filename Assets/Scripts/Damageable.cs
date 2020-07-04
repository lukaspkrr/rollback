using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Damageable : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public UnityEvent  onDeath;
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

}
