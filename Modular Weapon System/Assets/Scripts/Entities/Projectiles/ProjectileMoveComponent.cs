using UnityEngine;

[DisallowMultipleComponent]
public class ProjectileMoveComponent : ProjectileComponent
{
    [Header("Projectile Movement Settings")]
    Vector3 startPosition;
    [Range(1, 50)]
    [SerializeField] float projectileSpeed = default;
    [Range(1, 100)]
    [SerializeField] float maxProjectileTravel = default; public float MaxProjectileTravel => maxProjectileTravel;

    public void InitialiseMovement(Vector3 dir){
        startPosition = transform.position;
        transform.LookAt(dir * maxProjectileTravel);
        
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = dir.normalized * projectileSpeed;
        
        Debug.DrawLine(startPosition, dir * maxProjectileTravel, Color.blue, 5f);
    }
    
    void FixedUpdate(){
        if (MaxDistanceTravelled()){
            if (hasEffect){
                InitialiseEffect(transform.position);
            }
            
            Destroy(gameObject);
        }
    }
    
    bool MaxDistanceTravelled(){
        float maxDistance = maxProjectileTravel;
        
        // faster than Vector3.Distance and Mathf.Sqrt
        return (startPosition - transform.position).sqrMagnitude > maxDistance * maxDistance;
    }

    public void InitialiseProjectileMove(float projectileSpeed, float maxProjectileTravel){
        this.projectileSpeed = projectileSpeed;
        this.maxProjectileTravel = maxProjectileTravel;
    }
}
