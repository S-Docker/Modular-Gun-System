using UnityEngine;

[CreateAssetMenu(fileName = "New Increase Gun Damage", menuName = "Gun Mods/IncreaseGunDamage")]
public class IncreaseGunDamageMultiplierMod : GunModifier
{
    [Tooltip("Damage multiplier as a percentage in decimal form before modifiers.")]
    [SerializeField] float damageIncreaseMultiplier;

    float IncreaseGunDamage(float currentMultiplier){
        return currentMultiplier + damageIncreaseMultiplier;
    }

    public override void ApplyTo(Gun target){
        target.GunData.DamageMultiplier.AddMod(this.GetInstanceID(), IncreaseGunDamage);
    }

    public override void RemoveFrom(Gun target){
        target.GunData.DamageMultiplier.RemoveMod(this.GetInstanceID());
    }
}
