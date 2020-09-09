using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class Gun : MonoBehaviour
{
    [SerializeField] GunData gunData;

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
        reload.StopAudio();
    }

    public void Fire(){
        if (bulletsInMagazine > 0 && !reload.IsReloading()){
            fire.Action(this, gunData);
        }
    }

    public void Ability(){
        ability.Action(this, gunData);
    }

    public void Reload(){
        if (bulletsInMagazine < gunData.MagazineSize){
            reload.Action(this, gunData);
        }
    }

    public void IncreaseMagazine(int amount){
        bulletsInMagazine += amount;
    }

    public void DecrementMagazine(){
        bulletsInMagazine--;
    }
}
