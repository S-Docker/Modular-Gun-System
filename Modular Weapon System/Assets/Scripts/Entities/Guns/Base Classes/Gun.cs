using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class Gun : MonoBehaviour, IEquippable
{
    [SerializeField] GameObject gunMuzzlePosition; public GameObject GunMuzzlePosition => gunMuzzlePosition;
    [SerializeField] GunData gunData;

    [SerializeField] GunFireComponent fire;
    [SerializeField] GunAbilityComponent ability;
    [SerializeField] GunReloadComponent reload;

    [SerializeField] int bulletsInMagazine; public int BulletsInMagazine => bulletsInMagazine;

    void Start(){
        gunData = Instantiate(gunData); // make a copy so any changes made do not persist
    }

    public void OnEquipped(){

    }

    public void OnUnequipped(){
        Animator animator = GetComponent<Animator>();
        animator.SetTrigger("IsUnequipped");
        reload.StopReload();
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
