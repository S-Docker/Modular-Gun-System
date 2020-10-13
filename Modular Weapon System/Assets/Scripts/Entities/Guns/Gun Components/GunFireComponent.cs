using System;
using UnityEngine;
using Random = UnityEngine.Random;

[DisallowMultipleComponent]
public class GunFireComponent : GunComponent
{
    Camera cam;
    int masksToIgnore;

    [Header("Fire Delegates")]
    public OnGunAction onFire;

    protected override void Start(){
        base.Start();
        
        cam = Camera.main;
        masksToIgnore = ~(1 << 9 |1 << 10); // ignore player, weapons and projectile layers
    }

    public override void Action(Gun gun, GunData gunData){
        if (cooldown.IsCooldown) return;

        cooldown.StartCooldownTimer(60 / (gunData.RoundsPerMinute * gunData.RoundsPerMinuteMultiplier.Value));

        Vector3 muzzlePosition = gun.GunMuzzlePosition.transform.position;
        GameObject projectile = Instantiate(gunData.ProjectilePrefab, muzzlePosition, Quaternion.identity);
        
        // ensure spread is not a negative value
        float spreadRadius = Mathf.Max(0, gunData.SpreadRadius.Value);
        
        switch (gun.GunType){
            case GunType.Projectile:
                ProjectileMoveTypeHandler(muzzlePosition, spreadRadius, projectile);
                break;
            case GunType.Hitscan:
                ProjectileHitscanTypeHandler(spreadRadius, projectile);
                break;
        }
        
        ProjectileDamageComponentHandler(gunData, projectile);
        
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

    void ProjectileMoveTypeHandler(Vector3 muzzlePosition, float spreadRadius, GameObject projectile){
        ProjectileMoveComponent projectileMove = projectile.GetComponent<ProjectileMoveComponent>();
        float maxProjectileDistance = projectileMove.MaxProjectileTravel;
        projectileMove.InitialiseMovement(GetProjectileDir(spreadRadius, maxProjectileDistance) - muzzlePosition);
    }

    void ProjectileHitscanTypeHandler(float spreadRadius, GameObject projectile){
        ProjectileHitscanComponent projectileHitscan = projectile.GetComponent<ProjectileHitscanComponent>();
        float maxHitscanDistance = projectileHitscan.MaxHitscanDistance;
        projectileHitscan.InitialiseHitscan(GetProjectileDir(spreadRadius, maxHitscanDistance));
    }
    
    void ProjectileDamageComponentHandler(GunData gunData, GameObject projectile){
        ProjectileDamageComponent projectileDamageComponent = projectile.GetComponent<ProjectileDamageComponent>();
        
        // ensure damage is always 0 or greater to avoid edge cases when applying damage to enemies
        float damage = Mathf.Max(0,gunData.Damage * gunData.DamageMultiplier.Value); 
        
        bool isCrit = IsCrit(gunData);
        projectileDamageComponent.ProjectileDamage = (isCrit ? gunData.CritMultiplier.Value : 1) * damage;
    }
    
    Vector3 GetProjectileDir(float spreadRadius, float maxDistance){
        RaycastHit hit;
        
        Vector3 spreadDeviation = Random.insideUnitCircle * spreadRadius;
        // spreadDeviation / maxDistance provides the inverse of the circle radius based on the cameras current position
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, cam.nearClipPlane) + (spreadDeviation / maxDistance));
        
        return Physics.Raycast(ray, out hit, maxDistance, masksToIgnore) ? hit.point : ray.GetPoint(maxDistance);
    }
}
