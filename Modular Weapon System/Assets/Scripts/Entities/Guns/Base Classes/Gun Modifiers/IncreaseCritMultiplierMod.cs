using UnityEngine;

[CreateAssetMenu(fileName = "New Increase Crit Multiplier", menuName = "Gun Mods/IncreaseCritMultiplier")]
public class IncreaseCritMultiplierMod : GunModifier
{
    [Tooltip("Crit multiplier as a percentage in decimal form before modifiers.")]
    [SerializeField] float critMultiplier;

    float IncreaseCritMultiplier(float currentMultiplier){
        return currentMultiplier + critMultiplier;
    }

    public override void ApplyTo(Gun target){
        target.GunData.CritMultiplier.AddMod(this.GetInstanceID(), IncreaseCritMultiplier);
    }

    public override void RemoveFrom(Gun target){
        target.GunData.CritMultiplier.RemoveMod(this.GetInstanceID());
    }
}
