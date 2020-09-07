using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    [SerializeField] GameObject gunNozzlePosition; public GameObject GunNozzlePosition => gunNozzlePosition;
    [SerializeField] GunData gunData; public GunData GunData => gunData;

    [SerializeField] GunFireComponent fire;
    [SerializeField] GunAbilityComponent ability;
    [SerializeField] GunReloadComponent reload;

    [SerializeField] int bulletsInMagazine; public int BulletsInMagazine => bulletsInMagazine;

    public void Equip(GameObject gunHoldPosition){
        this.transform.parent = gunHoldPosition.transform;
        transform.localPosition = Vector3.zero;
    }

    public void Unequip(){
        this.transform.parent = null;
    }

    public void Fire(){
        if (bulletsInMagazine > 0 && !reload.IsReloading()){
            fire.Action(this);
        }
    }

    public void Ability(){
        ability.Action(this);
    }

    public void Reload(){
        if (bulletsInMagazine < gunData.MagazineSize && !reload.IsReloading()){
            reload.Action(this);
        }
    }

    public void IncreaseMagazine(int amount){
        bulletsInMagazine += amount;
    }

    public void DecrementMagazine(){
        bulletsInMagazine--;
    }
}
