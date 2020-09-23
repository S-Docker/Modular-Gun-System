using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileStickyCollisionComponent : ProjectileCollisionComponent
{
    Transform projectileTransform; // reduce transform. calls
    [Header("Explosion Delay Settings")] 
    [SerializeField] LayerMask explodeOnTouchLayers;
    [Tooltip("Maximum radius to check for nearby enemies.")]
    [SerializeField] float maxRadiusForDetection;
    [Tooltip("Amount of time before explosion when stuck to environment and no enemies near.")]
    [SerializeField] float maxLifetimeOnEnvironment;

    void OnCollisionEnter(Collision collision){
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        
        projectileTransform = transform;
        projectileTransform.position = collision.contacts[0].point;
        projectileTransform.parent = collision.transform;

        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy")){
            DamageAllEnemiesInRange();
            DestroyProjectile();
        }
        else{
            StartCoroutine(CheckProximity());
            StartCoroutine(EffectTimer(maxLifetimeOnEnvironment));
        }
    }

    IEnumerator CheckProximity(){
        while (true){
            Collider[] hits = EnemiesInRange();

            if (hits.Length != 0){
                DamageAllEnemiesInRange();
                DestroyProjectile();
            }

            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator EffectTimer(float waitTime){
        yield return new WaitForSeconds(waitTime);

        DestroyProjectile();
    }

    Collider[] EnemiesInRange(){
        return Physics.OverlapSphere(projectileTransform.position, maxRadiusForDetection, explodeOnTouchLayers);
    }

    void DamageAllEnemiesInRange(){
        Collider[] hits = EnemiesInRange();
            
        foreach (var hit in hits){
            damageComponent.ApplyDamage(hit.gameObject);
        }
    }

    void DestroyProjectile(){
        if (hasEffect){
            InitialiseEffect(effect, transform.position);
        }

        StopAllCoroutines();
        Destroy(gameObject);
    }
}
