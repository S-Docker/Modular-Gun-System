﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMoveComponent : MonoBehaviour
{
    [SerializeField] float projectileSpeed = default;
    Vector3 startPosition;
    [SerializeField] float maxProjectileTravel = default; public float MaxProjectileTravel => maxProjectileTravel;
    
    public void InitialiseMovement(Vector3 dir){
        startPosition = this.transform.position;
        transform.LookAt(dir);
        
        Rigidbody rb = this.GetComponent<Rigidbody>();
        rb.velocity = dir.normalized * projectileSpeed;
    }
    
    void FixedUpdate(){
        if (MaxDistanceTravelled()){
            // perform OnHit
            Destroy(this.gameObject);
        }
    }
    
    bool MaxDistanceTravelled(){
        float maxDistance = maxProjectileTravel;
        
        // faster than Vector3.Distance and Mathf.Sqrt
        return (startPosition - transform.position).sqrMagnitude > (maxDistance * maxDistance);
    }
}