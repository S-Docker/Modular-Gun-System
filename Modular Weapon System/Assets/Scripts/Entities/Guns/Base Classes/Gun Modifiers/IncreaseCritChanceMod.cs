using UnityEngine;

[CreateAssetMenu(fileName = "New Increase Crit Chance", menuName = "Gun Mods/IncreaseCritChance")]
public class IncreaseCritChanceMod : GunModifier
{
    [Tooltip("Crit chance as a percentage in value form before modifiers.")]
    [SerializeField] int critChanceIncrease;

    int IncreaseCritChance(int currentValue){
        return currentValue + critChanceIncrease;
    }

    public override void ApplyTo(Gun target){
        target.GunData.CritChance.AddMod(this.GetInstanceID(), IncreaseCritChance);
    }

    public override void RemoveFrom(Gun target){
        target.GunData.CritChance.RemoveMod(this.GetInstanceID());
    }
}
