using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMoveComponent : ProjectileComponent
{
    [SerializeField] float projectileSpeed = default;
    Vector3 startPosition;
    [SerializeField] float maxProjectileTravel = default; public float MaxProjectileTravel => maxProjectileTravel;
    
    [Header("Projectile Effect Settings")]
    [SerializeField] bool hasMaxOnReachedEffect = false;
    [SerializeField] GameObject maxDistReachedEffect = null;
    
    public void InitialiseMovement(Vector3 dir){
        startPosition = this.transform.position;
        transform.LookAt(dir);
        
        Rigidbody rb = this.GetComponent<Rigidbody>();
        rb.velocity = dir.normalized * projectileSpeed;
    }
    
    void FixedUpdate(){
        if (MaxDistanceTravelled()){
            if (hasMaxOnReachedEffect){
                InitialiseEffect(maxDistReachedEffect, transform.position);
            }
            
            Destroy(gameObject);
        }
    }
    
    bool MaxDistanceTravelled(){
        float maxDistance = maxProjectileTravel;
        
        // faster than Vector3.Distance and Mathf.Sqrt
        return (startPosition - transform.position).sqrMagnitude > maxDistance * maxDistance;
    }
}
