using UnityEngine;

[CreateAssetMenu(fileName = "New Crit Chance Modifier", menuName = "Gun Mods/Generic/Crit Chance Modifier")]
public class CritChanceMod : GunModifier
{
    [Tooltip("Crit chance as a percentage in value form before modifiers.")]
    [SerializeField] int critChanceValue;

    int CritChance(int currentValue){
        return currentValue + critChanceValue;
    }

    public override void ApplyTo(Gun target){
        target.GunData.CritChance.AddMod(this.GetInstanceID(), CritChance);
    }

    public override void RemoveFrom(Gun target){
        target.GunData.CritChance.RemoveMod(this.GetInstanceID());
    }
}
