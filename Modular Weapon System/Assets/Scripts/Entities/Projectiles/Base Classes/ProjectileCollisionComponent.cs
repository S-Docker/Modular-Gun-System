using UnityEngine;

[DisallowMultipleComponent]
public abstract class ProjectileCollisionComponent : ProjectileComponent
{
    protected ProjectileDamageComponent damageComponent;

    [Header("Projectile Effect Settings")]
    [SerializeField] protected bool hasEffect = false;
    [SerializeField] protected GameObject effect = null;

    void Start(){
        damageComponent = this.GetComponent<ProjectileDamageComponent>();
    }

    protected bool CheckIfGroundCollision(Collision collision){
        return collision.gameObject.layer == LayerMask.NameToLayer("Ground");
    }
}
