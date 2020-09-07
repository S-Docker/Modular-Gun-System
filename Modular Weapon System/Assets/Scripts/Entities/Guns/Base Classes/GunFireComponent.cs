using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GunFireComponent : MonoBehaviour, IGunComponent
{
    [SerializeField] protected Animation fireAnim;

    public void Action(Gun gun){
        GameObject bullet = Instantiate(gun.GunData.BulletPrefab, gun.GunNozzlePosition.transform.position, transform.rotation);
    }
}
