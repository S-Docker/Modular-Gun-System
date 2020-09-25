using UnityEngine;

[DisallowMultipleComponent]
public class ProjectileHitscanComponent : ProjectileComponent
{
    public void InitialiseHitscan(Vector3 pointOfImpact){
        transform.position = pointOfImpact;
    }
}
