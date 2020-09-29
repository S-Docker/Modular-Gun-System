﻿using UnityEngine;

[CreateAssetMenu(fileName = "New Guaranteed Crit After Reload", menuName = "Gun Mods/Guaranteed Crit After Reload")]
public class GuaranteedCritAfterReloadMod : GunModifier
{
    int bulletCount;
    [Tooltip("Maximum number of bullets that can crit after reload.")]
    [SerializeField] int maxCritBullets;
    
    bool CanCrit() => bulletCount < maxCritBullets;

    int IncreaseCritChancePercentage(int critChance){
        if (!CanCrit()){ return critChance; }

        return critChance += 100;
    }

    void OnFire(Gun gun){
        bulletCount++;
    }

    void OnReload(Gun target){
        bulletCount = 0;
    }

    public override void ApplyTo(Gun target){
        // Stops crits being applied prior to first reload
        bulletCount = maxCritBullets;

        target.FireComponent.onFire += OnFire;
        target.ReloadComponent.onReload += OnReload;
        target.GunData.CritChance.AddMod(this.GetInstanceID(), IncreaseCritChancePercentage);
    }

    public override void RemoveFrom(Gun target){
        target.FireComponent.onFire -= OnFire;
        target.ReloadComponent.onReload -= OnReload;
        target.GunData.CritChance.RemoveMod(this.GetInstanceID());
    }
}
