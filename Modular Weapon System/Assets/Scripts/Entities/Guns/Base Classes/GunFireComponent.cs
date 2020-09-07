using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GunFireComponent : GunComponent
{
    public override void Action(Gun gun){
        if (cooldown.IsCooldown) return;

        cooldown.StartCooldownTimer((float)60 / gun.GunData.RoundsPerMinute);

        GameObject bullet = Instantiate(gun.GunData.BulletPrefab, gun.GunNozzlePosition.transform.position, transform.rotation);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.BulletDamage = gun.GunData.BaseDamage;
        
        PlayAudio();
        gun.DecrementMagazine();
    }
}
