using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    // Start is called before the first frame update
    private int damage;
    private bool slowDown;

    public void SetAttack(Hit hit){
        damage = hit.damage;
        slowDown = hit.slowDown;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Damage enemy = other.GetComponent<Damage>();
        if(enemy != null){
            enemy.TakeDamage(damage);
        }
    }
}
