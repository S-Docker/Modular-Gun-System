using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollisionComponent : MonoBehaviour
{
    ProjectileDamageComponent damageComponent;
    
    [Header("Projectile Collision Settings")]
    [SerializeField] int maxCollisions = 1;
    [SerializeField] bool canBounce;
    int collisionCount = 0;

    [Header("Projectile Effect Settings")]
    [SerializeField] bool hasOnHitEffect = false;
    [SerializeField] GameObject onHitEffect = null;

    void Start(){
        damageComponent = this.GetComponent<ProjectileDamageComponent>();
    }

    void OnCollisionEnter(Collision collision){
        if (canBounce && CheckIfGroundCollision(collision)) return; // if can bounce and hit the ground, no action needed

        if (hasOnHitEffect){
            InitialiseEffect(collision);
        }
        
        damageComponent.ApplyDamage(collision.gameObject);
        collisionCount++;

        if (collisionCount == maxCollisions){
            Destroy(this.gameObject);
        }
    }
    
    void InitialiseEffect(Collision collision){
        GameObject effect = Instantiate(onHitEffect, collision.contacts[0].point,
            Quaternion.identity);
        
        Destroy(effect, 2f);
    }

    bool CheckIfGroundCollision(Collision collision){
        return collision.gameObject.layer == LayerMask.NameToLayer("Ground");
    }
}
