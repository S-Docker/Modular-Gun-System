using UnityEngine;

[DisallowMultipleComponent]
public class ProjectileHitscanComponent : ProjectileTypeComponent
{
    public override void InitialiseMovement(Vector3 pointOfImpact){
        transform.position = pointOfImpact;
    }
    
    // Used by gun creation tool for setting up initial values
    public void SetUpProjectileHitscanComponent(float maxProjectileTravel){
        this.maxProjectileTravel = maxProjectileTravel;
    }
}
