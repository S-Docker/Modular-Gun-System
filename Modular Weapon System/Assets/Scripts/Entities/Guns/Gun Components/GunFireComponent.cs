using System;
using UnityEngine;
using Random = UnityEngine.Random;

[DisallowMultipleComponent]
public class GunFireComponent : GunComponent
{
    Camera cam;
    int masksToIgnore;
    float projectileSpreadIncrementValue = 1f; public float ProjectileSpreadIncrementValue { set => projectileSpreadIncrementValue = value; }
    [SerializeField] protected AudioClip gunfireAudio;

    [Header("Fire Delegates")]
    public OnGunAction onFire;

    protected override void Start(){
        base.Start();
        
        cam = Camera.main;
        masksToIgnore = ~(1 << 9 |1 << 10); // ignore player, weapons and projectile layers
    }

    public override void Perform(Gun gun, GunData gunData){
        if (cooldown.IsCooldown) return;

        cooldown.StartCooldownTimer(60 / (gunData.RoundsPerMinute * gunData.RoundsPerMinuteMultiplier.Value));

        Vector3 muzzlePosition = gun.GunMuzzlePosition.transform.position;
        GameObject projectile = Instantiate(gunData.ProjectilePrefab, muzzlePosition, Quaternion.identity);
        
        SetProjectileDamage(gunData, projectile);
        
        float spreadValue = (gunData.SpreadRadius.Value * gunData.SpreadRadiusMultiplier.Value) * projectileSpreadIncrementValue;
        MoveProjectile(projectile, spreadValue, muzzlePosition);

        animator.SetTrigger("IsFire");
        PlayAudio();
        gun.DecrementMagazine();

        onFire?.Invoke(gun);
    }
    
    void PlayAudio(){
        if (gunfireAudio == null) return;
        
        audioSource.PlayOneShot(gunfireAudio);
    }

    static bool IsCrit(GunData gunData){
        int critChance = gunData.CritChance.Value;
        return Random.Range(1, 100) <= critChance;
    }
    
    void SetProjectileDamage(GunData gunData, GameObject projectile){
        ProjectileDamageComponent projectileDamageComponent = projectile.GetComponent<ProjectileDamageComponent>();
        
        float damage = gunData.Damage * gunData.DamageMultiplier.Value;
        bool isCrit = IsCrit(gunData);
        projectileDamageComponent.ProjectileDamage = (isCrit ? gunData.CritMultiplier.Value : 1) * damage;
    }

    void MoveProjectile(GameObject projectile, float spreadRadius, Vector3 muzzlePosition){
        ProjectileTypeComponent typeComponent = projectile.GetComponent<ProjectileTypeComponent>();
        float maxProjectileDistance = typeComponent.MaxProjectileTravel;

        Vector3 projectileDir = GetProjectileDir(spreadRadius, maxProjectileDistance);

        // raycast for point of impact is cast from center of camera whereas ballistics travel from the muzzle position
        // and must be offset, this is not required for raycast type as their position is set to point of impact
        if (typeComponent is ProjectileMoveComponent){
            projectileDir -= muzzlePosition;
        }
        typeComponent.InitialiseMovement(projectileDir);
    }

    Vector3 GetProjectileDir(float spreadRadius, float maxDistance){
        RaycastHit hit;
        
        Vector3 spreadDeviation = Random.insideUnitCircle * spreadRadius;
        // spreadDeviation / maxDistance provides the inverse of the circle radius based on the cameras current position
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, cam.nearClipPlane) + (spreadDeviation / maxDistance));
        
        return Physics.Raycast(ray, out hit, maxDistance, masksToIgnore) ? hit.point : ray.GetPoint(maxDistance);
    }
}
