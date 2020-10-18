using UnityEngine;

public abstract class ProjectileComponent : MonoBehaviour
{
    [Header("Projectile Effect Settings")]
    [SerializeField] protected bool hasEffect = false;
    [SerializeField] GameObject effect = null;

    protected void InitialiseEffect(Vector3 position){
        if (hasEffect){
            Instantiate(effect, position, Quaternion.identity);
        }
    }
}
