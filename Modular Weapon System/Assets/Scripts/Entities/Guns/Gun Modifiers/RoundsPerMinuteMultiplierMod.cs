using UnityEngine;

[CreateAssetMenu(fileName = "New Rounds Per Minute Multiplier", menuName = "Gun Mods/Generic/Rounds Per Minute Multiplier")]
public class RoundsPerMinuteMultiplierMod : GunModifier
{
    [Tooltip("Rounds per minute multiplier as a percentage in decimal form before modifiers.")]
    [SerializeField] float roundsPerMinuteMultiplier;

    float RoundsPerMinuteMultiplier(float currentMultiplier){
        return currentMultiplier + roundsPerMinuteMultiplier;
    }

    public override void ApplyTo(Gun target){
        target.GunData.RoundsPerMinuteMultiplier.AddMod(this.GetInstanceID(), RoundsPerMinuteMultiplier);
    }

    public override void RemoveFrom(Gun target){
        target.GunData.RoundsPerMinuteMultiplier.RemoveMod(this.GetInstanceID());
    }
}
