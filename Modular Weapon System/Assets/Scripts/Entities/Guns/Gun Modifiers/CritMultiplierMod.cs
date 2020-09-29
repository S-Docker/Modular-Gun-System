using UnityEngine;

[CreateAssetMenu(fileName = "New Crit Multiplier", menuName = "Gun Mods/Generic/Crit Multiplier")]
public class CritMultiplierMod : GunModifier
{
    [Tooltip("Crit multiplier as a percentage in decimal form before modifiers.")]
    [SerializeField] float critMultiplier;

    float AdjustCritMultiplier(float currentMultiplier){
        return currentMultiplier + critMultiplier;
    }

    public override void ApplyTo(Gun target){
        target.GunData.CritMultiplier.AddMod(this.GetInstanceID(), AdjustCritMultiplier);
    }

    public override void RemoveFrom(Gun target){
        target.GunData.CritMultiplier.RemoveMod(this.GetInstanceID());
    }
}
