using UnityEngine;

[DisallowMultipleComponent]
public abstract class GunFireComponent : GunComponent
{
    [Header("Fire Delegates")]
    public OnGunAction onFire;

    public override void Action(Gun gun, GunData gunData){
        if (cooldown.IsCooldown) return;

        cooldown.StartCooldownTimer((float)60 / (gunData.RoundsPerMinute * gunData.RoundsPerMinuteMultiplier.Value));

        GameObject projectile = Instantiate(gunData.ProjectilePrefab, gun.GunMuzzlePosition.transform.position, transform.rotation);
        Projectile projectileScript = projectile.GetComponent<Projectile>();

        float damage = gunData.Damage * gunData.DamageMultiplier.Value;
        bool isCrit = IsCrit(gunData);
        projectileScript.ProjectileDamage = (isCrit ? gunData.CritMultiplier.Value : 1) * damage;
        
        animator.SetTrigger("IsFire");
        PlayAudio();
        gun.DecrementMagazine();

        onFire?.Invoke(gun);
    }

    protected override void PlayAudio()
    {
        if (!componentAudio.Equals(null)){
            audioSource.PlayOneShot(componentAudio);
        }
    }

    bool IsCrit(GunData gunData){
        int critChance = gunData.CritChance.Value;

        return Random.Range(1, 100) <= critChance;
    }
}
