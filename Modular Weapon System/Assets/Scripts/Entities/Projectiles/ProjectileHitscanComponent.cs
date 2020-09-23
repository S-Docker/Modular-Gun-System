using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHitscanComponent : ProjectileComponent
{
    public void InitialiseHitscan(Vector3 pointOfImpact){
        Debug.DrawLine(transform.position, pointOfImpact);
        transform.position = pointOfImpact;
    }
}
