using UnityEngine;

[CreateAssetMenu(fileName = "New Increase Damage After Reload", menuName = "Gun Mods/Increase Damage After Reload")]
public class IncreaseDamageAfterReloadMod : GunModifier
{
    float elapsedTime;
    [Tooltip("Maximum increased damage duration after reload.")]
    [SerializeField] float maxDuration;
    [Tooltip("Guns damage multiplier as a percentage in decimal form before modifiers.")]
    [SerializeField] float damageIncreaseMultiplier;

    bool CanIncreaseDamage() => elapsedTime < maxDuration;

    float IncreaseDamageMultiplier(float currentMultiplier){
        if (!CanIncreaseDamage()){return currentMultiplier; }

        return currentMultiplier + damageIncreaseMultiplier;
    }

    void OnUpdate(Gun target){
        if (elapsedTime < maxDuration){
            elapsedTime += Time.deltaTime;
        } else {
            target.onUpdate -= OnUpdate;
        }
    }

    void OnReload(Gun target){
        elapsedTime = 0f;
        target.onUpdate += OnUpdate;
    }

    public override void ApplyTo(Gun target){
         // Stops bonus damage being applied prior to first reload
        elapsedTime = maxDuration;
        
        target.ReloadComponent.onReload += OnReload;
        target.GunData.DamageMultiplier.AddMod(this.GetInstanceID(), IncreaseDamageMultiplier);
    }

    public override void RemoveFrom(Gun target){
        target.onUpdate -= OnUpdate;
        target.ReloadComponent.onReload -= OnReload;
        target.GunData.DamageMultiplier.RemoveMod(this.GetInstanceID());
    }
}
