using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBounceComponent : MonoBehaviour
{
    [Header("Bounce Settings")]
    [Range(0, 1)]
    [SerializeField] float bounciness = 0;
    [SerializeField] int maxBounces = 3;
    int bounceCount = 0;

    [Header("Bounce Collision Settings")] 
    [SerializeField] LayerMask bounceLayers;

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
        Debug.Log(collision.gameObject.name);
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
