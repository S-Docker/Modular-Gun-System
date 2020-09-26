using UnityEngine;

[DisallowMultipleComponent]
public class ProjectileMoveComponent : ProjectileComponent
{
    [Header("Projectile Movement Settings")]
    Vector3 startPosition;
    [Range(1, 25)]
    [SerializeField] float projectileSpeed = default;
    [Range(1, 100)]
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
