using UnityEngine;

[DisallowMultipleComponent]
public class ProjectileMoveComponent : ProjectileTypeComponent
{
    [Header("Projectile Movement Settings")]
    Vector3 startPosition;
    [Range(1, 100)]
    [SerializeField] float projectileSpeed = default;

    public override void InitialiseMovement(Vector3 pointOfImpact){
        startPosition = transform.position;
        transform.LookAt(pointOfImpact);
        
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = pointOfImpact.normalized * projectileSpeed;
        
        Debug.DrawLine(startPosition, pointOfImpact * maxProjectileTravel, Color.blue, 5f);
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

    // Used by gun creation tool for setting up initial values
    public void SetUpProjectileMoveComponent(float projectileSpeed, float maxProjectileTravel){
        this.projectileSpeed = projectileSpeed;
        this.maxProjectileTravel = maxProjectileTravel;
    }
}
