using UnityEngine;

public abstract class ProjectileComponent : MonoBehaviour
{
    [Header("Projectile Effect Settings")]
    [SerializeField] protected bool hasEffect = false;
    [SerializeField] GameObject effect = null;
    
    protected void InitialiseEffect(Vector3 position){
        GameObject effectObject = Instantiate(effect, position, Quaternion.identity);
        
        Destroy(effectObject, 2f);
    }
}
