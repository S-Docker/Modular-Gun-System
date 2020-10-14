using UnityEngine;

[CreateAssetMenu(fileName = "New Reload Time Multiplier", menuName = "Gun Mods/Generic/Reload Time Multiplier")]
public class ReloadTimeMultiplierMod : GunModifier
{
    [Tooltip("Reload Time multiplier as a percentage in decimal form before modifiers.")]
    [SerializeField] float reloadTimeMultiplier;

    float ReloadTimeMultiplier(float currentMultiplier){
        return currentMultiplier + reloadTimeMultiplier;
    }

    public override void ApplyTo(Gun target){
        target.GunData.ReloadTimeMultiplier.AddMod(this.GetInstanceID(), ReloadTimeMultiplier);
    }

    public override void RemoveFrom(Gun target){
        target.GunData.ReloadTimeMultiplier.RemoveMod(this.GetInstanceID());
    }
}
