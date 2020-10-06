using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileStandardCollisionComponent : ProjectileCollisionComponent
{
    [Header("Projectile Collision Settings")]
    [SerializeField] int maxCollisions = 1;
    [SerializeField] bool canBounce;
    int collisionCount = 0;
    
    void OnCollisionEnter(Collision collision){
        if (canBounce && CheckIfGroundCollision(collision)) return; // if can bounce and hit the ground, no action needed

        if (hasEffect){
            InitialiseEffect(collision.contacts[0].point);
        }
        
        damageComponent.ApplyDamage(collision.gameObject);
        collisionCount++;

        if (collisionCount == maxCollisions){
            Destroy(gameObject);
        }
    }
}
