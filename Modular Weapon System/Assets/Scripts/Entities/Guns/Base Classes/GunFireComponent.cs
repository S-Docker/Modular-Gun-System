﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GunFireComponent : GunComponent
{

    public override void Action(Gun gun, GunData gunData){
        if (cooldown.IsCooldown) return;

        cooldown.StartCooldownTimer((float)60 / gunData.RoundsPerMinute);

        GameObject projectile = Instantiate(gunData.ProjectilePrefab, gun.GunMuzzlePosition.transform.position, transform.rotation);
        Projectile projectileScript = projectile.GetComponent<Projectile>();
        projectileScript.ProjectileDamage = gunData.BaseDamage;
        
        animator.SetTrigger("IsFire");
        PlayAudio();
        gun.DecrementMagazine();
    }

    protected override void PlayAudio()
    {
        if (componentAudio != null){
            audioSource.PlayOneShot(componentAudio);
        }
    }
}
