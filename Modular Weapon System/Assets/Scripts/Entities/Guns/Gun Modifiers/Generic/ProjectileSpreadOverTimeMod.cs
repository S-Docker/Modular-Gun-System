using UnityEngine;

[CreateAssetMenu(fileName = "New Projectile Spread Over Time Modifier", menuName = "Gun Mods/Generic/Projectile Spread Over Time Modifier")]
public class ProjectileSpreadOverTimeMod : GunModifier
{
    [Tooltip("Projectile spread increase amount per shot as a percentage in decimal form before modifiers")]
    [SerializeField] float spreadIncreasePerShot;
    [SerializeField] float delayBeforeSpreadReset = 1f;
    float currentModifierPercentage;
    float elapsedTimeSinceShot;

    void OnFire(Gun target){
        currentModifierPercentage += spreadIncreasePerShot;
        target.FireComponent.ProjectileSpreadIncrementValue = Mathf.Min(currentModifierPercentage,1);
        
        elapsedTimeSinceShot = 0f;
        target.SetCrosshairSize();
    }

    void OnUpdate(Gun target){
        if (elapsedTimeSinceShot > delayBeforeSpreadReset){
            if (currentModifierPercentage > 0){
                target.FireComponent.ProjectileSpreadIncrementValue = Mathf.Max(0, currentModifierPercentage -= spreadIncreasePerShot);
                target.SetCrosshairSize();
            }
        }

        elapsedTimeSinceShot += Time.deltaTime;
    }

    public override void ApplyTo(Gun target){
        target.FireComponent.onFire += OnFire;
        target.onUpdate += OnUpdate;

        target.FireComponent.ProjectileSpreadIncrementValue = 0f;
    }

    public override void RemoveFrom(Gun target){
        target.FireComponent.onFire -= OnFire;
        target.onUpdate -= OnUpdate;
    }
}