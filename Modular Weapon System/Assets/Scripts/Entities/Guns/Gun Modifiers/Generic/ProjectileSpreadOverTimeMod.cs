using UnityEngine;

[CreateAssetMenu(fileName = "New Projectile Spread Over Time Modifier", menuName = "Gun Mods/Generic/Projectile Spread Over Time Modifier")]
public class ProjectileSpreadOverTimeMod : GunModifier
{
    [Tooltip("Projectile spread increase amount per shot as a percentage in decimal form before modifiers")]
    [SerializeField] float spreadIncreasePerShot;
    [SerializeField] float delayBeforeSpreadReset = 1f;
    float currentModifierPercentage;
    float elapsedTimeSinceShot;

    float ProjectileSpreadRadiusModifier(float currentValue){
        float newValue = currentValue + currentModifierPercentage;
        return newValue <= 1 ? newValue : 1;
    }

    void OnFire(Gun target){
        currentModifierPercentage += spreadIncreasePerShot;
        elapsedTimeSinceShot = 0f;
    }

    void OnUpdate(Gun target){
        if (elapsedTimeSinceShot > delayBeforeSpreadReset){
            if (currentModifierPercentage > 0){
                currentModifierPercentage -= spreadIncreasePerShot;
            } 
            
            if (currentModifierPercentage < 0){
                currentModifierPercentage = 0; // zero out to fix precision errors
            }
        }
        
        elapsedTimeSinceShot += Time.deltaTime;
    }

    public override void ApplyTo(Gun target){
        target.FireComponent.onFire += OnFire;
        target.onUpdate += OnUpdate;
        target.GunData.SpreadRadiusModifier.AddMod(this.GetInstanceID(), ProjectileSpreadRadiusModifier);
    }

    public override void RemoveFrom(Gun target){
        target.FireComponent.onFire -= OnFire;
        target.onUpdate -= OnUpdate;
        target.GunData.SpreadRadiusModifier.RemoveMod(this.GetInstanceID());
    }
}