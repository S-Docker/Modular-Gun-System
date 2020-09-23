using UnityEngine;

[DisallowMultipleComponent]
public abstract class GunFireComponent : GunComponent
{
    Camera cam;
    int masksToIgnore;

    [Header("Fire Delegates")]
    public OnGunAction onFire;

    protected override void Start(){
        base.Start();
        
        cam = Camera.main;
        masksToIgnore = (1 << 9 | 10); // ignore player, weapons and projectile layers
    }

    public override void Action(Gun gun, GunData gunData){
        if (cooldown.IsCooldown) return;

        cooldown.StartCooldownTimer(60 / (gunData.RoundsPerMinute * gunData.RoundsPerMinuteMultiplier.Value));

        Vector3 muzzlePosition = gun.GunMuzzlePosition.transform.position;
        GameObject projectile = Instantiate(gunData.ProjectilePrefab, muzzlePosition, Quaternion.identity);
        
        ProjectileMoveComponent projectileMove = projectile.GetComponent<ProjectileMoveComponent>();
        float maxProjectileDistance = projectileMove.MaxProjectileTravel;
        projectileMove.InitialiseMovement(GetProjectileDir(maxProjectileDistance) - muzzlePosition);

        ProjectileDamageComponent projectileDamage = projectile.GetComponent<ProjectileDamageComponent>();
        float damage = gunData.Damage * gunData.DamageMultiplier.Value;
        bool isCrit = IsCrit(gunData);
        projectileDamage.ProjectileDamage = (isCrit ? gunData.CritMultiplier.Value : 1) * damage;
        
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
        
        return Physics.Raycast(ray, out hit, maxDistance, masksToIgnore) ? hit.point : ray.GetPoint(maxDistance);
    }
}
