using UnityEngine;

[CreateAssetMenu(fileName = "New Increase Reload Time Multiplier", menuName = "Gun Mods/IncreaseReloadTimeMultiplier")]
public class IncreaseReloadTimeMultiplierMod : GunModifier
{
    [Tooltip("Reload Time multiplier as a percentage in decimal form before modifiers.")]
    [SerializeField] float reloadTimeMultiplier;

    float IncreaseReloadTimeMultiplier(float currentMultiplier){
        return currentMultiplier + reloadTimeMultiplier;
    }

    public override void ApplyTo(Gun target){
        target.GunData.ReloadTimeMultiplier.AddMod(this.GetInstanceID(), IncreaseReloadTimeMultiplier);
    }

    public override void RemoveFrom(Gun target){
        target.GunData.ReloadTimeMultiplier.RemoveMod(this.GetInstanceID());
    }
}
