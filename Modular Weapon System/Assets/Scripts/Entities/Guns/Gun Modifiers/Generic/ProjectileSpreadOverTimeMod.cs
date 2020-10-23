using UnityEngine;

[CreateAssetMenu(fileName = "New Projectile Spread Over Time Modifier", menuName = "Gun Mods/Generic/Projectile Spread Over Time Modifier")]
public class ProjectileSpreadOverTimeMod : GunModifier
{
    [Tooltip("Projectile spread increase amount per shot as a percentage in decimal form before modifiers")]
    [SerializeField] float spreadIncreasePerShot;
    [SerializeField] float delayBeforeSpreadReset = 1f;
    float currentPercentageModifier;
    float elapsedTimeSinceShot;

    void OnFire(Gun target){
        currentPercentageModifier += spreadIncreasePerShot;
        target.FireComponent.ProjectileSpreadPercentage = currentPercentageModifier;
        
        elapsedTimeSinceShot = 0f;
        target.SetCrosshairSize();
    }

    void OnUpdate(Gun target){
        if (elapsedTimeSinceShot > delayBeforeSpreadReset){
            if (currentPercentageModifier > 0){
                target.FireComponent.ProjectileSpreadPercentage = currentPercentageModifier -= spreadIncreasePerShot;
                target.SetCrosshairSize();
            }
        }

        elapsedTimeSinceShot += Time.deltaTime;
    }

    public override void ApplyTo(Gun target){
        target.FireComponent.onFire += OnFire;
        target.onUpdate += OnUpdate;

        target.FireComponent.ProjectileSpreadPercentage = 0f;
    }

    public override void RemoveFrom(Gun target){
        target.FireComponent.onFire -= OnFire;
        target.onUpdate -= OnUpdate;
    }
}