﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
   public int damage;
   public bool destroyOnDemage;
   void DoDamage(Damageable damageable){
       damageable.TakeDamage(damage);
       if(destroyOnDemage){
           Destroy(gameObject);
       }
   }
   
//    private void OnCollisionEnter2D(Collision2D other){
//        Damageable damageable = other.gameObject.GetComponent<Damageable>();
//        if(damageable != null){
//            DoDamage(damageable);
//        }

//    }

   private void OnTriggerEnter2D(Collider2D other){
    Damageable damageable = other.GetComponent<Damageable>();
       if(damageable != null &&(gameObject.layer !=other.gameObject.layer)){
           DoDamage(damageable);
       }
   }
}
