using UnityEngine;

[CreateAssetMenu(fileName = "New Projectile Spread Multiplier", menuName = "Gun Mods/Generic/Projectile Spread Multiplier")]
public class ProjectileSpreadMultiplierMod : GunModifier
{
    [Tooltip("Bullet spread adjustment as a percentage in decimal form before additional modifiers.")]
    [SerializeField] float spreadMultiplierValue;

    float ProjectileSpreadRadiusMultiplier(float currentValue){
        return currentValue + spreadMultiplierValue;
    }

    public override void ApplyTo(Gun target){
        target.GunData.SpreadRadiusMultiplier.AddMod(this.GetInstanceID(), ProjectileSpreadRadiusMultiplier);
    }

    public override void RemoveFrom(Gun target){
        target.GunData.SpreadRadiusMultiplier.RemoveMod(this.GetInstanceID());
    }
}
