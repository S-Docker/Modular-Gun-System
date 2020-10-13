using UnityEngine;

[DisallowMultipleComponent]
public class ProjectileHitscanComponent : ProjectileComponent
{
    [Range(1, 100)]
    [SerializeField] float maxHitscanDistance = default; public float MaxHitscanDistance => maxHitscanDistance;
    
    public void InitialiseHitscan(Vector3 pointOfImpact){
        transform.position = pointOfImpact;
    }
}
