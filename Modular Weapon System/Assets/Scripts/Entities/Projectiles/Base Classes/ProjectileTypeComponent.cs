using UnityEngine;

[DisallowMultipleComponent]
public abstract class ProjectileTypeComponent : ProjectileComponent
{
    [Range(1, 100)]
    [SerializeField] protected float maxProjectileTravel = 1; public float MaxProjectileTravel => maxProjectileTravel;
    
    public abstract void InitialiseMovement(Vector3 pointOfImpact);
}
