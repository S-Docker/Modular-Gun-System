﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected ProjectileData projectileData;
    Vector3 startPosition;
    float projectileDamage; public float ProjectileDamage { set => projectileDamage = value; }

    protected virtual void Start(){
        startPosition = this.transform.position;

        if (projectileData.HasProjectileEffect){
            projectileData.ProjectileEffect.OnStartEffect();
        }
    }

    protected virtual void Update()
    {
        MoveProjectile();

        if (MaxDistanceTravelled()){
            DestroyProjectile();
        }
    }

    protected virtual void MoveProjectile(){
        transform.position += Vector3.forward * projectileData.ProjectileSpeed * Time.deltaTime;

        if (projectileData.HasProjectileEffect){
            projectileData.ProjectileEffect.ContinuousEffect();
        }
    }

    protected virtual void OnTriggerEnter(Collider other) {
        IHealthComponent healthComponent = other.GetComponent<IHealthComponent>();
        if (healthComponent == null) return;

        if (projectileData.HasProjectileEffect){
            projectileData.ProjectileEffect.OnEndEffect();
        }
        healthComponent.TakeDamage(projectileDamage);
        Destroy(this.gameObject);
    }


    protected virtual bool MaxDistanceTravelled(){
        return (int)Vector3.Distance(startPosition, transform.position) > projectileData.MaxProjectileTravel;
    }

    protected virtual void DestroyProjectile(){
        Destroy(this.gameObject);
    }
}
