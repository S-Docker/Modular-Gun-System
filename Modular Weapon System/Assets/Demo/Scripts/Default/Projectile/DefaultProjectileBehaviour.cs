using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultProjectileBehaviour : ProjectileBehaviour
{
    protected override void InitialiseEffect(Collision collision){
        GameObject effect = Instantiate(projectileData.OnHitEffect, collision.contacts[0].point,
            Quaternion.identity);
        
        Destroy(effect, 2f);
    }
}
