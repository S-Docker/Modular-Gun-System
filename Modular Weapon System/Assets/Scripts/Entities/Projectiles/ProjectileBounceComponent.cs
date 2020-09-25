using UnityEngine;

[DisallowMultipleComponent]
public class ProjectileBounceComponent : MonoBehaviour
{
    [Header("Bounce Settings")]
    [Range(0, 1)]
    [SerializeField] float bounciness = 0;
    [Range(1, 5)]
    [SerializeField] int maxBounces = 3;
    int bounceCount = 0;

    void Start()
    {
        PhysicMaterial projectileMaterial = new PhysicMaterial{
            bounciness = this.bounciness,
            frictionCombine = PhysicMaterialCombine.Minimum,
            bounceCombine = PhysicMaterialCombine.Maximum
        };

        Collider collider = this.GetComponent<Collider>();
        collider.material = projectileMaterial;

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
    }
    void OnCollisionEnter(Collision collision){
        if (collision.gameObject.layer != LayerMask.NameToLayer("Ground")) return;
        
        bounceCount++;
        
        if (bounceCount > maxBounces){
            OnMaxBounces();
        }
    }

    void OnMaxBounces(){
        Destroy(gameObject);
    }
}
