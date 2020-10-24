using UnityEngine;

[CreateAssetMenu(fileName = "New Projectile Spread Modifier", menuName = "Gun Mods/Generic/Projectile Spread Modifier")]
public class ProjectileSpreadMod : GunModifier
{
    [Tooltip("Bullet spread adjustment as a value before additional modifiers.")]
    [SerializeField] float projectileSpreadValue;

    float ProjectileSpreadRadius(float currentValue){
        return currentValue + projectileSpreadValue;
    }

    public override void ApplyTo(Gun target){
        target.GunData.SpreadRadius.AddMod(this.GetInstanceID(), ProjectileSpreadRadius);
    }

    public override void RemoveFrom(Gun target){
        target.GunData.SpreadRadius.RemoveMod(this.GetInstanceID());
    }
}


