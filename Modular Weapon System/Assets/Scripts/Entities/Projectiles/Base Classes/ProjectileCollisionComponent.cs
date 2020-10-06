using UnityEngine;

[DisallowMultipleComponent]
public abstract class ProjectileCollisionComponent : ProjectileComponent
{
    protected ProjectileDamageComponent damageComponent;

    void Start(){
        damageComponent = this.GetComponent<ProjectileDamageComponent>();
    }

    protected bool CheckIfGroundCollision(Collision collision){
        return collision.gameObject.layer == LayerMask.NameToLayer("Ground");
    }
}
