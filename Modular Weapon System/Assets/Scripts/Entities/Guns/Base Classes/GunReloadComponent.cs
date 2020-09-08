﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GunReloadComponent : GunComponent
{
    [SerializeField] AmmoStorage playerAmmoStorage;
    [SerializeField] float reloadTime;

    void Start() {
        if (componentAnim != null){
            reloadTime = componentAnim.clip.length;
        }
    }

    public override void Action(Gun gun){
        AmmoCategory category = gun.GunData.AmmoCategory;
        int availableAmmo = playerAmmoStorage.GetAmmoAmount(category);

        if (availableAmmo > 0){
            cooldown.StartCooldownTimer(reloadTime);
            StartCoroutine(ReloadGun(gun, category, availableAmmo));
        }
    }

    IEnumerator ReloadGun(Gun gun, AmmoCategory category, int availableAmmo){
        PlayAnimation();
        yield return new WaitForSeconds(reloadTime); // do not reload gun until it has been completed

        int maximumReloadAmount = Mathf.Min(availableAmmo, gun.GunData.MagazineSize - gun.BulletsInMagazine); // maximum amount should never exceed magazine size
        gun.IncreaseMagazine(maximumReloadAmount);
        playerAmmoStorage.ReduceAmmoAmount(category, maximumReloadAmount);
    }

    public bool IsReloading(){
        return cooldown.IsCooldown;
    }
}
