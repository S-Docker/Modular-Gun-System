using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileBehaviour : MonoBehaviour
{   Vector3 startPosition;
    [SerializeField] protected ProjectileData projectileData; public ProjectileData ProjectileData => projectileData;
    float projectileDamage; public float ProjectileDamage { set => projectileDamage = value; }
    
    public void ProjectileSetup(Vector3 dir){
        startPosition = this.transform.position;
        transform.LookAt(dir);
        this.GetComponent<Rigidbody>().velocity = dir.normalized * projectileData.ProjectileSpeed;
    }

    protected void FixedUpdate(){
        if (MaxDistanceTravelled()){
            Destroy(this.gameObject);
        }
    }

    protected void OnCollisionEnter(Collision collision){
        if (ProjectileData.HasOnHitEffect){
            InitialiseEffect(collision);
        }
        
        ApplyDamage(collision.gameObject);
        
        Destroy(this.gameObject);
    }

    protected virtual void InitialiseEffect(Collision collision){}

    protected virtual bool MaxDistanceTravelled(){
        float maxDistance = projectileData.MaxProjectileTravel;
        
        // faster than Vector3.Distance and Mathf.Sqrt
        return (startPosition - transform.position).sqrMagnitude > (maxDistance * maxDistance);
    }
    
    void ApplyDamage(GameObject other){
        IHealthComponent healthComponent = other.GetComponent<IHealthComponent>();
        healthComponent?.TakeDamage(projectileDamage);
    }
}
