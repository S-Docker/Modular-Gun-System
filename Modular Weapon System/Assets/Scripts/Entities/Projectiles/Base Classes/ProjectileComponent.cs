using UnityEngine;

public abstract class ProjectileComponent : MonoBehaviour
{
    protected void InitialiseEffect(GameObject effect, Vector3 position){
        GameObject effectObject = Instantiate(effect, position, Quaternion.identity);
        
        Destroy(effect, 2f);
    }
}
