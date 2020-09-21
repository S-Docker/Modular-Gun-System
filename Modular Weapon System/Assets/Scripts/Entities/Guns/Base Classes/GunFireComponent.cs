using UnityEngine;

[DisallowMultipleComponent]
public abstract class GunFireComponent : GunComponent
{
    Camera cam;

    [Header("Fire Delegates")]
    public OnGunAction onFire;

    protected override void Start(){
        base.Start();
        
        cam = Camera.main;
    }

    public override void Action(Gun gun, GunData gunData){
        if (cooldown.IsCooldown) return;

        cooldown.StartCooldownTimer((float)60 / (gunData.RoundsPerMinute * gunData.RoundsPerMinuteMultiplier.Value));

        GameObject projectile = Instantiate(gunData.ProjectilePrefab, gun.GunMuzzlePosition.transform.position, Quaternion.identity);
        ProjectileBehaviour projectileBehaviour = projectile.GetComponent<ProjectileBehaviour>();

        float maxProjectilDistance = projectileBehaviour.ProjectileData.MaxProjectileTravel;
        projectileBehaviour.ProjectileSetup(GetProjectileDir(maxProjectilDistance));
        
        float damage = gunData.Damage * gunData.DamageMultiplier.Value;
        bool isCrit = IsCrit(gunData);
        projectileBehaviour.ProjectileDamage = (isCrit ? gunData.CritMultiplier.Value : 1) * damage;
        
        animator.SetTrigger("IsFire");
        PlayAudio();
        gun.DecrementMagazine();

        onFire?.Invoke(gun);
    }

    protected override void PlayAudio(){
        if (componentAudio.Equals(null)) return;
        
        audioSource.PlayOneShot(componentAudio);
    }

    static bool IsCrit(GunData gunData){
        int critChance = gunData.CritChance.Value;

        return Random.Range(1, 100) <= critChance;
    }

    Vector3 GetProjectileDir(float maxDistance){
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        
        return Physics.Raycast(ray, out hit, maxDistance) ? hit.point : ray.GetPoint(maxDistance);
    }
}
