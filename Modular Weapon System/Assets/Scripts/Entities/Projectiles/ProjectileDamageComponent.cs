using UnityEngine;

[DisallowMultipleComponent]
public class ProjectileDamageComponent : ProjectileComponent
{
    [Header("Projectile Damage Settings")]
    float projectileDamage; public float ProjectileDamage { set => projectileDamage = value; }
    
    public void ApplyDamage(GameObject other){
        IHealthComponent healthComponent = other.GetComponent<IHealthComponent>();
        healthComponent?.TakeDamage(projectileDamage);
    }
}
