using UnityEngine;

[CreateAssetMenu(fileName = "New Gun Damage Multiplier", menuName = "Gun Mods/Generic/Gun Damage Multiplier")]
public class GunDamageMultiplierMod : GunModifier
{
    [Tooltip("Damage multiplier as a percentage in decimal form before modifiers.")]
    [SerializeField] float damageMultiplier;

    float GunDamageMultiplier(float currentMultiplier){
        return currentMultiplier + damageMultiplier;
    }

    public override void ApplyTo(Gun target){
        target.GunData.DamageMultiplier.AddMod(this.GetInstanceID(), GunDamageMultiplier);
    }

    public override void RemoveFrom(Gun target){
        target.GunData.DamageMultiplier.RemoveMod(this.GetInstanceID());
    }
}
