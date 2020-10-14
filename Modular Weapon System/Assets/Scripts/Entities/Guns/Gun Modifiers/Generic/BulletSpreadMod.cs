using UnityEngine;

[CreateAssetMenu(fileName = "New Bullet Spread Modifier", menuName = "Gun Mods/Generic/Bullet Spread Modifier")]
public class BulletSpreadMod : GunModifier
{
    [Tooltip("Bullet spread adjustment as a value before additional modifiers.")]
    [SerializeField] int bulletSpreadValue;

    float BulletSpreadRadius(float currentValue){
        return currentValue + bulletSpreadValue;
    }

    public override void ApplyTo(Gun target){
        target.GunData.SpreadRadius.AddMod(this.GetInstanceID(), BulletSpreadRadius);
    }

    public override void RemoveFrom(Gun target){
        target.GunData.CritChance.RemoveMod(this.GetInstanceID());
    }
}

